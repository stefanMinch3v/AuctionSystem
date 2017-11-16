namespace AuctionSystem.Controllers.Interfaces
{
    using Models;
    using Models.Enums;
    using System.Collections.Generic;

    public interface IPaymentController
    {
        void AddPayment(PaymentType type, string paymentTypeCode, int userId);

        Payment GetPayment(string paymentTypeCode);
        bool DeletePayment(string id);

        IList<Payment> GetPaymentsByUser(int userId);
    }
}
