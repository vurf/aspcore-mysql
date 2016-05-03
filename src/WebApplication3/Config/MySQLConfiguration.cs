using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Config
{

    public class MySQLConfiguration : DbConfiguration
    {
        public MySQLConfiguration()
        {
            var dataSet = (DataSet)ConfigurationManager.GetSection("system.data");
            dataSet.Tables[0].Rows.Clear();
            dataSet.Tables[0].Rows.Add(
                "MySQL Data Provider",
                ".Net Framework Data Provider for MySQL",
                "MySql.Data.MySqlClient",
                typeof(MySqlClientFactory).AssemblyQualifiedName
            );

            SetProviderServices("MySql.Data.MySqlClient", new MySqlProviderServices());
            SetDefaultConnectionFactory(new MySqlConnectionFactory());
        }
    }
}
