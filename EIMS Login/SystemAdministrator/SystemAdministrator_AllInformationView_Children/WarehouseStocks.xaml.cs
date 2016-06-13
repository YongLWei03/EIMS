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
    /// WarehouseStocks.xaml 的交互逻辑
    /// </summary>
    public partial class WarehouseStocks : UserControl
    {
        MoreInf Stkinf;
        MoreInf Wareinf;
        InfTotal infnum = new InfTotal();
        Connection temp = new Connection();
        string []Sur = { "库存编号", "装备编号", "装备单价", "装备数量", "生产日期", "仓库编号", "备注" };
        string []HouseStr = {"仓库编号","仓库名称","仓库说明" };
        public WarehouseStocks()
        {
            InitializeComponent();
            InitStocksTable();
            InitWarehouseTable();
            StocksTable.DataTableSelect("select * from ArmsSurplus", "更新");
            ArmsSurplusNum.Content = infnum.InfTotalSet("ArmsSurplus");
            WarehouseTable.DataTableSelect("select * from Storehouse", "更新");
            WarehouseNum.Content = infnum.InfTotalSet("Storehouse");
            InitRightBm();
        }
        private void InitStocksTable()
        {
            StocksTable.InitTableHeightWidth(420, 458);
            StocksTable.SetCanUserAddRows(false);
            StocksTable.AddColumns("SpId", "库存编号", 110);
            StocksTable.AddColumns("ZbId", "装备编号", 110);
            StocksTable.AddColumns("Zbnum", "装备数量", 110);
            StocksTable.AddColumns("Sid", "仓库编号", 110);
        }
        private void InitWarehouseTable()
        {
            WarehouseTable.InitTableHeightWidth(420, 458);
            WarehouseTable.SetCanUserAddRows(false);
            WarehouseTable.AddColumns("Sid", "仓库编号", 110);
            WarehouseTable.AddColumns("Sname", "名称", 110);
            WarehouseTable.AddColumns("Memo", "说明", 220);

        }

        private void EquipmentExport_left_Click(object sender, RoutedEventArgs e)
        {
            StocksTable.ExportExcel("select * from ArmsSurplus", Sur, "库存信息表格.xlsx");
        }

        private void EquipmentExport_right_Click(object sender, RoutedEventArgs e)
        {
            WarehouseTable.ExportExcel("select * from Storehouse", Sur, "仓库信息表格.xlsx");
        }
        public void InitRightBm()
        {
            MenuItem StocksMenu = StocksTable.AddMenuItem("更多信息");
            MenuItem WarehouseMenu = WarehouseTable.AddMenuItem("更多信息");
            MenuItem DeletRowMenu1 = StocksTable.AddMenuItem("删除选中的行");
            MenuItem DeletRowMenu2 = WarehouseTable.AddMenuItem("删除选中的行");

            StocksMenu.Click += StocksEventContent;
            WarehouseMenu.Click += WarehouseEventContent;
            DeletRowMenu1.Click += DeletRows1;
            DeletRowMenu2.Click += DeletRows2;
            StocksTable.dgMenu.Items.Add(StocksMenu);
            StocksTable.dgMenu.Items.Add(DeletRowMenu1);
            WarehouseTable.dgMenu.Items.Add(WarehouseMenu);
            WarehouseTable.dgMenu.Items.Add(DeletRowMenu2);
        }

        public void StocksEventContent(object sender, RoutedEventArgs e)
        {
            Stkinf = new MoreInf();
            string[] Table_Str = { "SpId", "ZbId", "Zbprice", "Zbnum", "MakeDate", "Sid", "Memo" };
            if (StocksTable.dataGrid.SelectedIndex != -1)
            {
                Stkinf.SetValues(StocksTable.Getdt(), StocksTable.dataGrid.SelectedIndex, StocksTable.Rows, Table_Str, Sur, 7);
                Stkinf.Show();
            }
            else MessageBox.Show("当前未选中任何行！");    
        }
        public void WarehouseEventContent(object sender, RoutedEventArgs e)
        {
            Wareinf = new MoreInf();
            string[] Table_Str = { "Sid", "Sname", "Memo" };
            if (WarehouseTable.dataGrid.SelectedIndex != -1)
            {
                Wareinf.SetValues(WarehouseTable.Getdt(), WarehouseTable.dataGrid.SelectedIndex, WarehouseTable.Rows, Table_Str, HouseStr, 3);
                Wareinf.Show();
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
                    for (int i = StocksTable.dataGrid.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        cmd.CommandText = "delete from ArmsSurplus where SpId= '" + ((DataRowView)StocksTable.dataGrid.SelectedItems[i]).Row["SpId"].ToString() + "'";
                        cmd.ExecuteNonQuery();
                        ((DataRowView)StocksTable.dataGrid.SelectedItems[i]).Delete();
                    }
                }
                catch (Exception ae)
                {
                    MessageBox.Show("删除失败！");
                }
            }

        }
        public void DeletRows2(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = temp.GetConn();
            if (MessageBox.Show("您确定要删除吗？", "系统提示：", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                try
                {
                    for (int i = WarehouseTable.dataGrid.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        cmd.CommandText = "delete from Storehouse where Sid= '" + ((DataRowView)WarehouseTable.dataGrid.SelectedItems[i]).Row["Sid"].ToString() + "'";
                        cmd.ExecuteNonQuery();
                        ((DataRowView)WarehouseTable.dataGrid.SelectedItems[i]).Delete();
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
