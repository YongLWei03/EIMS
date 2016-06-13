using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// WarehouseManager_3.xaml 的交互逻辑
    /// </summary>
    public partial class WarehouseManager_OutOfStorage : UserControl
    {
        Connection TempConn = new Connection();
        string ApplyTableSql = "select * from StoreIn";
        public WarehouseManager_OutOfStorage()
        {
            InitializeComponent();
            InWarehouseDate.DisplayDateEnd = DateTime.Now;
            ProductionDate.DisplayDateEnd = DateTime.Now;
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            int judge = 0;
            if (MessageBox.Show("    确认要提交？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.OK) != MessageBoxResult.OK)
            {
                return;
            }
            string readsql = "select Sid from Storehouse";
            SqlCommand warehouse = new SqlCommand(readsql, TempConn.GetConn());
            SqlDataReader wareid = warehouse.ExecuteReader();
            while(wareid.Read())
            {
                if(wareid[0].ToString() == WarehouseID.Text)
                {
                    judge = 1;
                    break;
                }
            }
            if(judge == 0)
            {
                MessageBox.Show("不存在该仓库，请重新输入！");
                return;
            }
            wareid.Close();
            readsql = "select * from ArmsSurplus";
            SqlCommand ZB = new SqlCommand(readsql, TempConn.GetConn());
            SqlDataReader ZBinformation = ZB.ExecuteReader();
            string DetectdRsetSQL = "" ;
            judge = 0;
            try
            {
                while (ZBinformation.Read())
                {
                    if (ZBinformation[1].ToString() == InWarehouseEquipmentID.Text)
                    {
                        if (MessageBox.Show("    库存中已有该装备，是否增加该装备数量？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.OK) != MessageBoxResult.OK)
                        {
                            return;
                        }
                        DetectdRsetSQL = "update ArmsSurplus set Zbnum = '" + (int.Parse(ZBinformation[3].ToString()) + int.Parse(InWarehouseCount.Text)) + "' where ZbId = '" + InWarehouseEquipmentID.Text + "'";
                        judge = 1;
                        break;
                    }
                }
                decimal dNum;
                if (InWarehousePrice.Text == "")
                {
                    dNum = 0;
                }
                else
                {
                    dNum = Convert.ToDecimal(InWarehousePrice.Text);
                }
                string data, makedata;
                if (ProductionDate.SelectedDate.ToString() == "")
                {
                    makedata = "";
                }
                else
                {
                    makedata = ProductionDate.SelectedDate.Value.ToString("yyyy-MM-dd");
                }
                if (judge == 0)
                {
                    DetectdRsetSQL = "insert into ArmsSurplus values('"+ InWarehouseEquipmentID.Text + "',"+dNum+",'" + InWarehouseCount.Text + "','"+ makedata + "','"+ WarehouseID .Text+ "','"+ Remark.Text+ "')";
                }
                if (InWarehouseDate.SelectedDate.ToString() == "")
                {
                    data = "";
                }
                else
                {
                    data = InWarehouseDate.SelectedDate.Value.ToString("yyyy-MM-dd");
                }
                string insertsql = "insert into StoreIn values('"+InWarehouseID.Text+"','"+InWarehouseType.Text+"','"+InWarehouseEquipmentID.Text+"','"+ makedata + "',"+dNum+",'"+InWarehouseCount.Text+"','"+WarehouseID.Text+"','"+Accepter.Text+"','"+Opertor.Text+ "','"+data+"','"+Remark.Text +"')";
                SqlCommand addwarehouse = new SqlCommand(insertsql, TempConn.GetConn());
                ZBinformation.Close();
                addwarehouse.ExecuteNonQuery();
                SqlCommand addnum = new SqlCommand(DetectdRsetSQL, TempConn.GetConn());
                addnum.ExecuteNonQuery();
                MessageBox.Show("提交成功！");
            }
            catch
            {
                MessageBox.Show("数据不合理，请重新输入！");
                return;
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            ApplyTableSql = "select * from Takeout";
            MessageBox.Show("即将导出表格，请你稍等。。。");
            string[] Str = { "出库编号", "出库类型", "出库装备编号", "装备单价", "出库装备数量", "仓库编号", "批准人", "经办人", "出库日期", "备注" };
            ExportExcel(ApplyTableSql, Str, "装备出库表格.xlsx");
        }
        public string ExportExcel(string SQL, string[] StrCloumns, string saveFileName)
        {
            try
            {
                DataTable Table0 = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(SQL, TempConn.GetConnStr());
                sda.Fill(Table0);
                DataSet ds = new DataSet();
                ds.Tables.Add(Table0);
                ChangeColumnName(ref ds, StrCloumns);
                if (ds == null)
                    return "数据库为空";

                bool fileSaved = false;
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    return "无法创建Excel对象，可能您的机子未安装Excel";
                }
                Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
                                                                                                                                      //写入字段
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = ds.Tables[0].Columns[i].ColumnName;
                }
                //写入数值
                for (int r = 0; r < ds.Tables[0].Rows.Count; r++)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        worksheet.Cells[r + 2, i + 1] = ds.Tables[0].Rows[r][i];
                    }
                    System.Windows.Forms.Application.DoEvents();
                }
                worksheet.Columns.EntireColumn.AutoFit();//列宽自适应。
                if (saveFileName != "")
                {
                    try
                    {
                        workbook.Saved = true;
                        workbook.SaveCopyAs(saveFileName);
                        fileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        fileSaved = false;
                        MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                        return "";
                    }
                }
                else
                {
                    fileSaved = false;
                }
                xlApp.Quit();
                GC.Collect();//强行销毁
                if (fileSaved && System.IO.File.Exists(saveFileName)) System.Diagnostics.Process.Start(saveFileName); //打开EXCEL
                MessageBox.Show("导出成功，默认保存在：我的电脑/文档");
                return "成功保存到Excel";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        /*对导出的Excel文档表头进行更改
         *参数1：dataset ds；为要进行修改的数据表
         *参数2：string []StrCloumns；修改的目标表头，顺序为从左到右
         */
        public void ChangeColumnName(ref DataSet ds, string[] StrCloumns)
        {
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                ds.Tables[0].Columns[i].ColumnName = StrCloumns[i];
            }

        }

        private void mytakeoutdatagrid_Loaded(object sender, RoutedEventArgs e)
        {
            mytakeoutdatagrid.connectataBase();
            totaltibrary.Content = mytakeoutdatagrid.takeout.Items.Count;
        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("即将导出表格，请你稍等。。。");
            string[] Str = { "入库编号", "入库类型", "入库装备编号", "生产日期", "入库装备单价", "入库装备数量", "仓库编号", "批准人", "经办人", "入库日期", "备注" };
            ExportExcel(ApplyTableSql, Str, "装备入库表格.xlsx");
        }
    }
}
