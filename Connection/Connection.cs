using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CORE_CRUD_API.Connection
{
    public class Connection
    {
        public static IDbConnection GetConnection()
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(projectPath)
            .AddJsonFile("appsettings.json")
            .Build();
            string cs= configuration.GetConnectionString("CS");
            IDbConnection connection = new SqlConnection(cs);
            return connection;
        }
    }
}
