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
        int AimRow, TotalRows;
        DataTable Tempdt;
        /*
         * 窗口默认构造函数
         */
        public ApplyDataMoreInfoWindows()
        {
            InitializeComponent();
        }
        /*
         * 窗口构造函数
         * 参数：dt 数据源
         *       Rows 显示的目标行数
         *       tr 数据源总行数
         */
        public void SetValues(DataTable dt, int Rows, int tr)
        {
            TotalRows = tr;
            AimRow = Rows;
            if (dt == null)
            {
                MessageBox.Show("数据源传入错误！");
            }
            else
            {
                Tempdt = dt;
                UpdateShow(Tempdt, AimRow);
            }
        }
        /*
         * 功能：更新窗口的显示
         * 参数：dt 数据源
         *       Rows 更新显示的目标行数
         */
        private void UpdateShow(DataTable dt, int Rows)
        {
            Ryid.Content = dt.Rows[Rows]["Ryid"].ToString();
            Ryname.Content = dt.Rows[Rows]["Ryname"].ToString();
            Position.Content = dt.Rows[Rows]["Position"].ToString();
            ApplyID.Content = dt.Rows[Rows]["ApplyID"].ToString();
            Applydate.Content = dt.Rows[Rows]["Applydate"].ToString();
            ApplydataID.Content = dt.Rows[Rows]["ApplydataID"].ToString();
            ApplyCount.Content = dt.Rows[Rows]["ApplyCount"].ToString();
            ApplyReason.Text = dt.Rows[Rows]["ApplyReason"].ToString();
            Status.Content = dt.Rows[Rows]["Status"].ToString();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            AimRow--;
            if (AimRow < 0)
            {
                MessageBox.Show("已是第一项，无法在查看上一项！");
                AimRow = 0;
                return;
            }
            else
                UpdateShow(Tempdt, AimRow);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            AimRow++;
            if (AimRow > TotalRows-1)
            {
                MessageBox.Show("已是最后一项，无法在查看下一项！");
                AimRow = TotalRows - 1;
                return;
            }
            else
                UpdateShow(Tempdt, AimRow);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void updata(DataTable dt, int tr)
        {
            Tempdt = dt;
            TotalRows = tr;
            AimRow++;
            UpdateShow(Tempdt, AimRow);
        }
    }
}
