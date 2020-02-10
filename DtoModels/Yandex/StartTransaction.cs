using SharedModels.Enums;
using System;


namespace SharedModels.Yandex
{
    public class StartTransaction
    {
        public Guid TransactionId { get; set; }
        public VIPstatusTypes Type { get; set; }
        public decimal Amount { get; set; }
    }
}
