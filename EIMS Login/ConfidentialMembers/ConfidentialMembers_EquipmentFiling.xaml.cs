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
    /// ConfidentialMembers_3.xaml 的交互逻辑
    /// </summary>
    public partial class ConfidentialMembers_NewEquipmentInformationAdded : UserControl
    {
        public ConfidentialMembers_NewEquipmentInformationAdded()
        {
            InitializeComponent();
            InitTable();
        }

        public void InitTable()
        {
            TableToDataFile.InitTableHeightWidth(480,880);
            TableToDataFile.AddColumns("Id","归档号",80);
            TableToDataFile.AddColumns("InDate", "归档日期", 80);
            TableToDataFile.AddColumns("DateNo", "资料编号", 80);
            TableToDataFile.AddColumns("InCount", "归档数量", 80);
            TableToDataFile.AddColumns("Usersname", "经办人", 80);
            TableToDataFile.AddColumns("Ryname", "审批人", 80);
            TableToDataFile.AddColumns("Detail", "说明", 300);
            TableToDataFile.AddColumns("Flag", "审核标记", 75);

        }
    }
}
