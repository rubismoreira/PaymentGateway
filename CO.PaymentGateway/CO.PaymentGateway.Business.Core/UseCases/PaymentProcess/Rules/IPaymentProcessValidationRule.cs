using CO.PaymentGateway.Business.Core.UseCases.Common;

namespace CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Rules
{
    public interface IPaymentProcessValidationRule
    {
        void ValidateAsync(ICommandRequest commandRequest);
    }
}