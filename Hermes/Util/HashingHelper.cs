using System.Text;
using System.Security.Cryptography;

namespace Hermes.Util
{
    class HashingHelper
    {
        public static string HashPassword(string input) 
        {
            byte[] hash = ConvertText(input);
            string finalString = ConvertToStringHash(hash);

            return finalString;
        }

        private static byte[] ConvertText(string input)
        {
            using SHA256 mySHA256 = SHA256.Create();
            byte[] text = Encoding.UTF8.GetBytes(input);
            byte[] hashValue = mySHA256.ComputeHash(text);

            return hashValue;
        }

        private static string ConvertToStringHash(byte[] hash)
        {
            string text = "";

            for (int i = 0; i < hash.Length; i++)
            {
                text += $"{hash[i]:X2}";
            }
            
            return text;
        }
    }
}
