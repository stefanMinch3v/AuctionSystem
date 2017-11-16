namespace AuctionSystem.WcfService
{
    using AuctionSystem.Controllers;
    using Interfaces;
    using Models;
    using Models.Enums;
    using System;
    using System.Collections.Generic;

    public class PaymentService : IPaymentService
    {
        // TODO in each method return the controller that supposed to handle its operation

        public void AddPayment(PaymentType type, string paymentTypeCode, int userId)
        {
            var paymentController = new PaymentController();
            paymentController.AddPayment(type, paymentTypeCode, userId);

        }

        public bool DeletePayment(Payment payment)
        {
            var paymentController = new PaymentController();
            paymentController.DeletePayment("1");
            return true;
        }
        public bool DeletePaymentById(string paymentID)
        {
            var paymentController = new PaymentController();
            paymentController.DeletePayment(paymentID);
            return true;
        }

        public Payment GetPaymentById(string paymentId)

        {
            var paymentController = new PaymentController();
            var payment = paymentController.GetPayment(paymentId);
            return TransferPayment(payment);
        }

        public Payment TransferPayment(Payment localpayment)
        {
            
            Payment localtransferpayment = new Payment();
            localtransferpayment.PaymentTypeCode = localpayment.PaymentTypeCode;
            localtransferpayment.Type = localpayment.Type;
            localtransferpayment.UserId = localpayment.UserId;
            return localtransferpayment;
                
        }

        public IList<Payment> GetPaymentsByUser(int userId)
        {
            var paymentController = new PaymentController();
            var list = paymentController.GetPaymentsByUser(userId);
            return list;
        }

        public bool UpdatePayment(Payment payment, PaymentType type, string paymentTypeCode)
        {
            throw new NotImplementedException();
        }
    }
}
