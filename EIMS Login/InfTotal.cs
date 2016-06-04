using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EIMS_Login
{
    class InfTotal
    {
        Connection temp = new Connection();
        public  InfTotal()
        {

        }
        public string InfTotalSet(string tablename)
        {
            string InfTotal = "select count(*) from " + tablename + "";
            SqlCommand cmd = new SqlCommand(InfTotal, temp.GetConn());
            return Convert.ToString(cmd.ExecuteScalar());
        }
    }
}
