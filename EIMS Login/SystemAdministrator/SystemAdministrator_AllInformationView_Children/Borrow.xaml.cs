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
    /// Borrow.xaml 的交互逻辑
    /// </summary>
    public partial class Borrow : UserControl
    {
        public Borrow()
        {
            InitializeComponent();
            InitBorrowTable();
        }
        private void InitBorrowTable()
        {
            BorrowTable.InitTableHeightWidth(420, 880);
            BorrowTable.SetCanUserAddRows(false);
            BorrowTable.AddColumns("Id", "借阅号", 120);
            BorrowTable.AddColumns("Ryname", "借阅人姓名", 110);
            BorrowTable.AddColumns("RyId", "借阅人编号", 150);
            BorrowTable.AddColumns("DataNo", "资料编号", 150);
            BorrowTable.AddColumns("LendCount", "数量", 110);
            BorrowTable.AddColumns("LendDate", "借阅日期", 110);
            //BorrowTable.AddColumns("Flag", "归还日期", 280);
            BorrowTable.AddColumns("Flag", "借阅状态", 130);
        }
    }
}
