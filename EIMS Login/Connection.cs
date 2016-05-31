using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace EIMS_Login
{
    class Connection
    {
        static string ConnStr = "Data Source=.;Initial Catalog=EIMS;User ID=EIMS;Password=1";
        static SqlConnection lo_conn = new SqlConnection(ConnStr);
        public Connection()
        {
            
        }
        public string GetConnStr()
        {
            return ConnStr;
        }
        public int SqlConn()
        {
            try
            {
                lo_conn.Open();
            }
            catch
            {
                return 1;
            }
            return 0;
        }
        public SqlConnection GetConn()
        {
            return lo_conn;
        }
    }
}
