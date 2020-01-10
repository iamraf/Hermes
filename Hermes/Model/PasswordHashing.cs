using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Hermes.Model
{
    class PasswordHashing
    {
        public static string hashPassword(string input) 
        {
            byte[] hash = _convertText(input);
            string finalString = _convertToStringHash(hash);
            return finalString;
        }

        private static byte[] _convertText(string input)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                //convert text to utf8 byte 
                byte[] text = Encoding.UTF8.GetBytes(input);
                //calculate hash value with mySHA265
                byte[] hashValue = mySHA256.ComputeHash(text);
                return hashValue;
            }
        }

        private static string _convertToStringHash(byte[] hash)
        {
            string text = "";
            //convert bytes back to string 
            for (int i = 0; i < hash.Length; i++)
                text = text + $"{hash[i]:X2}";
            
            return text;
        }
    }
}
