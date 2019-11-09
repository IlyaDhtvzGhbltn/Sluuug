using System.Text;
using System.Security.Cryptography;
using System;
using Slug.Context.Dto.OAuth.Ok;

namespace Slug.Crypto
{
    public static class Encryption
    {
        public static string EncryptionStringtoMD5(string strword, int length = 120)
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

        public static string EncryptionStringToSHA512(string input)
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

        public static OkApiMD5Params OkSecretParams(OkSignatureModel signature)
        {
            var md5Params = new OkApiMD5Params();

            string secretKeyDecripting = string.Format("{0}{1}", signature.AccessToken, signature.ApplicationSecretKey);
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(secretKeyDecripting);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            string secretKey = sb.ToString();
            md5Params.SecretKey = secretKey;

            //application_key=CJPNMFJGDIHBABABAformat=jsonmethod=users.getCurrentUser8678acffae05d5139a7253bb8168d571
            string paramString = 
                string.Format(
                    "application_key={0}format={1}method={2}{3}", signature.AppPublicKey, signature.Format, signature.Method, secretKey);

            byte[] inputParamStringBytes = Encoding.ASCII.GetBytes(paramString);
            byte[] hashParam = md5.ComputeHash(inputParamStringBytes);
            StringBuilder sbParameter = new StringBuilder();
            for (int i = 0; i < hashParam.Length; i++)
            {
                sbParameter.Append(hashParam[i].ToString("x2")); // the hash was here!
            }
            string secret = sbParameter.ToString();
            md5Params.Signature = secret;
            return md5Params;
        }
    }
}