namespace AuctionSystem.Tests.Mocks
{
    using AuctionSystem.Controllers.Common;
    using Controllers.Interfaces;
    using Data;
    using Models;
    using Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PaymentControllerMock : IPaymentController
    {
        private readonly AuctionContext dbContext;

        public PaymentControllerMock(AuctionContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // TODO
        public void AddPayment(PaymentType type, string paymentTypeCode, int userId)
        {
            CoreValidator.ThrowIfNullOrEmpty(paymentTypeCode, nameof(paymentTypeCode));
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));
            using (dbContext)
            {
                var payment = new Payment
                {
                    Type = type,
                    PaymentTypeCode = paymentTypeCode,
                    UserId = userId
                };
                dbContext.Payments.Add(payment);
                dbContext.SaveChanges();
            }
        }
         public Payment GetPayment(int paymentId)
         {
                CoreValidator.ThrowIfNegativeOrZero(paymentId, nameof(paymentId));
                using (dbContext)
                {
                return dbContext.Payments.SingleOrDefault(p => p.Id == paymentId);
                }

         }
        public bool DeletePayment(int id)
        {
            CoreValidator.ThrowIfNegativeOrZero(id, nameof(id));
            using (dbContext)
            {
                var payment = GetPayment(id);

                if (payment == null)
                {
                    return false;
                }
                dbContext.Payments.Remove(payment);
                dbContext.SaveChanges();
                return true;
            }
        }

        public IList<Payment> GetPaymentsByUser(int userId)
        {
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));
            using (dbContext)
            {
                var payment = dbContext.Payments.Where(p => p.UserId == userId).ToList();

                return payment;
            }
        }
       

        public bool UpdatePayment(int paymentId, string property, string value)
        {
            CoreValidator.ThrowIfNegativeOrZero(paymentId, nameof(paymentId));
            CoreValidator.ThrowIfNullOrEmpty(property, nameof(property));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));
            using (dbContext)
            {
                var payment = GetPayment(paymentId);
                CoreValidator.ThrowIfNull(payment, nameof(payment));
                dbContext.Payments.Attach(payment);

                switch (property.ToLower())
                {

                    case "paymenttypecode":
                        payment.PaymentTypeCode = value;
                        break;

                    default:
                        throw new ArgumentException("No such property.");
                }
                return true;
            }
        }
    }

}
