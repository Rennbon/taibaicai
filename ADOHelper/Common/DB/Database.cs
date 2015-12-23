using System;
using System.Data;
using System.Data.Common;

namespace ADOHelper.Common.DB
{
    public class Database
    {
        private string providerName;
        private DbProviderFactory factory;
        [ThreadStatic]
        private static AdoTransaction currentTransaction;

        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public string ProviderName
        {
            get { return providerName; }
            set
            {
                providerName = value;
                factory = DbProviderFactories.GetFactory(providerName);
            }
        }
        internal static bool IsInAdoTransaction
        {
            get { return currentTransaction != null; }
        }

        #region factory method

        internal DbCommand CreateCommand()
        {
            return factory.CreateCommand();
        }

        internal DbDataAdapter CreateDataAdapter()
        {
            return factory.CreateDataAdapter();
        }

        #endregion

        #region resource management

        internal void AllocateResource(DbCommand cmd)
        {
            if (!IsInAdoTransaction)
            {
                AllocateConnection(cmd);
            }
            else if (currentTransaction.DbTransaction == null)
            {
                AllocateConnection(cmd);
                currentTransaction.Database = this;
                currentTransaction.DbTransaction = cmd.Transaction = cmd.Connection.BeginTransaction();
            }
            else
            {
                if (IsNotEqual(currentTransaction.Database, this))
                {
                    throw new InvalidOperationException("AdoTransctionScope不支持分布式事务。");
                }
                cmd.Connection = currentTransaction.DbConnection;
                cmd.Transaction = currentTransaction.DbTransaction;
            }
        }

        private void AllocateConnection(DbCommand cmd)
        {
            cmd.Connection = factory.CreateConnection();
            cmd.Connection.ConnectionString = ConnectionString;
            cmd.Connection.Open();
        }

        private static bool IsNotEqual(Database expected, Database actual)
        {
            return !(expected.Name == actual.Name &&
                expected.ConnectionString == actual.ConnectionString &&
                expected.ProviderName == actual.ProviderName);
        }

        internal void CloseConnectionIfNecessary(DbCommand cmd)
        {
            if (!IsInAdoTransaction)
            {
                cmd.Connection.Close();
            }
        }

        #endregion

        #region transaction managent

        internal static void BeginTransaction()
        {
            currentTransaction = new AdoTransaction();
        }

        internal static void CommitTransaction()
        {
            currentTransaction.DbTransaction.Commit();
        }

        internal static void DisposeTransaction()
        {
            if (currentTransaction.DbTransaction != null)
            {
                currentTransaction.DbConnection.Close();
            }
            currentTransaction = null;
        }

        #endregion
    }
}
