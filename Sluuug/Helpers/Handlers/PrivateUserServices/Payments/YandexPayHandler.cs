using Context;
using SharedModels.Yandex;
using Slug.Context.Tables;
using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Slug.Helpers.Handlers.PrivateUserServices.Payments
{
    public class YandexPayHandler
    {
        public void RegisterTransaction(StartTransaction request, string sessionNumber)
        {
            using (var context = new DataBaseContext())
            {
                Session session = context.Sessions.First(x => x.Number == sessionNumber);
                User user = context.Users.First(x => x.Id == session.UserId);

                Transaction transaction = new Transaction();
                transaction.Session = sessionNumber;
                transaction.PaySender = user;
                transaction.Type = request.Type;
                transaction.InternalId = request.TransactionId;
                transaction.Amount = request.Amount;
                transaction.CreateDate = DateTime.UtcNow;

                context.Transactions.Add(transaction);
                context.SaveChanges();
            }
        }

        public async Task CompleteTransaction(CompleteTransaction tr)
        {
            await Task.Run(async () => 
            {
                using (var context = new DataBaseContext())
                {
                    Guid incomId = Guid.Parse(tr.label);
                    Transaction transaction = context.Transactions.First(x => x.InternalId == incomId);
                    string jsonProp = JsonConvert.SerializeObject(tr);

                    transaction.PaidDate = DateTime.UtcNow;
                    transaction.IsPaid = true;
                    transaction.Properties = jsonProp;
                    await context.SaveChangesAsync();
                }
            });
        }

        public bool TransactionStatus(Guid transactionId)
        {
            using (var context = new DataBaseContext())
            {
                Transaction tr = context.Transactions.First(x=>x.InternalId == transactionId);
                return tr.IsPaid;
            }
        }
    }
}