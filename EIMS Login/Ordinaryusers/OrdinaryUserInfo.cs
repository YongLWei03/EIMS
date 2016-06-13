using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace EIMS_Login.Ordinaryusers
{
    public class OrdinaryUserInfo
    {
         Connection Temp = new Connection();
         public struct UserInfo
        {
            public string Ryid;
            public string RyName;
            public Bitmap Photo;
            public string Sex;
            public string Nationalty;
            public string Birth;
            public string Title;
            public string Rank;
            public string Political_Party;
            public string Culture_Level;
            public string Marital_Condition;
            public string Family_Place;
            public string Id_Card;
            public int Dep_Id;
            public string Position;
            public string UpperId;
        }
        public UserInfo UserInfoTemp = new UserInfo();
        string Ryid;//用户编号
        public bool GetRyidStatus = true;//用户编号获取成功与否标记
        public OrdinaryUserInfo()
        {
             if(GetRyid())
                DataReader(); 
        }
        private bool GetRyid()
        {
            try
            {
                string UserStrSql = "select * from ArmsUsers where Usersname='" + MainWindow.CurrentUser + "'";
                SqlCommand CMD_1 = new SqlCommand(UserStrSql, Temp.GetConn());
                SqlDataReader Sdr_1 = CMD_1.ExecuteReader();
                Sdr_1.Read();
                Ryid = Sdr_1[3].ToString();
                Sdr_1.Close();
            }
            catch (Exception ex)
            { 
                 MessageBox.Show("获取用户编号失败，将导致无法获取个人信息!"+ex);
                 GetRyidStatus = false;
                 return false;
            }
                 return true;

        }
        private void DataReader()
        {
            try
            {
                string UserStrSql = "select * from ArmsPerson where Ryid='" + Ryid + "'";
                SqlCommand CMD_1 = new SqlCommand(UserStrSql, Temp.GetConn());
                SqlDataReader Sdr_1 = CMD_1.ExecuteReader();
                if(Sdr_1.Read())
                {
                    UserInfoTemp.Ryid = Ryid;
                    UserInfoTemp.RyName = Sdr_1[1].ToString();
                    UserInfoTemp.Sex =  Sdr_1[3].ToString();
                    UserInfoTemp.Nationalty = Sdr_1[4].ToString();
                    UserInfoTemp.Birth = Sdr_1[5].ToString();
                    UserInfoTemp.Title = Sdr_1[6].ToString();
                    UserInfoTemp.Rank = Sdr_1[7].ToString();
                    UserInfoTemp.Political_Party = Sdr_1[8].ToString();
                    UserInfoTemp.Culture_Level = Sdr_1[9].ToString();
                    UserInfoTemp.Marital_Condition = Sdr_1[10].ToString();
                    UserInfoTemp.Family_Place = Sdr_1[11].ToString();
                    UserInfoTemp.Id_Card = Sdr_1[12].ToString();
                    UserInfoTemp.Dep_Id = Convert.ToInt32(Sdr_1[13].ToString());
                    UserInfoTemp.Position = Sdr_1[14].ToString();
                    UserInfoTemp.UpperId = Sdr_1[15].ToString();

                    //有错！
                    
                    if (Sdr_1[2] != DBNull.Value)
                    {
                        byte[] image_bytes = (byte[])Sdr_1[2];
                        MemoryStream ms = new MemoryStream(image_bytes);
                        UserInfoTemp.Photo = new Bitmap(ms);
                    }
                    else
                    {
                        UserInfoTemp.Photo = null;
                    }
                    Sdr_1.Close();
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("拉取个人信息失败！"+ ex);
            }
        }
        public void UpDateInfo()
        {
            DataReader();
        }
    }
}
