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


namespace EIMS_Login.Ordinary_users
{
    /// <summary>
    /// OrdinaryUsers_Databorrow.xaml 的交互逻辑
    /// </summary>

    public partial class OrdinaryUsers_Databorrow : UserControl
    {
        Connection Temp = new Connection();
        public OrdinaryUsers_Databorrow()
        {
            InitializeComponent();
            InitTabel();//表格初始化 
            TableToApply.DataTableSelect("select * from ApplyData","更新");
        }

        private void InitTabel()
        {
            TableToApply.InitTableHeightWidth(210, 810);
            TableToApply.AddColumns("Ryid", "编号", 80);
            TableToApply.AddColumns("Ryname", "姓名", 80);
        }


        //申请提交按钮功能
        private void ApplicationSubmit_Click(object sender, RoutedEventArgs e)
        {
            string StrSQL = "insert into ApplyData values('1','卧槽','啥','6019','20140808','"+ ApplicationDataNumber .Text+ "',"
                + ApplicationDataCount.Text + ",'"+ApplicationReasons.Text+"','未操作')";
            try
            {
                SqlCommand cmd = new SqlCommand(StrSQL, Temp.GetConn());
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("申请失败！");
                return;
            }
            TableToApply.DataTableSelect("select * from ApplyData","更新");//申请成功更新：申请历史表格
        }

        //申请数量TextBox设置为只能输入数字！
        private void ApplicationDataCount_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                (e.Key >= Key.D0 && e.Key <= Key.D9) ||
                 e.Key == Key.Back ||
                 e.Key == Key.Left || e.Key == Key.Right)
            {
                if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        

        private void ExportTable_AH_Click(object sender, RoutedEventArgs e)
        {
            string[] Str = { "ID", "申请人编号", "申请人名字", "工作岗位", "申请编号", "申请日期", "申请资料编号",
                "申请数量", "申请原因", "操作状态" };
            TableToApply.ExportExcel("select * from ApplyData", Str, "申请历史表格.xlsx");
        }
    }
}
