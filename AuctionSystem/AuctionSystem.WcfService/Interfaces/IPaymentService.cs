namespace AuctionSystem.WcfService.Interfaces
{
    using Models;
    using Models.Enums;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IPaymentService
    {
        [OperationContract]
        void AddPayment(PaymentType type, string paymentTypeCode, int userId);

        [OperationContract]
        bool UpdatePayment(Payment payment, PaymentType type, string paymentTypeCode);

        [OperationContract]
        bool DeletePayment(Payment payment);

        [OperationContract]
        IList<Payment> GetPaymentsByUser(int userId);
    }
}
