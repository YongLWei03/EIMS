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
    /// Allocation.xaml 的交互逻辑
    /// </summary>
    public partial class Allocation : UserControl
    {
        public Allocation()
        {
            InitializeComponent();
            InitAllocationTable();
        }
        private void InitAllocationTable()
        {
            AllocationTable.InitTableHeightWidth(420, 880);
            AllocationTable.SetCanUserAddRows(false);
            AllocationTable.AddColumns("Id", "调拨单号", 110);
            AllocationTable.AddColumns("Person", "提货人", 110);
            AllocationTable.AddColumns("InDep", "调入单位", 110);
            AllocationTable.AddColumns("ADate", "完成日期", 80);
            AllocationTable.AddColumns("ZbId", "调出装备编号", 110);
            AllocationTable.AddColumns("AType", "调拨类型", 80);
            AllocationTable.AddColumns("Reason", "备注", 280);
        }
    }
}
