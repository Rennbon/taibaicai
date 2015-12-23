using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace ADOHelper.Common.DB
{
    public static class DbCommandExtension
    {
        public static DbParameter DeclareParameter(this DbCommand cmd, string name)
        {
            DbParameter parameter = cmd.CreateParameter();
            parameter.ParameterName = name;
            cmd.Parameters.Add(parameter);
            return parameter;
        }
    }
}
