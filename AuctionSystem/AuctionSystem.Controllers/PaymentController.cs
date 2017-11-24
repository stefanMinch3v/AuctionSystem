namespace AuctionSystem.Controllers
{
    using Common;
    using Contracts;
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
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

        public void AddPayment(Payment payment, User user)
        {
            CoreValidator.ThrowIfNull(payment, nameof(payment));
            CoreValidator.ThrowIfNull(user, nameof(user));
            CoreValidator.ThrowIfNullOrEmpty(payment.PaymentTypeCode, nameof(payment.PaymentTypeCode));
            CoreValidator.ThrowIfNegativeOrZero(user.Id, nameof(user.Id));

            using (var db = new AuctionContext())
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

            using (var db = new AuctionContext())
            {
                var payment = db.Payments
                                        .Include("User")
                                        .FirstOrDefault(p => p.Id == paymentId);

                CoreValidator.ThrowIfNull(payment, nameof(payment));

                return payment;
            }
        }


        public bool UpdatePayment(Payment newPayment)
        {
            CoreValidator.ThrowIfNull(newPayment, nameof(newPayment));
            CoreValidator.ThrowIfNegativeOrZero(newPayment.Id, nameof(newPayment.Id));
            CoreValidator.ThrowIfNullOrEmpty(newPayment.PaymentTypeCode, nameof(newPayment.PaymentTypeCode));
            CoreValidator.ThrowIfNegativeOrZero(newPayment.UserId, nameof(newPayment.UserId));

            using (var db = new AuctionContext())
            {
                var dbPayment = GetPayment(newPayment.Id);

                CoreValidator.ThrowIfNull(dbPayment, nameof(dbPayment));

                db.Payments.Attach(dbPayment);

                dbPayment = newPayment;

                db.Entry(dbPayment).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }
        }
        public bool DeletePayment(Payment payment)
        {
            CoreValidator.ThrowIfNegativeOrZero(payment.Id, nameof(payment.Id));

            using (var db = new AuctionContext())
            {
                var paymentNew = GetPayment(payment.Id);

                CoreValidator.ThrowIfNull(payment, nameof(payment));

                db.Payments.Attach(paymentNew);
                db.Payments.Remove(paymentNew);
                db.Entry(paymentNew).State = EntityState.Deleted;
                db.SaveChanges();

                return true;
            }
        }

        public IList<Payment> GetPaymentsByUser(User user)
        {
            using (var db = new AuctionContext())
            {
                var payment = db.Payments.Where(p => p.UserId == user.Id).ToList();

                return payment;
            }
        }
    }
}

