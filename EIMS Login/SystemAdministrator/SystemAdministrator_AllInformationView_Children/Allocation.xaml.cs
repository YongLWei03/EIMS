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
    /// Allocation.xaml 的交互逻辑
    /// </summary>
    public partial class Allocation : UserControl
    {
        MoreInf Allomof;
        InfTotal infnum = new InfTotal();
        Connection temp = new Connection();
        string[] Str = { "调拨单号", "调拨人员编号", "装备编号", "数量", "装备单价", "调出单位", "调入单位", "调拨类型", "提货人", "有效期(月)", "批准人", "承办人", "批准日期", "备注", "状态"};
        public Allocation()
        {
            InitializeComponent();
            InitAllocationTable();
            AllocationNum.Content = infnum.InfTotalSet("ArmsAllo");
            AllocationTable.DataTableSelect(AllocationStatus(0) , "更新");
            InitRightBm();
        }
        private void InitAllocationTable()
        {
            AllocationTable.InitTableHeightWidth(420, 898);
            AllocationTable.SetCanUserAddRows(false);
            AllocationTable.AddColumns("Id", "调拨单号", 110);
            AllocationTable.AddColumns("Person", "提货人", 110);
            AllocationTable.AddColumns("InDep", "调入单位", 110);
            AllocationTable.AddColumns("ADate", "批准日期", 80);
            AllocationTable.AddColumns("ZbId", "调出装备编号", 110);
            AllocationTable.AddColumns("AType", "调拨类型", 80);
            AllocationTable.AddColumns("Reason", "备注", 280);
        }
        private string AllocationStatus(int SelectStatus)
        {
            string BeenRepair = "select * from ArmsAllo where Status = '未完成'";
            string RepairDone = "select * from ArmsAllo where Status = '已完成'";

            switch (SelectStatus)
            {
                case 0:

                    return BeenRepair;
                case 1:

                    return RepairDone;
            }
            return null;
        }
        private void AllocationExport_Click(object sender, RoutedEventArgs e)
        {
            AllocationTable.ExportExcel("select * from ArmsAllo", Str, "调拨信息表格.xlsx"); 
        }
        public void InitRightBm()
        {
            MenuItem AllocationMenu = AllocationTable.AddMenuItem("更多信息");
            MenuItem DeletRowMenu1 = AllocationTable.AddMenuItem("删除选中的行");
            AllocationMenu.Click += AllocationEventContent;
            DeletRowMenu1.Click += DeletRows1;
            AllocationTable.dgMenu.Items.Add(AllocationMenu);
            AllocationTable.dgMenu.Items.Add(DeletRowMenu1);
        }
        public void AllocationEventContent(object sender, RoutedEventArgs e)
        {
            Allomof = new MoreInf();
            string[] Table_Str = { "Id", "RyId", "ZbId", "ANum", "Zbprice", "OutDep", "InDep", "AType", "Person", "UsefulDate", "Ryname1", "Ryname", "ADate", "Memo", "Status" };
            if (AllocationTable.dataGrid.SelectedIndex != -1)
            {
                Allomof.SetValues(AllocationTable.Getdt(), AllocationTable.dataGrid.SelectedIndex, AllocationTable.Rows, Table_Str, Str, 15);
                Allomof.Show();
            }
            else MessageBox.Show("当前未选中任何行！");
        }

        private void Status_DropDownClosed(object sender, EventArgs e)
        {
            AllocationTable.DataTableSelect(AllocationStatus(Status.SelectedIndex), "更新");
        }
        public void DeletRows1(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = temp.GetConn();
            if (MessageBox.Show("您确定要删除吗？", "系统提示：", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                try
                {
                    for (int i = AllocationTable.dataGrid.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        cmd.CommandText = "delete from ArmsAllo where Id= '" + ((DataRowView)AllocationTable.dataGrid.SelectedItems[i]).Row["Id"].ToString() + "'";
                        cmd.ExecuteNonQuery();
                        ((DataRowView)AllocationTable.dataGrid.SelectedItems[i]).Delete();
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
