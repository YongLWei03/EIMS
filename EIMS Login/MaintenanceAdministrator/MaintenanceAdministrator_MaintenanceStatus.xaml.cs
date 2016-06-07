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

namespace EIMS_Login
{
    /// <summary>
    /// MaintenanceAdministrator_2.xaml 的交互逻辑
    /// </summary>
    public partial class MaintenanceAdministrator_MaintenanceStatus : UserControl
    {
        public MaintenanceAdministrator_MaintenanceStatus()
        {
            InitializeComponent();
            InitTabelToMaintenance();
        }
        private void InitTabelToMaintenance()
        {
            TableToMaintenance.InitTableHeightWidth(500, 1250);
            TableToMaintenance.SetCanUserAddRows(false);

            TableToMaintenance.AddColumns("MaintenanceNumber", "维修编号", 125);
            TableToMaintenance.AddColumns("EquipmentNumber", "装备编号", 125);
            TableToMaintenance.AddColumns("MaintenanceDate", "维修日期", 80);
            TableToMaintenance.AddColumns("MaintenanceUnit", "维修单位", 120);
            TableToMaintenance.AddColumns("MaintenanceCost", "维修费用", 100);
            TableToMaintenance.AddColumns("MaintenanceResult", "维修结果", 100);
            TableToMaintenance.AddColumns("MaintenancePerson", "维修负责人", 120);
            TableToMaintenance.AddColumns("TroubleReason", "故障原因", 300);
            TableToMaintenance.AddColumns("MaintenanceStatus", "维修状态", 90);
            TableToMaintenance.AddColumns("FilingDate", "提交日期", 90);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
