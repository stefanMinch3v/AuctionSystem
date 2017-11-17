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

        public bool DeletePaymentById(int paymentID)
        {
            var paymentController = new PaymentController();
            paymentController.DeletePayment(paymentID);
            return true;
        }

        public Payment GetPaymentById(int paymentId)

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
            localtransferpayment.Id = localpayment.Id;
            return localtransferpayment;
                
        }

        public IList<Payment> GetPaymentsByUser(int userId)
        {
            var paymentController = new PaymentController();
            var list = paymentController.GetPaymentsByUser(userId);
            return list;
        }

        public bool UpdatePayment(int userId, string property, string value)
        {
            return new PaymentController().UpdatePayment(userId, property, value);
        }
    }
}
