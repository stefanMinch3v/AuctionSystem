namespace AuctionSystem.Controllers.Contracts
{
    using Models;
    using System.Collections.Generic;

    public interface IPaymentController
    {
        void AddPayment(Payment payment, User user);

        Payment GetPayment(int paymentId);
        
        bool DeletePayment(Payment payment);

        bool UpdatePayment(Payment payment);
        
        IList<Payment> GetPaymentsByUser(User user);
    }
}