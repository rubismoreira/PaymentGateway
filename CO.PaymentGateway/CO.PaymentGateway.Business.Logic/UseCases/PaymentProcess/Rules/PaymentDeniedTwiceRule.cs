using CO.PaymentGateway.Business.Core.PaymentProcessException;
using CO.PaymentGateway.Business.Core.Repositories;
using CO.PaymentGateway.Business.Core.UseCases.Common;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Commands;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Rules;

namespace CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Rules
{
    public class PaymentDeniedTwiceRule : IPaymentProcessValidationRule
    {
        private readonly IPaymentProcessReadRepository _paymentReadRepository;

        public PaymentDeniedTwiceRule(IPaymentProcessReadRepository paymentReadRepository)
        {
            _paymentReadRepository = paymentReadRepository;
        }

        public void ValidateAsync(ICommandRequest commandRequest)
        {
            var paymentProcessCommandRequest = commandRequest as PaymentProcessRequest;
            var numberOfDenials = _paymentReadRepository.GetNumberOfDenialsForCreditCardInAContextAsync
                (paymentProcessCommandRequest.ContextId, paymentProcessCommandRequest.CardNumber);

            if (numberOfDenials.Result >= 2) throw new PaymentDeniedThriceException();
        }
    }
}