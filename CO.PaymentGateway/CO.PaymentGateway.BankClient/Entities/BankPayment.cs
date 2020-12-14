using System;

namespace CO.PaymentGateway.BankClient.Entities
{
    public class BankPayment
    {
        public string CreditCard { get; set; }
        public string CardHolderName { get; set; }
        public string Cvv { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public decimal Amount { get; set; }
    }
}