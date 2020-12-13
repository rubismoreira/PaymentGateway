using System.Collections.Generic;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Commands;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Rules;

namespace CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Rules
{
    public class PaymentRuleEngine : IPaymentRuleEngine
    {
        private readonly IEnumerable<IPaymentProcessValidationRule> _rules;

        public PaymentRuleEngine(IEnumerable<IPaymentProcessValidationRule> rules)
        {
            this._rules = rules;
        }

        public void ProcessRules(PaymentProcessRequest request)
        {
            foreach (var rule in this._rules)
            {
                rule.ValidateAsync(request);
            }
        }
    }
}