namespace AuctionSystem.Tests.Mocks
{
    using AuctionSystem.Controllers.Common;
    using Controllers.Contracts;
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
       public void AddPayment(Payment payment, User user)
        {
            CoreValidator.ThrowIfNull(payment, nameof(payment));
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNullOrEmpty(payment.PaymentTypeCode, nameof(payment.PaymentTypeCode));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));

            using (var db = dbContext)
            {
                var paymentNew = new Payment
                {
                    Type = payment.Type,
                    PaymentTypeCode = payment.PaymentTypeCode,
                    UserId = user.Id
                };

                db.Payments.Add(paymentNew);
                db.SaveChanges();
            }
        }

        public Payment GetPayment(int paymentId)
        {
            CoreValidator.ThrowIfNegativeOrZero(paymentId, nameof(paymentId));

            using (var db = dbContext)
            {
                return db.Payments.FirstOrDefault(p => p.Id == paymentId);
            }

        }


        public bool UpdatePayment(Payment payment, string property, string value)
        {
            CoreValidator.ThrowIfNull(payment, nameof(payment));
            CoreValidator.ThrowIfNegativeOrZero(payment.Id, nameof(payment.Id));
            CoreValidator.ThrowIfNullOrEmpty(property, nameof(property));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            using (var db = dbContext)
            {
                var paymentNew = GetPayment(payment.Id);

                CoreValidator.ThrowIfNull(paymentNew, nameof(paymentNew));

                db.Payments.Attach(paymentNew);

                switch (property.ToLower())
                {
                    case "paymenttypecode":
                        payment.PaymentTypeCode = value;
                        break;
                    default:
                        throw new ArgumentException("No such property.");
                }

                // db.Entry(paymentNew).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return true;
            }
        }
        public bool DeletePayment(Payment payment)
        {
            CoreValidator.ThrowIfNegativeOrZero(payment.Id, nameof(payment.Id));

            using (var db = dbContext)
            {
                var paymentNew = GetPayment(payment.Id);

                CoreValidator.ThrowIfNull(paymentNew, nameof(paymentNew));

                db.Payments.Attach(paymentNew);
                db.Payments.Remove(paymentNew);
                //db.Entry(paymentNew).State = EntityState.Deleted;
                db.SaveChanges();

                return true;
            }
        }

        public IList<Payment> GetPaymentsByUser(User user)
        {
            using (var db = dbContext)
            {
                var payment = db.Payments.Where(p => p.UserId == user.Id).ToList();

                return payment;
            }
        }
    }

}
