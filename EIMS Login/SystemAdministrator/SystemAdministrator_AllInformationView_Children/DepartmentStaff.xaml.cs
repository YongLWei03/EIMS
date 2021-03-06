﻿using System;
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
using EIMS_Login;

namespace EIMS_Login.SystemAdministrator.SystemAdministrator_AllInformationView_Children
{
    /// <summary>
    /// DepartmentStaff.xaml 的交互逻辑
    /// </summary>
    public partial class DepartmentStaff : UserControl
    {
        MoreInf Demoinf;
        MoreInf Staffmoinf;
        InfTotal infnum = new InfTotal();
        Connection temp = new Connection();
        string[] StrDe = { "部门编号", "名称", "职能描述", "上级部门编号"};
        string[] StrRy = { "人员编号", "姓名", "性别", "民族", "生日", "职务", "军衔", "政治面貌", "文化程度", "婚姻状况", "籍贯", "身份证号", "所在部门编号", "工作岗位", "部门领导编号"};
        public DepartmentStaff()
        {
            InitializeComponent();
            InitDepartmentTable();
            InitStaffTable();
            DepartmentTable.DataTableSelect("select * from Departments", "更新");
            DepartmentsNum.Content = infnum.InfTotalSet("Departments");
            StaffTable.DataTableSelect("select * from ArmsPerson", "更新");
            RyNum.Content = infnum.InfTotalSet("ArmsPerson");
            InitRightBm();
        }
        private void InitDepartmentTable()
        {
            DepartmentTable.InitTableHeightWidth(420, 458);
            DepartmentTable.SetCanUserAddRows(false);
            DepartmentTable.SetCanUserDeletRows(true);
            DepartmentTable.AddColumns("DepId", "部门编号", 100);
            DepartmentTable.AddColumns("DepName", "名称", 100);
            DepartmentTable.AddColumns("Describes", "职能描述", 100);
            DepartmentTable.AddColumns("UpperId", "上级部门编号", 140);
        }
        private void InitStaffTable()
        {
            StaffTable.InitTableHeightWidth(420, 458);
            StaffTable.SetCanUserAddRows(false);
            StaffTable.SetCanUserDeletRows(true);
            StaffTable.AddColumns("RyId", "人员编号", 100);
            StaffTable.AddColumns("RyName", "姓名", 100);
            StaffTable.AddColumns("Sex", "性别", 100);
            StaffTable.AddColumns("Dep_Id", "所在部门编号", 140);
        }

        private void DepartmentExport_Click(object sender, RoutedEventArgs e)
        {
            DepartmentTable.ExportExcel("select * from Departments", StrDe, "部门信息表格.xlsx"); 
        }

        private void RyExport_Click(object sender, RoutedEventArgs e)
        {
            StaffTable.ExportExcel("select RyId, RyName, Sex, Nationalty, Birth, Title, Rank, Political_Party, Culture_Level, Marital_Condition, Family_Place, Id_Card, Dep_Id, Position, UpperId from ArmsPerson", StrRy, "人员信息表格.xlsx"); 
        }
        public void InitRightBm()
        {
            MenuItem DepartmentMenu = DepartmentTable.AddMenuItem("更多信息");
            MenuItem StaffMenu = StaffTable.AddMenuItem("更多信息");
            MenuItem DeletRowMenu1 = DepartmentTable.AddMenuItem("删除选中的行");
            MenuItem DeletRowMenu2 = DepartmentTable.AddMenuItem("删除选中的行");

            DepartmentMenu.Click += DepartmentEventContent;
            StaffMenu.Click += StaffEventContent;
            DeletRowMenu1.Click += DeletRows1;
            DeletRowMenu2.Click += DeletRows2;
            DepartmentTable.dgMenu.Items.Add(DepartmentMenu);
            DepartmentTable.dgMenu.Items.Add(DeletRowMenu1);
            StaffTable.dgMenu.Items.Add(StaffMenu);
            StaffTable.dgMenu.Items.Add(DeletRowMenu2);
        }

        public void DepartmentEventContent(object sender, RoutedEventArgs e)
        {
            Demoinf = new MoreInf();
            string[] Table_Str = { "DepId", "DepName", "Describes", "UpperId" };            
            if (DepartmentTable.dataGrid.SelectedIndex != -1)
            {
                Demoinf.SetValues(DepartmentTable.Getdt(), DepartmentTable.dataGrid.SelectedIndex, DepartmentTable.Rows, Table_Str, StrDe, 4);
                Demoinf.Show();
            }
            else MessageBox.Show("当前未选中任何行！");               
        }
        public void StaffEventContent(object sender, RoutedEventArgs e)
        {
            Staffmoinf = new MoreInf();
            string[] Table_Str = { "RyId", "RyName", "Sex", "Nationalty", "Birth", "Title", "Rank", "Political_Party", "Culture_Level", "Marital_Condition", "Family_Place", "Id_Card", "Dep_Id", "Position", "UpperId" };
            if (StaffTable.dataGrid.SelectedIndex != -1)
            {
                Staffmoinf.SetValues(StaffTable.Getdt(), StaffTable.dataGrid.SelectedIndex, StaffTable.Rows, Table_Str, StrRy, 15);
                Staffmoinf.Show();
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
                    for (int i = DepartmentTable.dataGrid.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        cmd.CommandText = "delete from Departments where DepId= '" + ((DataRowView)DepartmentTable.dataGrid.SelectedItems[i]).Row["DepId"].ToString() + "'";
                        cmd.ExecuteNonQuery();
                        ((DataRowView)DepartmentTable.dataGrid.SelectedItems[i]).Delete();
                    }
                }
                catch (Exception ae)
                {
                    MessageBox.Show("删除失败！");
                }
            }
           
        }
        public void DeletRows2(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = temp.GetConn();
            if (MessageBox.Show("您确定要删除吗？", "系统提示：", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                try
                {
                    for (int i = StaffTable.dataGrid.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        cmd.CommandText = "delete from ArmsPerson where RyId= '" + ((DataRowView)StaffTable.dataGrid.SelectedItems[i]).Row["RyId"].ToString() + "'";
                        cmd.ExecuteNonQuery();
                        ((DataRowView)StaffTable.dataGrid.SelectedItems[i]).Delete();
                    }
                }
                catch (Exception ae)
                {
                    MessageBox.Show("删除失败！");
                }
            }
                                   
        }

        private void StaffTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SystemAdministrator_NetUser NetUser = new SystemAdministrator_NetUser();
            NetUser.SetValue(Grid.RowProperty, 0);
            NetUser.SetValue(Grid.ColumnSpanProperty, 12);
            NetUser.SearchText.Text = ((DataRowView)StaffTable.dataGrid.SelectedItem).Row["RyId"].ToString();
            NetUser.Search_button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

    }
}
