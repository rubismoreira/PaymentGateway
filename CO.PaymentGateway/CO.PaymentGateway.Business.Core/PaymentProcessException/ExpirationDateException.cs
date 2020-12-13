namespace CO.PaymentGateway.Business.Core.PaymentProcessException
{
    public class ExpirationDateException : COPaymentException
    {
        public override string Message => "Expiration Date is already due";
    }
}