namespace AuctionSystem.Controllers.Interfaces
{
    using Models;
    using Models.Enums;
    using System.Collections.Generic;

    public interface IPaymentController
    {
        void AddPayment(PaymentType type, string paymentTypeCode, int userId);

        bool UpdatePayment(Payment payment, PaymentType type, string paymentTypeCode);

        bool DeletePayment(int id);

        IList<Payment> GetPaymentsByUser(int userId);
    }
}
