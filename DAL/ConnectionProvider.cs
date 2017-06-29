using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class ConnectionProvider
    {

        internal static SqlConnection CreateConnection() 
        {
            SqlConnection newConnection = new SqlConnection();
            newConnection.ConnectionString = ConnectionProvider.ConnectionString;
            return newConnection;
        }

        internal static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString.ToString();
            }
        }
    }
}
