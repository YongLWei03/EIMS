using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using EIMS_Login.Ordinaryusers;
namespace EIMS_Login.WarehouseManager.warehousedatagrid
{
    public enum statusEnum
    {
        未操作,
        同意,
        不同意 
    }
    /// <summary>
    /// transferapplicationdatagrid.xaml 的交互逻辑
    /// </summary>
    public partial class transferapplicationdatagrid : UserControl
    {
        Connection TempConn = new Connection();
        OrdinaryUserInfo TempUser = new OrdinaryUserInfo();
        public statusEnum Mystatus
        {
            get;
            set;
        }
        public transferapplicationdatagrid()
        {
            InitializeComponent();
        }
        public void connectatabase(int myselect)
        {
            statusEnum newstatus = new statusEnum();
            newstatus = (statusEnum)myselect;
            string sqll = "Select * From ApplyEquip where Status = '"+newstatus+"'";
            SqlDataAdapter sqldata = new SqlDataAdapter(sqll, TempConn.GetConn());
            DataSet ds = new DataSet();
            sqldata.Fill(ds);
            Application.ItemsSource = ds.Tables[0].DefaultView;
        }

        private void Application_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
        /*函数功能：更新数据库*/
        public void updata()
        {
            DataTable dt = (Application.ItemsSource as DataView).Table;
            DataRow[] Changedata = dt.Select("Status Like '同意'");
            Application.CommitEdit();
            string StrSelect = "select * from ApplyEquip";
            SqlDataAdapter ShlyAdapter = new SqlDataAdapter();
            ShlyAdapter.SelectCommand = new SqlCommand(StrSelect, TempConn.GetConn());
            SqlCommandBuilder cb = new SqlCommandBuilder(ShlyAdapter);
            //cb.RefreshSchema();
            ShlyAdapter.Update(dt);

            string ZBStrSql;
            SqlCommand ZB;
            SqlDataReader ZBPrice;

            SqlCommand Allocation = new SqlCommand();
            Allocation.Connection = TempConn.GetConn();
            
            int i = 0;
            while(Changedata.Length >i)
            {
                try
                {
                    ZBStrSql = "Select Zbprice From ArmsSurplus Where Zbid='" + Changedata[i][5] + "'";
                    ZB = new SqlCommand(ZBStrSql, TempConn.GetConn());
                    ZBPrice = ZB.ExecuteReader();
                    ZBPrice.Read();
                    MessageBox.Show(ZBPrice[0].ToString());
                    string insertsql = "insert into ArmsAllo values('" + Changedata[i][1] + "' , '" + Changedata[i][5] + "','" + Changedata[i][6] + "','" + ZBPrice[0] + "','','"
                        + Changedata[i][8] + "','" + Changedata[i][7] + "' , '','1','" + TempUser.UserInfoTemp.RyName + "','','" + DateTime.Now.ToString("yyyy-MM-dd") + "','','未完成')";
                    Allocation.CommandText = insertsql;
                    ZBPrice.Close();
                    Allocation.ExecuteNonQuery();
                    i++;
                }
                catch(Exception Error)
                {
                    MessageBox.Show(Error.ToString());
                }
            }
        }
    }
}
