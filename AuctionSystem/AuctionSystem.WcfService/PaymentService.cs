namespace AuctionSystem.WcfService
{
    using AutoMapper;
    using Contracts;
    using Controllers;
    using Models;
    using Models.DTOs;
    using System.Collections.Generic;

    public class PaymentService : IPaymentService
    {
        public void AddPayment(Payment payment, int userId)
        {
            PaymentController.Instance().AddPayment(payment, userId);
        }

        public PaymentDto GetPayment(int paymentId)
        {
            var dbPayment = PaymentController.Instance().GetPayment(paymentId);

            return MapDbPaymentToPaymentDto(dbPayment);
        }

        public PaymentDto MapDbPaymentToPaymentDto(Payment payment)
        {
            return Mapper.Map<PaymentDto>(payment);
        }

        private Payment MapPaymentDtoToDbPayment(PaymentDto paymentDto)
        {
            return new Payment
            {
                Id = paymentDto.Id,
                UserId = paymentDto.UserId,
                Type = paymentDto.Type,
                PaymentTypeCode = paymentDto.PaymentTypeCode
            };

            //  return Mapper.Map<Payment>(paymentDto);

        }

        public bool DeletePayment(int paymentId)
        {
            return PaymentController.Instance().DeletePayment(paymentId);
        }

        public bool UpdatePayment(PaymentDto paymentDto)
        {

            var paymentToUpdate = MapPaymentDtoToDbPayment(paymentDto);

            return PaymentController.Instance().UpdatePayment(paymentToUpdate);
        }

        //public IList<Payment> GetPaymentsByUser(User user)
        //{
        //    return PaymentController.Instance().GetPaymentsByUser(user);
        //}
    }
}
