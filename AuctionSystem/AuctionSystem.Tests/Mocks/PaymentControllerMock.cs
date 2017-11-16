namespace AuctionSystem.Tests.Mocks
{
    using Controllers.Interfaces;
    using Data;
    using Models;
    using Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PaymentControllerMock : IPaymentController
    {
        private readonly AuctionContext dbContext;

        public PaymentControllerMock(AuctionContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // TODO
        public void AddPayment(PaymentType type, string paymentTypeCode, int userId)
        {
            using (dbContext)
            {
                var payment = new Payment
                {
                    Type = type,
                    PaymentTypeCode = paymentTypeCode,
                    UserId = userId
                };
                dbContext.Payments.Add(payment);
                dbContext.SaveChanges();
            }
        }

        public bool DeletePayment(int id)
        {

            using (dbContext)
            {
                var payment = GetPaymentById(id);

                if (payment == null)
                {
                    return false;
                }
                dbContext.Payments.Remove(payment);
                dbContext.SaveChanges();
                return true;
            }
        }

        public IList<Payment> GetPaymentsByUser(int userId)
        {
            using (dbContext)
            {
                var payment = dbContext.Payments.Where(p => p.UserId == userId).ToList();

                return payment;
            }
        }
        public Payment GetPaymentById(int paymentId)
        {
            using (dbContext)
            {
                return dbContext.Payments.FirstOrDefault(p => p.Id == paymentId);
            }

        }

    }
}
