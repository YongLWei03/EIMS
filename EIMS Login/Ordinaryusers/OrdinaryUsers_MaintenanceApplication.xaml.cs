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

namespace EIMS_Login.Ordinaryusers
{
    /// <summary>
    /// OrdinaryUsers_MaintenanceApplication.xaml 的交互逻辑
    /// </summary>
    public partial class OrdinaryUsers_MaintenanceApplication : UserControl
    {
        Connection Temp = new Connection();
        OrdinaryUserInfo UITemp = new OrdinaryUserInfo();
        string ApplyTableSql;
        string HistoryTableSql;
        string ApplyType = "wx_sq";
        MaintainMoreInfoWindows mmiw;//详细信息窗口
        int mmiw_OpenSign = 0;//详细信息窗口标记，0为未打开过！
        public OrdinaryUsers_MaintenanceApplication()
        {
            InitializeComponent();
            ApplyTableSql = "select * from ApplyMaintain where Ryid='" + UITemp.UserInfoTemp.Ryid + "'";
            HistoryTableSql = "select * from ArmsRepair where RyId='" + UITemp.UserInfoTemp.Ryid + "'";


            InitTabelToApply();//申请历史表格初始化 
            InitTableToHistory();//借阅历史表格初始化
            TableToApply.DataTableSelect(ApplyTableSql, "更新");
            Initttalbm();
            ApplicationHistoryCount.Content = TableToApply.Rows;//申请总计
            TableToHistory.DataTableSelect(HistoryTableSql, "更新");
            Inittthlbm();
            MaintenanceHistoryCount.Content = TableToHistory.Rows;//借阅历史总计
            UpDateRLR(1);//更新左下侧操作历史
            
        }
        


        //初始化申请历史右键菜单
        public void Initttalbm()
        {
            MenuItem TempMenu = TableToApply.AddMenuItem("查看更多");
            TempMenu.Click += LookMore;
            TableToApply.dgMenu.Items.Add(TempMenu);
        }



        //初始化申请历史表格
        private void InitTabelToApply()
        {
            TableToApply.InitTableHeightWidth(210, 810);
            TableToApply.SetCanUserAddRows(false);
            TableToApply.AddColumns("Ryname", "姓名", 80);
            TableToApply.AddColumns("Zbid", "装备编号", 100);
            TableToApply.AddColumns("ApplyCount", "数量", 40);
            TableToApply.AddColumns("ApplyDate", "申请日期", 170);
            TableToApply.AddColumns("ApplyID", "申请编号", 190);
            TableToApply.AddColumns("FaultReason", "申请原因", 130);
            TableToApply.AddColumns("Status", "同意状态", 80);
            
        }



        //查看更多信息事件函数
        public void LookMore(object sender, RoutedEventArgs e)
        {
            mmiw = new MaintainMoreInfoWindows();
            mmiw.SetValues(TableToApply.Getdt(), TableToApply.dataGrid.SelectedIndex, TableToApply.Rows);
            mmiw.Show();
            mmiw_OpenSign = 1;
        }


        //初始化借阅历史右键菜单
        public void Inittthlbm()
        {
            string Str = "无操作";
            TableToHistory.dgMenu.Items.Add(TableToHistory.AddMenuItem(Str));
        }



        //初始化借阅历史表格
        private void InitTableToHistory()
        {
            TableToHistory.InitTableHeightWidth(210, 810);
            TableToHistory.SetCanUserAddRows(false);
            TableToHistory.AddColumns("RepId", "维修记录号", 100);
            TableToHistory.AddColumns("ZbId", "装备编号", 80);
            TableToHistory.AddColumns("RepairDate", "维修日期", 100);
            TableToHistory.AddColumns("Unit", "维修单位", 140);
            TableToHistory.AddColumns("Cost", "费用", 120);
            TableToHistory.AddColumns("Status", "维修状态", 80);
            TableToHistory.AddColumns("Result", "维修结果", 110);
            TableToHistory.AddColumns("PostDate", "提交日期", 80);
        }


        //申请提交按钮功能
        private void ApplicationSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicationMaintenanceCount.Text.Length == 0 || ApplicationEquipmentNumber.Text.Length == 0)
            {
                MessageBox.Show("错误：装备编号或申请数量为空！");
            }
            else if (Convert.ToInt32(ApplicationMaintenanceCount.Text) < 0)
            {
                MessageBox.Show("错误：申请数量不能负数！");
            }
            else if (ApplicationReasons.Text.Length > 100)
            {
                MessageBox.Show("错误：申请原因字数不能超过100！");
            }
            else if (UITemp.GetRyidStatus)
            {
                if (UITemp.UserInfoTemp.Position == "") UITemp.UserInfoTemp.Position = "信息暂无";
                string Date = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToLongTimeString().ToString(); ;//获取当前时间
                string StrSQL = "insert into ApplyMaintain values('" + UITemp.UserInfoTemp.Ryid + "','" + UITemp.UserInfoTemp.RyName + "','" + UITemp.UserInfoTemp.Position +
                    "','" + Date + "','" + ApplicationEquipmentNumber.Text + "',"
                    + ApplicationMaintenanceCount.Text + ",'" + ApplicationReasons.Text + "','未操作')";
                
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
                MessageBox.Show("申请成功，请耐心等待批准结果。。。");

                TableToApply.DataTableSelect(ApplyTableSql, "更新");//申请成功更新：申请历史表格
                UpDataLog();//更新操作日志
                UpDateRLR(0);//更新左下部操作提示
                ApplicationHistoryCount.Content = TableToApply.Rows;//更新申请总计
                if (mmiw_OpenSign == 1)
                    mmiw.updata(TableToApply.Getdt(), TableToApply.Rows);//更新查看详细信息窗口的总行数
                
            }
            else
            {
                MessageBox.Show("个人信息拉取失败或个人信息未填写充分，无法提交申请!");
            }

}

        //向数据库提交操作日志
        private void UpDataLog()
        {
            UpDataSysLog TempLog = new UpDataSysLog();

            TempLog.SetLogValues(ApplyType, UITemp.UserInfoTemp.RyName + "提交维修申请",
                UITemp.UserInfoTemp.RyName + "于" + DateTime.Now.ToString("yyyy年MM月dd日") + " " + DateTime.Now.ToLongTimeString().ToString()
                + "提交维修申请，申请装备编号为：" + ApplicationEquipmentNumber.Text +
                "；申请装备数量为：" + ApplicationMaintenanceCount.Text, UITemp.UserInfoTemp.Ryid);
        }


        private void UpDateRLR(int Sign)
        {
            if (Sign == 1)
                ApplyRLR.SetValues(UITemp.UserInfoTemp.Ryid, ApplyType);
            ApplyRLR.UpdateShowLables(TableToApply.Getdt());
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

        /*
         * 功能：导出申请历史表格按钮事件
         */
        private void ExportTable_AH_Click(object sender, RoutedEventArgs e)
        {
            string[] Str = { "申请编号" ,"申请人编号", "申请人名字", "工作岗位",  "申请日期", "申请装备编号",
                "申请数量", "故障原因", "操作状态" };
            TableToApply.ExportExcel(ApplyTableSql, Str, "维修申请历史表格.xlsx");
        }
        /*
         * 功能：导出借阅历史表格按钮事件
         */
        private void ExportTable_BH_Click(object sender, RoutedEventArgs e)
        {
            string[] Str = { "维修记录号","送修人编号", "送修人姓名","装备编号", "维修日期", "维修单位", "故障原因",
                "维修状态", "维修费用","维修结果","维修负责人","提交日期" };
            TableToApply.ExportExcel(HistoryTableSql, Str, "维修历史表格.xlsx");
        }
    }
}
