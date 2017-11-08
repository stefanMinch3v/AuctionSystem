namespace AuctionSystem.WcfService
{
    using Interfaces;
    using Models;
    using Models.Enums;
    using System;
    using System.Collections.Generic;

    public class PaymentService : IPaymentService
    {
        // TODO in each method return the controller that supposed to handle its operation

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
