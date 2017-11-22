namespace AuctionSystem.Controllers
{
    using Common;
    using Data;
    using Contracts;
    using Models;
    using Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PaymentController : IPaymentController
    {
        private static PaymentController instance;

        private PaymentController()
        {
        }

        public static PaymentController Instance()
        {
            if (instance == null)
            {
                instance = new PaymentController();
            }

            return instance;
        }

        public void AddPayment(PaymentType type, string paymentTypeCode, int userId)
        {
            CoreValidator.ThrowIfNullOrEmpty(paymentTypeCode, nameof(paymentTypeCode));
            CoreValidator.ThrowIfNegativeOrZero(userId, nameof(userId));

            using (var db = new AuctionContext())
            {
                var payment = new Payment
                {
                    Type = type,
                    PaymentTypeCode = paymentTypeCode,
                    UserId = userId
                };

                db.Payments.Add(payment);
                db.SaveChanges();
            }
        }

        public Payment GetPayment(int paymentId)
        {
            CoreValidator.ThrowIfNegativeOrZero(paymentId, nameof(paymentId));

            using (var db = new AuctionContext())
            {
                return db.Payments.FirstOrDefault(p => p.Id == paymentId);
            }

        }


        public bool UpdatePayment(int paymentId, string property, string value)
        {
            CoreValidator.ThrowIfNegativeOrZero(paymentId, nameof(paymentId));
            CoreValidator.ThrowIfNullOrEmpty(property, nameof(property));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            using (var db = new AuctionContext())
            {
                var payment = GetPayment(paymentId);

                CoreValidator.ThrowIfNull(payment, nameof(payment));

                db.Payments.Attach(payment);

                switch (property.ToLower())
                {
                    case "paymenttypecode":
                        payment.PaymentTypeCode = value;
                        break;
                    default:
                        throw new ArgumentException("No such property.");
                }

                db.Entry(payment).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return true;
            }
        }
        public bool DeletePayment(int paymentId)
        {
            CoreValidator.ThrowIfNegativeOrZero(paymentId, nameof(paymentId));

            using (var db = new AuctionContext())
            {
                var payment = GetPayment(paymentId);

                CoreValidator.ThrowIfNull(payment, nameof(payment));

                db.Payments.Attach(payment);
                db.Payments.Remove(payment);
                db.SaveChanges();

                return true;
            }
        }

        public IList<Payment> GetPaymentsByUser(int userId)
        {
            using (var db = new AuctionContext())
            {
                var payment = db.Payments.Where(p => p.UserId == userId).ToList();

                return payment;
            }
        }
    }
}

