using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CPDemo.Common
{
    public static class MD5
    {
        public static string ToMD5(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            byte[] _cipherText = new MD5CryptoServiceProvider().ComputeHash(bytes);
            return ByteArrayToString(_cipherText);
        }

        static string ByteArrayToString(byte[] arrInput)
        {
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (var i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }
}