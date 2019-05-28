using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Management_Simple
{
    public static class Connection
    {
        public static SqlConnection GetConnection()
        {
            // Add referemce to > Assemblies > System.Configutation
            SqlConnection con = new SqlConnection();
            con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["StockConn"].ConnectionString;
            return con;
        }
    }
}
