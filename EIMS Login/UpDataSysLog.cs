using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace EIMS_Login
{
    class UpDataSysLog
    {
        struct Log
        {
            public string LogId;
            public string LogDate;
            public string LogTime;
            public string LogType;
            public string Title;
            public string Body;
            public string UserName;
        };//日志结构体
        Log TempLog = new Log();
        Connection TempConn = new Connection();
        DataTable Logdt = new DataTable();//从数据库都到的当前用户操作日志
        int TotalRows;//当前用户日志总条数
        string SQL = "select * from SysLog where UserName='";//SQL语句，未完成
        public UpDataSysLog()
        {

        }
        public void SetLogValues(string LogType,string Title,string Body,string UserName)
        {
            TempLog.LogDate = DateTime.Now.ToString("yyyy-MM-dd");
            TempLog.LogTime = DateTime.Now.ToLongTimeString().ToString();
            TempLog.LogType = LogType;
            TempLog.Title = Title;
            TempLog.Body = Body;
            TempLog.UserName = UserName;
            SaveLog();
        }//生成日志信息
        public void UpdateLogTable()
        {
            try
            {
                SQL += TempLog.UserName + "'";//补全SQL语句
                SqlDataAdapter sda = new SqlDataAdapter(SQL, TempConn.GetConnStr());
                sda.Fill(Logdt);
                TotalRows = Logdt.Rows.Count;//更新日志总条数
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常！\n\n" + ex);
            }
        }//更新读取的数据库数据
        private void SaveLog()
        {
            string StrSQL = "insert into SysLog values('"+ TempLog.LogDate + "','"+ TempLog .LogTime+ "','"+ TempLog .LogType+ 
                "','"+ TempLog .Title+ "','"+ TempLog .Body+ "','"+ TempLog .UserName+ "')";
            try
            {
                SqlCommand cmd = new SqlCommand(StrSQL, TempConn.GetConn());
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show("日志写入异常！"+ ex);
            }
        }//向数据库写入数据
    }
}
