namespace AuctionSystem.WcfService.Contracts
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
        bool DeletePaymentById(int paymentId);

        [OperationContract]
        IList<Payment> GetPaymentsByUser(int userId);

        [OperationContract]
        bool UpdatePayment(int userId,string property,string value);

        [OperationContract]
        Payment GetPaymentById(int paymentId);
    }
}
