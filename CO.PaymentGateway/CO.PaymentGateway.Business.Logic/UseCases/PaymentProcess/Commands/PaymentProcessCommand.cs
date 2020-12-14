using CO.PaymentGateway.Business.Core.Entities;
using CO.PaymentGateway.Business.Core.Repositories;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Commands;
using System.Threading.Tasks;
using CO.PaymentGateway.BankClient.Client;
using CO.PaymentGateway.BankClient.Entities;
using CO.PaymentGateway.Business.Core.Enums;
using System;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Rules;

namespace CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Commands
{
    public class PaymentProcessCommand : IPaymentProcessCommand
    {
        private readonly IPaymentProcessWriteRepository _repository;

        private readonly IBankHttpClient _bankClient;

        private readonly IPaymentRuleEngine _ruleEngine;
        
        public PaymentProcessCommand(IPaymentProcessWriteRepository repository, IBankHttpClient bankClient, IPaymentRuleEngine ruleEngine)
        {
            this._repository = repository;
            this._bankClient = bankClient;
            this._ruleEngine = ruleEngine;
        }

        public async Task<PaymentProcessResponse> ExecuteAsync(PaymentProcessRequest request)
        {
            this._ruleEngine.ProcessRules(request);

            var clientResponse = await this._bankClient.CreatePayment(new BankPayment
            {
                Amount = request.Amount,
                CardHolderName = request.CardHolderName,
                CreditCard = request.CardNumber,
                Cvv = request.CVV,
                ExpirationMonth = request.ExpirationMonth,
                ExpirationYear = request.ExpirationYear
            });

            PaymentProcessEntity entity = new PaymentProcessEntity()
            {
                Amount = request.Amount,
                CardNumber = request.CardNumber,
                CardType = request.CardType,
                CVV = request.CVV,
                Currency = request.Currency,
                RegistrationTime = request.RegistrationTime,
                ContextId = request.ContextId,
                UserId = request.UserId,
                BankResponse = clientResponse.BankResponseId,
                BankResponseStatus = clientResponse.BankResponseId == Guid.Empty ? PaymentStatus.NoAnswer : (PaymentStatus)clientResponse.Status
            };

            await this._repository.WriteAsync(entity);

            return new PaymentProcessResponse { ContextId = entity.ContextId, PaymentAcceptanceStatus = entity.BankResponseStatus, PaymentProcessId = entity.Id };
        }
    }
}
