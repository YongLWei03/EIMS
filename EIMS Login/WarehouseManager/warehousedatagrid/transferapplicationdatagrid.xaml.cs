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
            Application.CommitEdit();
            string StrSelect = "select * from ApplyEquip";
            SqlDataAdapter ShlyAdapter = new SqlDataAdapter();
            ShlyAdapter.SelectCommand = new SqlCommand(StrSelect, TempConn.GetConn());
            SqlCommandBuilder cb = new SqlCommandBuilder(ShlyAdapter);
            //cb.RefreshSchema();
            ShlyAdapter.Update(dt);
        }
    }
}
