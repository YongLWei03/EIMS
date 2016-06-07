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
using System.Data;
using System.Data.SqlClient;

namespace EIMS_Login.SystemAdministrator.SystemAdministrator_AllInformationView_Children
{
    /// <summary>
    /// Borrow.xaml 的交互逻辑
    /// </summary>
    public partial class Borrow : UserControl
    {
        MoreInf Bormoinf;
        InfTotal infnum = new InfTotal();
        Connection temp = new Connection();
        string[] Str = { "借阅号", "资料编号", "借阅日期", "借阅人编号", "借阅数量", "批准人", "标记" };
        public Borrow()
        {
            InitializeComponent();
            InitBorrowTable();
            BorrowNum.Content = infnum.InfTotalSet("DataLend");
            BorrowTable.DataTableSelect("select * from DataLend", "更新");
            InitRightBm();
        }
        private void InitBorrowTable()
        {
            BorrowTable.InitTableHeightWidth(420, 898);
            BorrowTable.SetCanUserAddRows(false);
            BorrowTable.AddColumns("Id", "借阅号", 120);            
            BorrowTable.AddColumns("RyId", "借阅人编号", 150);
            BorrowTable.AddColumns("DataNo", "资料编号", 150);
            BorrowTable.AddColumns("LendCount", "数量", 60);
            BorrowTable.AddColumns("LendDate", "借阅日期", 160);
            BorrowTable.AddColumns("Ryname", "批准人姓名", 110);
            BorrowTable.AddColumns("Flag", "借阅状态", 130);
        }

        private void BorrowExport_Click(object sender, RoutedEventArgs e)
        {
            BorrowTable.ExportExcel("select * from DataLend", Str, "借阅信息表格.xlsx");
        }
        public void InitRightBm()
        {
            MenuItem BorrowMenu = BorrowTable.AddMenuItem("更多信息");
            MenuItem DeletRowMenu1 = BorrowTable.AddMenuItem("删除选中的行");
            BorrowMenu.Click += BorrowEventContent;
            DeletRowMenu1.Click += DeletRows1;
            BorrowTable.dgMenu.Items.Add(BorrowMenu);
            BorrowTable.dgMenu.Items.Add(DeletRowMenu1);
        }
        public void BorrowEventContent(object sender, RoutedEventArgs e)
        {
            Bormoinf = new MoreInf();
            string[] Table_Str = { "Id", "DataNo", "LendDate", "RyId", "LendCount", "Ryname", "Flag" };
            if (BorrowTable.dataGrid.SelectedIndex != -1)
            {
                Bormoinf.SetValues(BorrowTable.Getdt(), BorrowTable.dataGrid.SelectedIndex, BorrowTable.Rows, Table_Str, Str, 7);
                Bormoinf.Show();
            }
            else MessageBox.Show("当前未选中任何行！");
        }
        public void DeletRows1(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = temp.GetConn();
            if (MessageBox.Show("您确定要删除吗？", "系统提示：", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                try
                {
                    for (int i = BorrowTable.dataGrid.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        cmd.CommandText = "delete from DataLend where Id= '" + ((DataRowView)BorrowTable.dataGrid.SelectedItems[i]).Row["Id"].ToString() + "'";
                        cmd.ExecuteNonQuery();
                        ((DataRowView)BorrowTable.dataGrid.SelectedItems[i]).Delete();
                    }
                }
                catch (Exception ae)
                {
                    MessageBox.Show("删除失败！");
                }
            }

        }
    }
}
