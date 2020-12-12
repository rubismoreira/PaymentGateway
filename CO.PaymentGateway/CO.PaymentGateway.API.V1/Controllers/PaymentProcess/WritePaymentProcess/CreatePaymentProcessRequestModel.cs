using System;
using System.ComponentModel.DataAnnotations;
using CO.PaymentGateway.Business.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Commands;
namespace CO.PaymentGateway.API.V1.Controllers.PaymentProcess.WritePaymentProcess
{
    public class CreatePaymentProcessRequestModel
    {
        public CreatePaymentProcessRequestModel()
        {

        }

        [Required, FromBody]
        public string CardHolderName { get; set; }

        [Required, CreditCard, FromBody]
        public string CardNumber { get; set; }

        [Required, FromBody]
        [RegularExpression("^[0-9]{3, 4}$")]
        public string CVV { get; set; }

        [Required, FromBody]
        [Range(1, 12)]
        public int ExpirationMonth { get; set; }

        [Required, FromBody]
        [Range(2020, Int32.MaxValue)]
        public int ExpirationYear { get; set; }

        [Required, FromBody]
        [Range(typeof(decimal), "5.0", "7500.0")]
        public decimal Amount { get; set; }

        [Required, FromBody]
        [EnumDataType(typeof(CardType))]
        public CardType CardType { get; set; }

        [Required, FromBody]
        [EnumDataType(typeof(Currency))]
        public Currency Currency { get; set; }

        public PaymentProcessRequest ToBusinessRequestModel()
        {
            return new PaymentProcessRequest
            {
                Amount = this.Amount,
                CardNumber = this.CardNumber,
                CardType = this.CardType,
                CCV = this.CVV,
                Currency = this.Currency,
                RegistrationTime = DateTime.Now,
                ExpirationMonth = this.ExpirationMonth,
                ExpirationYear = this.ExpirationYear
            };
        }
    }
}