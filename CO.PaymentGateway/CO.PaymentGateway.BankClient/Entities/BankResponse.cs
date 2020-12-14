using System;
using CO.PaymentGateway.BankClient.Enums;

namespace CO.PaymentGateway.BankClient.Entities
{
    public class BankResponse
    {
        public Guid BankResponseId { get; set; }

        public BankResponseStatus Status { get; set; }
    }
}