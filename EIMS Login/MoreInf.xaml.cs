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

namespace EIMS_Login
{
    /// <summary>
    /// MoreInf.xaml 的交互逻辑
    /// </summary>
    public partial class MoreInf : Window
    {
        int AimRow, TotalRows;
        string[] ziduan;
        DataTable Tempdt;
        int Countziduan;
        public MoreInf()
        {
            InitializeComponent();
        }

        public void SetValues(DataTable dt,int Rows,int tr,string[] ziduan,string[] str,int Countziduan)
        {
            TotalRows = tr;
            AimRow = Rows;
            this.ziduan = ziduan;
            this.Countziduan = Countziduan;
            if(dt == null)
            {
                MessageBox.Show("数据源传入错误！");
            }
            else
            {
                Tempdt = dt;
                Setziduan(str, Countziduan);
                UpdateShow(Tempdt, AimRow, ziduan, Countziduan);
            }
        }
        public void Setziduan(string[] ziduan, int Countziduan)
        {
            if (1 <= Countziduan)
            {
                label1.Content = ziduan[0] + ":";
            }
            if (2 <= Countziduan)
            {
                label2.Content = ziduan[1] + ":";
            }
            if (3 <= Countziduan)
            {
                label3.Content = ziduan[2] + ":";
            }
            if (4 <= Countziduan)
            {
                label4.Content = ziduan[3] + ":";
            }
            if (5 <= Countziduan)
            {
                label5.Content = ziduan[4] + ":";
            }
            if (6 <= Countziduan)
            {
                label6.Content = ziduan[5] + ":";
            }
            if (7 <= Countziduan)
            {
                label7.Content = ziduan[6] + ":";
            }
            if (8 <= Countziduan)
            {
                label8.Content = ziduan[7] + ":";
            }
            if (9 <= Countziduan)
            {
                label9.Content = ziduan[8] + ":";
            }
            if (10 <= Countziduan)
            {
                label10.Content = ziduan[9] + ":";
            }
            if (11 <= Countziduan)
            {
                label11.Content = ziduan[10] + ":";
            }
            if (12 <= Countziduan)
            {
                label12.Content = ziduan[11] + ":";
            }
            if (13 <= Countziduan)
            {
                label13.Content = ziduan[12] + ":";
            }
            if (14 <= Countziduan)
            {
                label14.Content = ziduan[13] + ":";
            }
            if (15 <= Countziduan)
            {
                label15.Content = ziduan[14] + ":";
            }

        }
        public void UpdateShow(DataTable dt, int Rows, string[] ziduan, int Countziduan)
        {
            //int i = 0;
            //for (; i < Countziduan; i++)
            //{
            //    La[i].Content = dt.Rows[Rows][ziduan[i]].ToString();
            //}
            //i = 0;
            if (1 <= Countziduan)
            {
                label1_text.Text = dt.Rows[Rows][ziduan[0]].ToString();
            }
            if (2 <= Countziduan)
            {
                label2_text.Text = dt.Rows[Rows][ziduan[1]].ToString();
            }
            if (3 <= Countziduan)
            {
                label3_text.Text = dt.Rows[Rows][ziduan[2]].ToString();
            }
            if (4 <= Countziduan)
            {
                label4_text.Text = dt.Rows[Rows][ziduan[3]].ToString();
            }
            if (5 <= Countziduan)
            {
                label5_text.Text = dt.Rows[Rows][ziduan[4]].ToString();
            }
            if (6 <= Countziduan)
            {
                label6_text.Text = dt.Rows[Rows][ziduan[5]].ToString();
            }
            if (7 <= Countziduan)
            {
                label7_text.Text = dt.Rows[Rows][ziduan[6]].ToString();
            }
            if (8 <= Countziduan)
            {
                label8_text.Text = dt.Rows[Rows][ziduan[7]].ToString();
            }
            if (9 <= Countziduan)
            {
                label9_text.Text = dt.Rows[Rows][ziduan[8]].ToString();
            }
            if (10 <= Countziduan)
            {
                label10_text.Text = dt.Rows[Rows][ziduan[9]].ToString();
            }
            if (11 <= Countziduan)
            {
                label11_text.Text = dt.Rows[Rows][ziduan[10]].ToString();
            }
            if (12 <= Countziduan)
            {
                label12_text.Text = dt.Rows[Rows][ziduan[11]].ToString();
            }
            if (13 <= Countziduan)
            {
                label13_text.Text = dt.Rows[Rows][ziduan[12]].ToString();
            }
            if (14 <= Countziduan)
            {
                label14_text.Text = dt.Rows[Rows][ziduan[13]].ToString();
            }
            if (15 <= Countziduan)
            {
                label15_text.Text = dt.Rows[Rows][ziduan[14]].ToString();
            }


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
                UpdateShow(Tempdt, AimRow, ziduan, Countziduan);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            AimRow++;
            if (AimRow > TotalRows - 1)
            {
                MessageBox.Show("已是最后一项，无法在查看下一项！");
                AimRow = TotalRows - 1;
                return;
            }
            else
                UpdateShow(Tempdt, AimRow, ziduan, Countziduan);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
