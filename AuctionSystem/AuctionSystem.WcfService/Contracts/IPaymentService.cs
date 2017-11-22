namespace AuctionSystem.WcfService.Contracts
{
    using Models;
    using Models.Enums;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IPaymentService
    {
        [OperationContract(IsOneWay = true)]
        void AddPayment(Payment payment, User user);

        [OperationContract]
        Payment GetPayment(int paymentId);

        [OperationContract]
        bool DeletePayment(Payment payment);

        [OperationContract]
        bool UpdatePayment(Payment payment, string property, string value);

        [OperationContract]
        IList<Payment> GetPaymentsByUser(User user);
    }
}