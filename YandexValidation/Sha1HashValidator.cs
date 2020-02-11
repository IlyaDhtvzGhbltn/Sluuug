using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SharedModels.Yandex;

namespace YandexValidation
{
    public static class Sha1HashValidator
    {
        private static readonly string secret = "CutvgqZgCI6lf/k00B/auu59";
        public static bool ValidateHash(CompleteTransaction y)
        {
            string dateTime = y.datetime;
            string code = y.codepro.ToString().ToLower();
            string query = $"{y.notification_type}&{y.operation_id}&{y.amount}&{y.currency}&{dateTime}&{y.sender}&{code}&{secret}&{y.label}";
            string querySha = string.Empty;

            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(query));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // "x2" = lowercase
                    sb.Append(b.ToString("x2"));
                }

                querySha = sb.ToString();
            }

            if (y.sha1_hash.ToLower() == querySha)
                return true;

            return false;
        }
    }
}
