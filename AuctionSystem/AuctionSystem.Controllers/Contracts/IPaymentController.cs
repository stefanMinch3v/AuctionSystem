namespace AuctionSystem.Controllers.Contracts
{
    using Models;
    using Models.Enums;
    using System.Collections.Generic;

    public interface IPaymentController
    {
        void AddPayment(Payment payment, User user);

        Payment GetPayment(int paymentId);
        
        bool DeletePayment(Payment payment);

        bool UpdatePayment(Payment payment, string property, string value);
        
        IList<Payment> GetPaymentsByUser(User user);
    }
}