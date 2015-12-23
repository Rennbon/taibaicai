using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOHelper.Common.DB;

namespace BusinessService
{
    public class DAL : AdoTemplate
    {
        public DAL()
            : base(new Database()
            {
                Name = string.Empty,
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString,
                ProviderName = "System.Data.SqlClient"
            })
        {
        }
    }
}
