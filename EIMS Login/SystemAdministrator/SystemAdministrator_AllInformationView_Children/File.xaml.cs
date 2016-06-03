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
    /// File.xaml 的交互逻辑
    /// </summary>
    public partial class File : UserControl
    {
        public File()
        {
            InitializeComponent();
            InitFileTable();
        }
        private void InitFileTable()
        {
            FileTable.InitTableHeightWidth(420, 880);
            FileTable.SetCanUserAddRows(false);
            FileTable.AddColumns("Id", "归档号", 100);
            FileTable.AddColumns("DataNo", "资料编号", 150);
            FileTable.AddColumns("InCount", "归档数量", 120);
            FileTable.AddColumns("Username", "经办人", 95);
            FileTable.AddColumns("Username", "审批人", 95);
            FileTable.AddColumns("Ryname", "归档日期", 110);
            FileTable.AddColumns("Detail", "审批状态", 80);
            FileTable.AddColumns("Flag", "备注", 130);
        }
    }
}
