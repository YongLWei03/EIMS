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

namespace EIMS_Login.ConfidentialMembers
{
    public enum statusEnum
    {
        未操作,
        同意,
        不同意
    }
    /// <summary>
    /// ApplyDataGrid.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class ApplyDataGrid : UserControl
    {
        Connection TempConn = new Connection();
        public statusEnum Mystatus
        {
            get;
            set;
        }
        public ApplyDataGrid()
        {
            InitializeComponent();
        }
        public void connectatabase(int myselect)
        {
            statusEnum newstatus = new statusEnum();
            newstatus = (statusEnum)myselect;
            string sqll = "Select * From ApplyData where Status = '" + newstatus + "'";
            SqlDataAdapter sqldata = new SqlDataAdapter(sqll, TempConn.GetConn());
            DataSet ds = new DataSet();
            sqldata.Fill(ds);
            Application.ItemsSource = ds.Tables[0].DefaultView;
        }
        private void Application_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
        public void updata()
        {
            DataTable dt = (Application.ItemsSource as DataView).Table;
            DataRow[] Changedata = dt.Select("Status Like '同意'");
            Application.CommitEdit();
            string StrSelect = "select * from ApplyData";
            SqlDataAdapter ShlyAdapter = new SqlDataAdapter();
            ShlyAdapter.SelectCommand = new SqlCommand(StrSelect, TempConn.GetConn());
            SqlCommandBuilder cb = new SqlCommandBuilder(ShlyAdapter);
            //cb.RefreshSchema();
            ShlyAdapter.Update(dt);

            string StrSql;
            SqlCommand ZB;
            SqlDataReader ArmsData;

            SqlCommand Allocation = new SqlCommand();
            Allocation.Connection = TempConn.GetConn();


            int i = 0;
            int Total = 0;
            while (Changedata.Length > i)
            {
                Total += Convert.ToInt32(Changedata[i][6]);
                i++;
            }
            i = 0;
            for (;i< Changedata.Length;i++) 
            {
                try
                {
                    StrSql = "Select * From ArmsData Where DataNo='" + Changedata[i][5] + "'";
                    ZB = new SqlCommand(StrSql, TempConn.GetConn());
                    ArmsData = ZB.ExecuteReader();
                    ArmsData.Read();
                    if (Total > Convert.ToInt32(ArmsData[3].ToString()))
                    {
                        MessageBox.Show("同意数量超过库存资料总量，无法提交！");
                        return;
                    }
                    string insertsql = "insert into DataLend values('" + Changedata[i][5] + "' , '" + Changedata[i][4] + "','" + Changedata[i][1] + "'," + Changedata[i][6] + ",'"
                        + Changedata[i][2] + "','借阅')";
                    Allocation.CommandText = insertsql;
                    ArmsData.Close();
                    Allocation.ExecuteNonQuery();
                }
                catch (Exception Error)
                {
                    MessageBox.Show(Error.ToString());
                    return;
                }
            }
            MessageBox.Show("提交成功！");
        }
    }
}
