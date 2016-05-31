﻿using System;
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
using EIMS_Login.Ordinaryusers;//用户信息命名空间申明

namespace EIMS_Login.Ordinaryusers
{
    /// <summary>
    /// OrdinaryUsers_EquipmentApplication.xaml 的交互逻辑
    /// </summary>
    public partial class OrdinaryUsers_EquipmentApplication : UserControl
    {
        Connection Temp = new Connection();
        OrdinaryUserInfo UITemp = new OrdinaryUserInfo();
        string ApplyTableSql = "select * from ApplyEquip where Ryid='" + MainWindow.CurrentUser + "'";
        string HistoryTableSql = "select * from ArmsAllo where RyId='" + MainWindow.CurrentUser + "'";
        string ApplyType = "zb_sq";
        string AType;
        ApplyEquipMoreInfoWindows aemiw;//详细信息窗口
        int aemiw_OpenSign = 0;//详细信息窗口标记，0为未打开过！



        public OrdinaryUsers_EquipmentApplication()
        {
            InitializeComponent();
            InitTabelToApply();//申请历史表格初始化 
            InitTableToHistory();//借阅历史表格初始化
            TableToApply.DataTableSelect(ApplyTableSql, "更新");
            Initttalbm();
            ApplicationHistoryCount.Content = TableToApply.Rows;//申请总计
            TableToHistory.DataTableSelect(HistoryTableSql, "更新");
            Inittthlbm();
            PayoutHistoryCount.Content = TableToHistory.Rows;//借阅历史总计
            UpDateRLR(1);//更新左下侧操作历史
        }
        //初始化申请历史表格
        private void InitTabelToApply()
        {
            TableToApply.InitTableHeightWidth(210, 810);
            TableToApply.AddColumns("Ryname", "姓名", 80);
            TableToApply.AddColumns("Zbid", "装备编号", 100);
            TableToApply.AddColumns("ApplyCount", "数量", 40);
            TableToApply.AddColumns("ApplyDate", "申请日期", 170);
            TableToApply.AddColumns("ApplyID", "申请编号", 190);
            TableToApply.AddColumns("Status", "同意状态", 80);
            TableToApply.AddColumns("ApplyReason", "申请原因", 130);
        }
        public void Initttalbm()
        {
            MenuItem TempMenu = TableToApply.AddMenuItem("查看更多");
            TempMenu.Click += LookMore;
            TableToApply.dgMenu.Items.Add(TempMenu);
        }
        public void LookMore(object sender, RoutedEventArgs e)//待修改
        {
            aemiw = new ApplyEquipMoreInfoWindows();
            aemiw.SetValues(TableToApply.Getdt(), TableToApply.dataGrid.SelectedIndex, TableToApply.Rows);
            aemiw.Show();
            aemiw_OpenSign = 1;
        }
        public void Inittthlbm()//待修改
        {
            string Str = "无操作";
            TableToHistory.dgMenu.Items.Add(TableToHistory.AddMenuItem(Str));
        }
        //初始化借阅历史表格
        private void InitTableToHistory()
        {
            TableToHistory.InitTableHeightWidth(210, 810);
            TableToHistory.AddColumns("Zbid", "装备编号", 80);
            TableToHistory.AddColumns("ANum", "数量", 100);
            TableToHistory.AddColumns("Zbprice", "单价", 220);
            TableToHistory.AddColumns("InDep", "调入单位", 120);
            TableToHistory.AddColumns("Atype", "类型", 80);
            TableToHistory.AddColumns("Person", "提货人", 110);
            TableToHistory.AddColumns("ADate", "日期", 80);
        }

        private void ApplicationSubmit_Click(object sender, RoutedEventArgs e)//待修改
        {
            string Date = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToLongTimeString().ToString(); ;//获取当前时间
            
            if (AllotType.SelectedIndex == 0)
                AType = "价拨";
            else
                AType = "调拨";
            string StrSQL = "insert into ApplyEquip values('" + UITemp.UserInfoTemp.Ryid + "','" + UITemp.UserInfoTemp.RyName + "','" + UITemp.UserInfoTemp.Position +  "','" + Date + "','" + ApplicationEquipmentNumber.Text + "',"
                + ApplicationEquipmentCount.Text + ",'"+ AType + "','"+ TransferredUnit.Text + "','"+ ApplicationReasons .Text+ "','未操作')";
            try
            {
                SqlCommand cmd = new SqlCommand(StrSQL, Temp.GetConn());
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show("申请失败！"+ex);
                return;
            }
            MessageBox.Show("申请成功，请耐心等待批准结果。。。");

            TableToApply.DataTableSelect(ApplyTableSql, "更新");//申请成功更新：申请历史表格
            UpDataLog();//更新操作日志
            UpDateRLR(0);//更新左下部操作提示
            ApplicationHistoryCount.Content = TableToApply.Rows;//更新申请总计
            if (aemiw_OpenSign == 1)
                aemiw.updata(TableToApply.Getdt(), TableToApply.Rows);//更新查看详细信息窗口的总行数
        }

        private void UpDataLog()
        {
            UpDataSysLog TempLog = new UpDataSysLog();

            TempLog.SetLogValues(ApplyType, UITemp.UserInfoTemp.RyName + "提交装备申请",
                UITemp.UserInfoTemp.RyName + "于" + DateTime.Now.ToString("yyyy年MM月dd日") + " " + DateTime.Now.ToLongTimeString().ToString()
                + "提交装备申请，申请的装备编号为：" + ApplicationEquipmentNumber.Text + "；申请资料数量为：" + ApplicationEquipmentCount.Text + "；申请调拨方案为：" + AType, UITemp.UserInfoTemp.Ryid
                );
        }


        private void UpDateRLR(int Sign)
        {
            if (Sign == 1)
                ApplyRLR.SetValues(UITemp.UserInfoTemp.Ryid, ApplyType);
            ApplyRLR.UpdateShowLables(TableToApply.Getdt());
        }


        /*
         * 功能：导出申请历史表格按钮事件
         */
        private void ExportTable_AH_Click(object sender, RoutedEventArgs e)
        {
            string[] Str = { "ID", "申请人编号", "申请人名字", "工作岗位", "申请编号", "申请日期", "申请装备编号",
                "申请数量","调拨类型","调入单位", "申请原因", "操作状态" };
            TableToApply.ExportExcel(ApplyTableSql, Str, "装别申请历史表格.xlsx");
        }
        /*
         * 功能：导出借阅历史表格按钮事件
         */
        private void ExportTable_BH_Click(object sender, RoutedEventArgs e)
        {
            string[] Str = { "调拨单号","单号对应申请人编号", "装备编号", "数量", "单价", "调出单位", "调入单位", "调拨类型","提货人",
                "有效时间", "批准人", "承办人", "日期", "备注" };
            TableToApply.ExportExcel(HistoryTableSql, Str, "装别调拨历史表格.xlsx");
        }

        private void ApplicationEquipmentCount_KeyDown(object sender, KeyEventArgs e)
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
    }
}
