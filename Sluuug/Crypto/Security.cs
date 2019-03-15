using System.Text;
using System.Security.Cryptography;
using System;

namespace Slug.Crypto
{
    public static class Security
    {
        public static string ConvertStringtoMD5(string strword, int length = 120)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(strword);
                byte[] hash = md5.ComputeHash(inputBytes);
                var sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2"));
                }
                StringBuilder result = new StringBuilder(length);
                for (int i = 0; i < length; i++)
                {
                    result.Append(sb.ToString()[random.Next(sb.Length)]);
                }
                return result.ToString();
            }
        }

        public static string ConvertStringToSHA512(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("x2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}