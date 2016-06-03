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
    /// ConfidentialMembers_2.xaml 的交互逻辑
    /// </summary>
    public partial class ConfidentialMembers_Borrowinfo : UserControl
    {
        public ConfidentialMembers_Borrowinfo()
        {
            InitializeComponent();
            InitTable();
        }
        public void InitTable()
        {
            TableToBorrow.InitTableHeightWidth(480, 880);
            TableToBorrow.AddColumns("Id", "借阅号", 120);
            TableToBorrow.AddColumns("DateNo", "资料编号", 120);
            TableToBorrow.AddColumns("LendDate", "借阅日期", 120);
            TableToBorrow.AddColumns("Ryid", "借阅人编号", 120);
            TableToBorrow.AddColumns("LendCount", "借阅数量",120 );
            TableToBorrow.AddColumns("Ryname", "审批人",120 );
            TableToBorrow.AddColumns("Detail", "借阅状态",120 );
           

        }
    }
}
