using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Yandex
{
    public class CompleteTransaction
    {
        public string operation_id { get; set; }
        public string notification_type { get; set; }
        public DateTime datetime { get; set; }
        public string sha1_hash { get; set; }
        public string sender { get; set; }
        public bool codepro { get; set; }
        public string currency { get; set; }
        public decimal amount { get; set; }
        public decimal withdraw_amount { get; set; }
        public string label { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string fathersname { get; set; }
        public int zip { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public int building { get; set; }
        public int suite { get; set; }
        public int flat { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }
}
