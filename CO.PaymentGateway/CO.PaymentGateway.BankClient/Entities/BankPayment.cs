namespace CO.PaymentGateway.BankClient.Entities
{
    public class BankPayment
    {
        public string CardEncryptedData { get; set; }
        
        public decimal Amount { get; set; }
        
        public bool Deny { get; set; }
    }
}