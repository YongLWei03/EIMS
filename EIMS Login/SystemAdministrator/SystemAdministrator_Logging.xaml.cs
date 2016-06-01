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
using EIMS_Login;

namespace EIMS_Login
{
    /// <summary>
    /// SystemAdministrator_3.xaml 的交互逻辑
    /// </summary>
    public partial class SystemAdministrator_Logging : UserControl
    {
        Connection Temp = new Connection();
        MoreInf moinf;
        string[] Str = { "日志编号", "事件发生日期", "事件发生时间", "事件类型", "事件标题", "操作用户账号","事件内容" };
        
        public SystemAdministrator_Logging()
        {
            InitializeComponent();
            
            InitLoggingTable();
            LoggingTable.DataTableSelect("select * from SysLog where UserName like 'A%'", "更新");
            //LoggingTable.DataTableSelect(loggingRy(user.SelectedIndex), "更新");
            InitRightBm();
        }
        private string loggingRy(int SelectUser)
        {
            string System_administrator = "select * from SysLog where UserName like 'B%'";
            string Maintenance_man = "select * from SysLog where UserName like 'C%'";
            string Warehouse_manager = "select * from SysLog where UserName like 'D%'";
            string Finance_department = "select * from SysLog where UserName like 'E%'";
            string Confidential_clerk = "select * from SysLog where UserName like 'F%'";
            MessageBox.Show(user.SelectedIndex.ToString());
            switch(SelectUser)
            {
                case -1:
                    
                    return System_administrator;
                case 0:

                    return System_administrator;
                case 1:
                    
                    return Maintenance_man;
                case 2:
                    
                    return Warehouse_manager;
                case 3:
                    
                    return Finance_department;
                case 4:
                    
                    return Confidential_clerk;
            }
            return null;
        }
        private void loggingscan()
        {
            
        }
        private void InitLoggingTable()
        {
            LoggingTable.InitTableHeightWidth(449, 1250);
            LoggingTable.AddColumns("LogId", "日志编号", 80);
            LoggingTable.AddColumns("LogDate", "事件发生日期", 120);
            LoggingTable.AddColumns("LogTime", "事件发生时间", 120);
            LoggingTable.AddColumns("LogType", "事件类型", 120);
            LoggingTable.AddColumns("Title", "事件标题", 160);           
            LoggingTable.AddColumns("UserName", "操作用户账号", 169);
            LoggingTable.AddColumns("Body", "事件内容", 580);

        }

        private void user_DropDownClosed(object sender, EventArgs e)
        {
            LoggingTable.DataTableSelect(loggingRy(user.SelectedIndex), "更新");
        }

        private void OrdinarySearch_button_Click(object sender, RoutedEventArgs e)
        {
            string OriStrSql = "select * from SysLog where UserName ='" + OrdinarySearch.Text + "'";
            try
            {
                SqlCommand Ori = new SqlCommand(OriStrSql, Temp.GetConn());
                if (Ori.ExecuteScalar() == null)
                {
                    MessageBox.Show("无此人员！");
                }
                else
                {
                    LoggingTable.DataTableSelect(OriStrSql, "更新");
                }

            }
            catch (Exception a)
            {
                MessageBox.Show("提交失败！" + a);
                return;
            }
        }

        private void OrdinaryExport_button_Click(object sender, RoutedEventArgs e)
        {
           
            LoggingTable.ExportExcel(loggingRy(user.SelectedIndex), Str, "借阅历史表格.xlsx"); 
        }

        public void InitRightBm()
        {
            MenuItem TempMenu = LoggingTable.AddMenuItem("事件内容");
            TempMenu.Click += EventContent;
            LoggingTable.dgMenu.Items.Add(TempMenu);
        }

        public void EventContent(object sender,RoutedEventArgs e)
        {
            moinf = new MoreInf();
            string[] Table_Str = { "LogId", "LogDate", "LogTime", "LogType", "Title", "UserName", "Body" };
            moinf.SetValues(LoggingTable.Getdt(), LoggingTable.dataGrid.SelectedIndex, LoggingTable.Rows, Table_Str, Str, 7);
            moinf.Show();
        }
    }
}
