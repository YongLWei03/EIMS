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
    /// WarehouseStocks.xaml 的交互逻辑
    /// </summary>
    public partial class WarehouseStocks : UserControl
    {
        public WarehouseStocks()
        {
            InitializeComponent();
            InitStocksTable();
            InitWarehouseTable();
        }
        private void InitStocksTable()
        {
            StocksTable.InitTableHeightWidth(420, 440);
            StocksTable.SetCanUserAddRows(false);
            StocksTable.AddColumns("SpId", "库存编号", 110);
            StocksTable.AddColumns("ZbId", "装备编号", 110);
            StocksTable.AddColumns("Zbnum", "装备数量", 110);
            StocksTable.AddColumns("Sid", "仓库编号", 110);
        }
        private void InitWarehouseTable()
        {
            WarehouseTable.InitTableHeightWidth(420, 440);
            WarehouseTable.SetCanUserAddRows(false);
            WarehouseTable.AddColumns("Sid", "仓库编号", 110);
            WarehouseTable.AddColumns("Sname", "名称", 110);
            WarehouseTable.AddColumns("Memo", "说明", 220);

        }
    }
}
