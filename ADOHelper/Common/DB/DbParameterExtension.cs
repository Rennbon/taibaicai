using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace ADOHelper.Common.DB
{
    public static class DbParameterExtension
    {
        public static DbParameter Name(this DbParameter parameter, string name)
        {
            parameter.ParameterName = name;
            return parameter;
        }

        public static DbParameter Type(this DbParameter parameter, DbType type)
        {
            parameter.DbType = type;
            return parameter;
        }

        public static DbParameter Size(this DbParameter parameter, int size)
        {
            parameter.Size = size;
            return parameter;
        }

        public static DbParameter Direction(this DbParameter parameter, ParameterDirection direction)
        {
            parameter.Direction = direction;
            return parameter;
        }

        public static DbParameter Precision(this DbParameter parameter, byte precision)
        {
            ((IDbDataParameter)parameter).Precision = precision;
            return parameter;
        }

        public static DbParameter Scale(this DbParameter parameter, byte scale)
        {
            ((IDbDataParameter)parameter).Scale = scale;
            return parameter;
        }

        public static DbParameter Value(this DbParameter parameter, object value)
        {
            parameter.Value = value == null ? DBNull.Value : value;
            return parameter;
        }
    }
}
