namespace AuctionSystem.WcfService
{
    using Controllers;
    using Contracts;
    using Models;
    using Models.Enums;
    using System.Collections.Generic;

    public class PaymentService : IPaymentService
    {
        // TODO in each method return the controller that supposed to handle its operation

        public void AddPayment(Payment payment, User user)
        {
            PaymentController.Instance().AddPayment(payment, user);
        }

        public Payment GetPayment(int paymentId)
        {
            return PaymentController.Instance().GetPayment(paymentId);
        }

        public bool DeletePayment(Payment payment)
        {
            return PaymentController.Instance().DeletePayment(payment);
        }

        public bool UpdatePayment(Payment payment, string property, string value)
        {
            return PaymentController.Instance().UpdatePayment(payment, property, value);
        }

        public IList<Payment> GetPaymentsByUser(User user)
        {
            return PaymentController.Instance().GetPaymentsByUser(user);
        }
    }
}
