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
    /// Maintenance.xaml 的交互逻辑
    /// </summary>
    public partial class Maintenance : UserControl
    {
        public Maintenance()
        {
            InitializeComponent();
            InitMaintenanceTable();
        }
        private void InitMaintenanceTable()
        {
            MaintenanceTable.InitTableHeightWidth(420, 880);
            MaintenanceTable.SetCanUserAddRows(false);
            MaintenanceTable.AddColumns("RepId", "维修编号", 80);
            MaintenanceTable.AddColumns("RyId", "申请人员编号", 120);
            MaintenanceTable.AddColumns("ZbId", "装备编号", 80);
            MaintenanceTable.AddColumns("RepairDate", "维修日期", 80);
            MaintenanceTable.AddColumns("Unit", "维修单位", 80);
            MaintenanceTable.AddColumns("Result", "维修结果", 80);
            MaintenanceTable.AddColumns("Reason", "故障原因", 280);
            MaintenanceTable.AddColumns("Status", "维修状态", 80);
        }
    }
}
