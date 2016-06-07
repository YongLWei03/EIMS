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
    /// InputLibrary.xaml 的交互逻辑
    /// </summary>
    public partial class InputLibrary : UserControl
    {
        MoreInf Ilmoinf;
        InfTotal infnum = new InfTotal();
        Connection temp = new Connection();
        string[] Str = { "入库编号", "入库类型", "入库装备编号", "生产日期", "入库装备单价", "入库装备数量", "仓库编号", "验收人", "经办人", "入库时间", "备注"};
        public InputLibrary()
        {
            InitializeComponent();
            InitInputLibraryTable();
            InputLibraryNum.Content = infnum.InfTotalSet("StoreIn");
            InputLibraryTable.DataTableSelect("select * from StoreIn", "更新");
            InitRightBm();
        }
        private void InitInputLibraryTable()
        {
            InputLibraryTable.InitTableHeightWidth(420, 898);
            InputLibraryTable.SetCanUserAddRows(false);
            InputLibraryTable.AddColumns("SiId", "入库编号", 100);
            InputLibraryTable.AddColumns("SiType", "入库类型", 100);
            InputLibraryTable.AddColumns("ZbId", "装备编号", 100);
            InputLibraryTable.AddColumns("MakeDate", "生产日期", 80);
            InputLibraryTable.AddColumns("Zbprice", "入库单价", 80);
            InputLibraryTable.AddColumns("Zbnum", "入库数量", 100);
            InputLibraryTable.AddColumns("Ryname1", "仓库编号", 100);
            InputLibraryTable.AddColumns("OptDate", "验收人", 100);
            InputLibraryTable.AddColumns("OptDate", "入库时间", 120);
        }

        private void InputLibraryExport_Click(object sender, RoutedEventArgs e)
        {
            InputLibraryTable.ExportExcel("select * from StoreIn", Str, "入库信息表格.xlsx");
        }
        public void InitRightBm()
        {
            MenuItem InputLibraryMenu = InputLibraryTable.AddMenuItem("更多信息");
            MenuItem DeletRowMenu1 = InputLibraryTable.AddMenuItem("删除选中的行");
            InputLibraryMenu.Click += InputLibraryEventContent;
            DeletRowMenu1.Click += DeletRows1;
            InputLibraryTable.dgMenu.Items.Add(InputLibraryMenu);
            InputLibraryTable.dgMenu.Items.Add(DeletRowMenu1);
        }
        public void InputLibraryEventContent(object sender, RoutedEventArgs e)
        {
            Ilmoinf = new MoreInf();
            string[] Table_Str = { "SiId", "SiType", "ZbId", "MakeDate", "Zbprice", "Zbnum", "Sid", "Ryname1", "Ryname", "OptDate", "Memo" };
            if (InputLibraryTable.dataGrid.SelectedIndex != -1)
            {
                Ilmoinf.SetValues(InputLibraryTable.Getdt(), InputLibraryTable.dataGrid.SelectedIndex, InputLibraryTable.Rows, Table_Str, Str, 11);
                Ilmoinf.Show();
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
                    for (int i = InputLibraryTable.dataGrid.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        cmd.CommandText = "delete from StoreIn where SiId= '" + ((DataRowView)InputLibraryTable.dataGrid.SelectedItems[i]).Row["SiId"].ToString() + "'";
                        cmd.ExecuteNonQuery();
                        ((DataRowView)InputLibraryTable.dataGrid.SelectedItems[i]).Delete();
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
