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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace EIMS_Login.Ordinaryusers
{
    /// <summary>
    /// ApplyDataMoreInfoWindows.xaml 的交互逻辑
    /// </summary>
    public partial class ApplyDataMoreInfoWindows : Window
    {
        int TempRow, Select = 1, TempTotal;
        DataTable Tempdt;
        public ApplyDataMoreInfoWindows()
        {
            InitializeComponent();
        }
        public void Update(DataTable dt,int Rows,int TotalRows)
        {
            if (Select == 1)//零时数据只读取一次！
            {
                Select = 0;
                TempTotal = TotalRows;
                TempRow = Rows;
                Tempdt = dt;
            }
            Ryid.Content = dt.Rows[Rows]["Ryid"].ToString();
            Ryname.Content = dt.Rows[Rows]["Ryname"].ToString();
            Position.Content = dt.Rows[Rows]["Position"].ToString();
            ApplyID.Content = dt.Rows[Rows]["ApplyID"].ToString();
            Applydate.Content = dt.Rows[Rows]["Applydate"].ToString();
            ApplydataID.Content = dt.Rows[Rows]["ApplydataID"].ToString();
            ApplyCount.Content = dt.Rows[Rows]["ApplyCount"].ToString();
            ApplyReason.Content = dt.Rows[Rows]["ApplyReason"].ToString();
            Status.Content = dt.Rows[Rows]["Status"].ToString();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            TempRow--;
            if (TempRow < 0)
            {
                MessageBox.Show("已是第一项，无法在查看上一项！");
                TempRow = 0;
                return;
            }
            else
                Update(Tempdt, TempRow, TempTotal);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            TempRow++;
            if (TempRow > TempTotal-1)
            {
                MessageBox.Show("已是最后一项，无法在查看下一项！");
                TempRow = TempTotal-1;
                return;
            }
            else
                Update(Tempdt, TempRow, TempTotal);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
    }
}
