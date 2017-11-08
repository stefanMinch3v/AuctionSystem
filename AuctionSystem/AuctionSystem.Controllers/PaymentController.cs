namespace AuctionSystem.Controllers
{
    using Interfaces;
    using Models;
    using Models.Enums;
    using System.Collections.Generic;

    public class PaymentController : IPaymentController
    {
        // TODO
        public void AddPayment(PaymentType type, string paymentTypeCode, int userId)
        {
            throw new System.NotImplementedException();
        }

        public bool DeletePayment(Payment payment)
        {
            throw new System.NotImplementedException();
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
