using CO.PaymentGateway.Business.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Commands
{
    public class PaymentProcessRequest
    {
        [Required]
        [FromBody]
        public string CardNumber { get; set; }


        [Required]
        [FromBody]
        public string CCV { get; set; }


        [Required]
        [FromBody]
        public string Validation { get; set; }


        [Required]
        [FromBody]
        public decimal Amount { get; set; }


        [Required]
        [FromBody]
        public DateTime RegistrationTime { get; set; }


        [Required]
        [FromBody]
        public CardType CardType { get; set; }


        [Required]
        [FromBody]
        public Currency Currency { get; set; }
    }
}
