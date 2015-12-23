using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ADOHelper.Common.DB
{
    public static class DbConfig
    {
        public static Database CreateDatabase(string name)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            if (settings == null)
            {
                throw new ArgumentException("无效数据库名称。", "name");
            }

            return new Database()
            {
                Name = name,
                ConnectionString = settings.ConnectionString,
                ProviderName = settings.ProviderName
            };
        }
    }
}
