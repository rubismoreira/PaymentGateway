using System;
using CO.PaymentGateway.Business.Core.PaymentProcessException;
using CO.PaymentGateway.Business.Core.UseCases.Common;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Commands;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Rules;

namespace CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Rules
{
    public class ExpirationDateIsValidRule : IPaymentProcessValidationRule
    {
        public void ValidateAsync(ICommandRequest commandRequest)
        {
            var paymentProcessCommandRequest = commandRequest as PaymentProcessRequest;
            var expirationDate = new DateTime(paymentProcessCommandRequest.ExpirationYear, paymentProcessCommandRequest.ExpirationMonth, 1);

            if (expirationDate < DateTime.Today)
            {
                throw new  ExpirationDateException();
            }
        }
        
    }
}