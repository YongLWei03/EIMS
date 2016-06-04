using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows;

namespace EIMS_Login
{
    class MD5Str
    {
        public MD5Str()
        {

        }
        public string MD5Encoding(string Str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] byteString = md5.ComputeHash(Encoding.Unicode.GetBytes(Str));
            md5.Clear();
            string builder = null;
            for (int i = 0; i < byteString.Length; i++)
                builder += byteString[i].ToString("x").PadLeft(2, '0');
            MessageBox.Show(builder);
            return builder;
        }
    }
}
