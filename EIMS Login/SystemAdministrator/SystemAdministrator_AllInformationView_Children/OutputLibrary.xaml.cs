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

namespace EIMS_Login.SystemAdministrator.SystemAdministrator_AllInformationView_Children
{
    /// <summary>
    /// OutputLibrary.xaml 的交互逻辑
    /// </summary>
    public partial class OutputLibrary : UserControl
    {
        MoreInf Olmoinf;
        InfTotal infnum = new InfTotal();
        string[] Str = { "出库编号", "出库类型", "出库装备编号", "出库装备单价", "出库装备数量", "仓库编号", "批准人", "经办人", "出库时间", "备注"};
        public OutputLibrary()
        {
            InitializeComponent();
            InitOutputLibraryTable();
            OutputLibraryNum.Content = infnum.InfTotalSet("Takeout");
            OutputLibraryTable.DataTableSelect("select * from Takeout", "更新");
            InitRightBm();
        }
        private void InitOutputLibraryTable()
        {
            OutputLibraryTable.InitTableHeightWidth(420, 898);
            OutputLibraryTable.SetCanUserAddRows(false);
            OutputLibraryTable.AddColumns("ToId", "出库编号", 100);
            OutputLibraryTable.AddColumns("Ttype", "出库类型", 100);
            OutputLibraryTable.AddColumns("ZbId", "装备编号", 100);
            OutputLibraryTable.AddColumns("Zbprice", "出库单价", 80);
            OutputLibraryTable.AddColumns("Zbnum", "出库数量", 80);
            OutputLibraryTable.AddColumns("Sid", "仓库编号", 100);
            OutputLibraryTable.AddColumns("Ryname1", "批准人", 100);
            OutputLibraryTable.AddColumns("Ryname", "经办人", 100);
            OutputLibraryTable.AddColumns("OptDate", "出库时间", 120);
        }

        private void OutputLibraryExport_Click(object sender, RoutedEventArgs e)
        {
            OutputLibraryTable.ExportExcel("select * from Takeout", Str, "出库信息表格.xlsx");
        }
        public void InitRightBm()
        {
            MenuItem OutputLibraryMenu = OutputLibraryTable.AddMenuItem("更多信息");
            OutputLibraryMenu.Click += MaintenanceEventContent;
            OutputLibraryTable.dgMenu.Items.Add(OutputLibraryMenu);
        }
        public void MaintenanceEventContent(object sender, RoutedEventArgs e)
        {
            Olmoinf = new MoreInf();
            string[] Table_Str = { "ToId", "Ttype", "ZbId", "Zbprice", "Zbnum", "Sid", "Ryname1", "Ryname", "OptDate", "Memo" };
            if (OutputLibraryTable.dataGrid.SelectedIndex != -1)
            {
                Olmoinf.SetValues(OutputLibraryTable.Getdt(), OutputLibraryTable.dataGrid.SelectedIndex, OutputLibraryTable.Rows, Table_Str, Str, 10);
                Olmoinf.Show();
            }
            else MessageBox.Show("当前未选中任何行！");
        }
    }
}
