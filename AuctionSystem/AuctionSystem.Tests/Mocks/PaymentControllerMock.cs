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
            throw new NotImplementedException();
        }

        public bool DeletePayment(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Payment> GetPaymentsByUser(int userId)
        {
            throw new NotImplementedException();
        }
        public Payment GetPaymentById(int paymentId)
        {
            using (var db = new AuctionContext())
            {
                return db.Payments.FirstOrDefault(p => p.Id == paymentId);
            }

        }

        public bool UpdatePayment(Payment payment, PaymentType type, string paymentTypeCode)
        {
            throw new NotImplementedException();
        }
    }
}
