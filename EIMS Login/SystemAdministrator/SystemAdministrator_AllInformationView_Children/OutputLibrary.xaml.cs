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
        public OutputLibrary()
        {
            InitializeComponent();
            InitOutputLibraryTable();
        }
        private void InitOutputLibraryTable()
        {
            OutputLibraryTable.InitTableHeightWidth(420, 880);
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
    }
}
