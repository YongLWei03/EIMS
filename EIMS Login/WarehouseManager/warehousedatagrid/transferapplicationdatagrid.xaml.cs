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
        DataTable tansfertable = new DataTable();
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
            tansfertable = ds.Tables[0];
        }

        private void Application_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
        /*函数功能：更新数据库*/
        public bool updata()
        {
            DataTable dt = (Application.ItemsSource as DataView).Table;
            DataRow[] Changedata = dt.Select("Status Like '同意'");
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
                    ZBStrSql = "Select Zbprice,Zbnum,Sid From ArmsSurplus Where Zbid='" + Changedata[i][5] + "'";
                    ZB = new SqlCommand(ZBStrSql, TempConn.GetConn());
                    ZBPrice = ZB.ExecuteReader();
                    ZBPrice.Read();
                    if((int.Parse(ZBPrice[1].ToString()) - int.Parse(Changedata[i][6].ToString())) < 0)
                    {
                        MessageBox.Show("装备id为"+ Changedata[i][5]+"库存不足！");
                        ZBPrice.Close();
                        return false;
                    }
                    string ZBsetsql = "update ApplyEquip set Status = '同意' where ApplyID = '"+Changedata[i][0]+"'";
                    SqlCommand ZBset = new SqlCommand(ZBsetsql,TempConn.GetConn());
                    string insertsql = "insert into ArmsAllo values('" + Changedata[i][1] + "' , '" + Changedata[i][5] + "','" + Changedata[i][6] + "','" + ZBPrice[0] + "','"+ZBPrice[2]+"','"
                        + Changedata[i][8] + "','" + Changedata[i][7] + "' , '','1','" + TempUser.UserInfoTemp.RyName + "','','" + DateTime.Now.ToString("yyyy-MM-dd") + "','','未完成')";
                    Allocation.CommandText = insertsql;
                    string setsql = "update ArmsSurplus set Zbnum = '" + (int.Parse(ZBPrice[1].ToString()) - int.Parse(Changedata[i][6].ToString())) + "' where Zbid = '" + Changedata[i][5] + "'";
                    ZBPrice.Close();
                    ZBset.ExecuteNonQuery();
                    Allocation.ExecuteNonQuery();
                    SqlCommand setnum = new SqlCommand(setsql, TempConn.GetConn());
                    setnum.ExecuteNonQuery();
                    i++;
                }
                catch
                {
                    MessageBox.Show("错误！");
                    return false;
                }
            }
            return true;
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MoreInf moinf = new MoreInf();
            string[] Table_Str = { "ApplyID", "Ryid", "Ryname", "Position", "ApplyDate", "Zbid", "ApplyCount", "AType", "InDep", "ApplyReason", "Status" };
            string[] Str = { "申请编号", "人员编号", "人员姓名", "工作岗位",  "申请日期", "装备编号",
                "申请数量", "调拨方式", "调入单位", "申请理由", "完成状态" };
            if (Application.SelectedIndex != -1)
            {
                moinf.SetValues(tansfertable, Application.SelectedIndex, Application.Items.Count, Table_Str, Str, 11);
                moinf.Show();
            }
            else MessageBox.Show("当前未选中任何行！");
        }
    }
}
