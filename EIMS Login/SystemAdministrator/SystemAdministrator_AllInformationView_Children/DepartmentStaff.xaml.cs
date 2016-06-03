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
    /// DepartmentStaff.xaml 的交互逻辑
    /// </summary>
    public partial class DepartmentStaff : UserControl
    {
        MoreInf Demoinf;
        MoreInf Staffmoinf;
        InfTotal infnum = new InfTotal();
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
            DepartmentTable.AddColumns("DepId", "部门编号", 100);
            DepartmentTable.AddColumns("DepName", "名称", 100);
            DepartmentTable.AddColumns("Describes", "职能描述", 100);
            DepartmentTable.AddColumns("UpperId", "上级部门编号", 140);
        }
        private void InitStaffTable()
        {
            StaffTable.InitTableHeightWidth(420, 458);
            StaffTable.SetCanUserAddRows(false);
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

            DepartmentMenu.Click += DepartmentEventContent;
            StaffMenu.Click += StaffEventContent;
            DepartmentTable.dgMenu.Items.Add(DepartmentMenu);
            StaffTable.dgMenu.Items.Add(StaffMenu);
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
    }
}
