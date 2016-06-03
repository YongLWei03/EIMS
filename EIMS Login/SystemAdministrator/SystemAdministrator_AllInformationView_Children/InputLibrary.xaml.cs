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
    /// InputLibrary.xaml 的交互逻辑
    /// </summary>
    public partial class InputLibrary : UserControl
    {
        public InputLibrary()
        {
            InitializeComponent();
            InitInputLibraryTable();
        }
        private void InitInputLibraryTable()
        {
            InputLibraryTable.InitTableHeightWidth(420, 880);
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
    }
}
