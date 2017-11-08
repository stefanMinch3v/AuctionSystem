namespace AuctionSystem.Tests.Mocks
{
    using Controllers.Interfaces;
    using Data;
    using Models;
    using Models.Enums;
    using System;
    using System.Collections.Generic;

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

        public bool DeletePayment(Payment payment)
        {
            throw new NotImplementedException();
        }

        public IList<Payment> GetPaymentsByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePayment(Payment payment, PaymentType type, string paymentTypeCode)
        {
            throw new NotImplementedException();
        }
    }
}
