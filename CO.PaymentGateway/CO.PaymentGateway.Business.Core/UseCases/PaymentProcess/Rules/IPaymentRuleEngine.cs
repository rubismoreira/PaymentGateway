using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Commands;

namespace CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Rules
{
    public interface IPaymentRuleEngine
    {
        void ProcessRules(PaymentProcessRequest request);
    }
}