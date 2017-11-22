namespace AuctionSystem.Controllers.Contracts
{
    using Models;
    using Models.Enums;
    using System.Collections.Generic;

    public interface IPaymentController
    {
        void AddPayment(PaymentType type, string paymentTypeCode, int userId);

        Payment GetPayment(int paymentId);
        bool DeletePayment(int paymentId);

        bool UpdatePayment(int paymentId, string property, string value);
        IList<Payment> GetPaymentsByUser(int userId);
    }
}
