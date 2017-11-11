namespace AuctionSystem.Controllers
{
    using AuctionSystem.Data;
    using Interfaces;
    using Models;
    using Models.Enums;
    using System.Collections.Generic;
    using System.Linq;

    public class PaymentController : IPaymentController
    {
        // TODO
        public void AddPayment(PaymentType type, string paymentTypeCode, int userId)
        {
            using (var db = new AuctionContext())
            {
                var payment = new Payment
                {
                    Type = type,
                    PaymentTypeCode = paymentTypeCode,
                    UserId = userId
                };
                db.Payments.Add(payment);
                db.SaveChanges();
            }
    }

        public Payment GetPaymentById(int paymentId)
        {
            using (var db = new AuctionContext())
            {
                return db.Payments.FirstOrDefault(p => p.Id == paymentId);
            }
             
        }


        
        public bool DeletePayment(int id)
        {
            using (var db = new AuctionContext())
            {
                var payment = GetPaymentById(id);

                    if(payment == null)
                    {
                    return false;
                    }
                db.Payments.Remove(payment);
                db.SaveChanges();
                return true;
            }
        }

        public IList<Payment> GetPaymentsByUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdatePayment(Payment payment, PaymentType type, string paymentTypeCode)
        {
            throw new System.NotImplementedException();
        }
    }
}
