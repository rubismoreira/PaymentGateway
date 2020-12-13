namespace CO.PaymentGateway.Business.Core.PaymentProcessException
{
    public class PaymentDeniedThriceException : COPaymentException
    {
        public override string Message => "This credit card was refused for this very same context twice";
    }

}