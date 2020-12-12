using CO.PaymentGateway.Business.Core.Enums;
using System;

namespace CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Commands
{
    public class PaymentProcessResponse
    {
        public Guid BankResponse { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
    }
}
