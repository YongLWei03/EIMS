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

namespace EIMS_Login.SystemAdministrator.SystemAdministrator_AllInformationView_Children
{
    /// <summary>
    /// Maintenance.xaml 的交互逻辑
    /// </summary>
    public partial class Maintenance : UserControl
    {
        MoreInf Mamoinf;
        InfTotal infnum = new InfTotal();
        Connection temp = new Connection();
        string[] Str = { "记录编号", "申请人员编号", "送修人编号", "装备编号", "维修日期", "维修单位", "故障原因", "维修状态", "维修费用", "维修结果", "维修负责人", "提交日期"};
        public Maintenance()
        {
            InitializeComponent();
            InitMaintenanceTable();
            MaintenanceNum_right.Content = infnum.InfTotalSet("ArmsRepair");
            MaintenanceTable.DataTableSelect(MaintenanceStatus(0), "更新");
            InitRightBm();
        }
        private void InitMaintenanceTable()
        {
            MaintenanceTable.InitTableHeightWidth(420, 898);
            MaintenanceTable.SetCanUserAddRows(false);
            MaintenanceTable.AddColumns("RepId", "维修编号", 80);
            MaintenanceTable.AddColumns("RyId", "申请人员编号", 120);
            MaintenanceTable.AddColumns("ZbId", "装备编号", 80);
            MaintenanceTable.AddColumns("Unit", "维修单位", 80);
            MaintenanceTable.AddColumns("Result", "维修结果", 80);
            MaintenanceTable.AddColumns("Cost", "费用", 80);
            MaintenanceTable.AddColumns("PostDate", "提交日期", 140);
            MaintenanceTable.AddColumns("RepairDate", "维修日期", 140);
            MaintenanceTable.AddColumns("Status", "维修状态", 80);
        }
        private string MaintenanceStatus(int SelectStatus)
        {
            string BeenRepair = "select * from ArmsRepair where Status = '已送修'";
            string RepairDone = "select * from ArmsRepair where Status = '维修完毕'";

            switch (SelectStatus)
            {
                case 0:

                    return BeenRepair;
                case 1:

                    return RepairDone;
            }
            return null;
        }

        private void Status_DropDownClosed(object sender, EventArgs e)
        {
            MaintenanceTable.DataTableSelect(MaintenanceStatus(Status.SelectedIndex), "更新");
        }

        private void EquipmentExport_right_Click(object sender, RoutedEventArgs e)
        {
            MaintenanceTable.ExportExcel(MaintenanceStatus(Status.SelectedIndex), Str, "维修状态表格.xlsx"); 
        }
        public void InitRightBm()
        {
            MenuItem MaintenanceMenu = MaintenanceTable.AddMenuItem("更多信息");
            MenuItem DeletRowMenu1 = MaintenanceTable.AddMenuItem("删除选中的行");
            MaintenanceMenu.Click += MaintenanceEventContent;
            DeletRowMenu1.Click += DeletRows1;
            MaintenanceTable.dgMenu.Items.Add(MaintenanceMenu);
            MaintenanceTable.dgMenu.Items.Add(DeletRowMenu1);
        }
        public void MaintenanceEventContent(object sender, RoutedEventArgs e)
        {
            Mamoinf = new MoreInf();
            string[] Table_Str = { "RepId", "RyId", "Ryname1", "ZbId", "RepairDate", "Unit", "Reason", "Status", "Cost", "Result", "Ryname", "PostDate" };
            if (MaintenanceTable.dataGrid.SelectedIndex != -1)
            {
                Mamoinf.SetValues(MaintenanceTable.Getdt(), MaintenanceTable.dataGrid.SelectedIndex, MaintenanceTable.Rows, Table_Str, Str, 12);
                Mamoinf.Show();
            }
            else MessageBox.Show("当前未选中任何行！");           
        }
        public void DeletRows1(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = temp.GetConn();
            if (MessageBox.Show("您确定要删除吗？", "系统提示：", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                try
                {
                    for (int i = MaintenanceTable.dataGrid.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        cmd.CommandText = "delete from ArmsRepair where RepId= '" + ((DataRowView)MaintenanceTable.dataGrid.SelectedItems[i]).Row["RepId"].ToString() + "'";
                        cmd.ExecuteNonQuery();
                        ((DataRowView)MaintenanceTable.dataGrid.SelectedItems[i]).Delete();
                    }
                }
                catch (Exception ae)
                {
                    MessageBox.Show("删除失败！");
                }
            }

        }
    }
}
