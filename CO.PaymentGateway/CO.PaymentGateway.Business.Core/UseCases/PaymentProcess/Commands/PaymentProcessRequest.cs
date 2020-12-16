using System;
using CO.PaymentGateway.Business.Core.Enums;
using CO.PaymentGateway.Business.Core.UseCases.Common;

namespace CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Commands
{
    public class PaymentProcessRequest : ICommandRequest
    {
        public string CardNumber { get; set; }

        public string CardHolderName { get; set; }

        public string CVV { get; set; }

        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }

        public decimal Amount { get; set; }

        public DateTime RegistrationTime { get; set; }

        public CardType CardType { get; set; }

        public Currency Currency { get; set; }

        public Guid ContextId { get; set; }
    }
}