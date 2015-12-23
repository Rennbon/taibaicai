using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.SqlClient;
using System.Transactions;

namespace ADOHelper.Common.DB
{
    public class AdoTemplate
    {
        private const int SUCCESS = 0;

        public AdoTemplate(Database db)
        {
            Database = db;
        }

        private Database Database { get; set; }

        public T ExecuteReaderForObject<T>(Action<DbCommand> prepare, Func<IDataRecord, T> extract)
        {
            Func<DbCommand, T> execute = cmd =>
            {
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return extract(reader);
                    }
                    else
                    {
                        return default(T);
                    }
                }
            };

            return ExecuteStoredProcedure(prepare, execute);
        }

        public List<T> ExecuteReaderForList<T>(Action<DbCommand> prepare, Func<IDataRecord, T> extract)
        {
            Func<DbCommand, List<T>> execute = cmd =>
            {
                List<T> list = new List<T>();
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(extract(reader));
                    }
                }
                return list;
            };

            return ExecuteStoredProcedure(prepare, execute);
        }

        public int ExecuteStoredProcedure(Action<DbCommand> prepare, Action<DbParameterCollection> extract)
        {
            Func<DbCommand, int> execute = cmd =>
            {
                cmd.ExecuteNonQuery();
                int ret = (int)cmd.Parameters["@ret"].Value;
                if (ret == SUCCESS)
                {
                    extract(cmd.Parameters);
                }
                return ret;
            };

            return ExecuteStoredProcedure(prepare, execute);
        }
  
        public int ExecuteStoredProcedure(Action<DbCommand> prepare)
        {
            Func<DbCommand, int> execute = cmd =>
            {
                cmd.ExecuteNonQuery();
                return (int)cmd.Parameters["@ret"].Value;
            };

            return ExecuteStoredProcedure(prepare, execute);
        }

        public T ExecuteScale<T>(Action<DbCommand> prepare) where T : struct
        {
            Func<DbCommand, T> execute = cmd =>
            {
                object result = cmd.ExecuteScalar();
                return (T)(result == null ? 0 : result);
            };

            return ExecuteStoredProcedure(prepare, execute);
        }

        public DataSet ExecuteDataSet(Action<DbCommand> prepare)
        {
            Func<DbCommand, DataSet> execute = cmd =>
            {
                DbDataAdapter adapter = Database.CreateDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            };

            return ExecuteStoredProcedure(prepare, execute);
        }

        private T ExecuteStoredProcedure<T>(Action<DbCommand> prepare, Func<DbCommand, T> execute)
        {
            if (Transaction.Current != null && Database.IsInAdoTransaction)
            {
                throw new InvalidOperationException("AdoTransactionScope作为根事务使用时不能嵌套TransactionScope。");
            }

            DbCommand cmd = Database.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            prepare(cmd);
            cmd.DeclareParameter("@ret").Type(DbType.Int32).Direction(ParameterDirection.ReturnValue);

            try
            {
                Database.AllocateResource(cmd);
                return execute(cmd);
            }
            finally
            {
                Database.CloseConnectionIfNecessary(cmd);
            }
        }
    }
}
