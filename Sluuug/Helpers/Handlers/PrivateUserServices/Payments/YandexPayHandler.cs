using Context;
using SharedModels.Yandex;
using Slug.Context.Tables;
using System;
using System.Linq;

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

        public void CompleteTransaction()
        {


        }
    }
}