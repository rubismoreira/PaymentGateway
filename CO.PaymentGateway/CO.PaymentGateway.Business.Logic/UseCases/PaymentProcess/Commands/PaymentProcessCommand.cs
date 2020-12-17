using System;
using System.Threading.Tasks;
using CO.PaymentGateway.BankClient.Client;
using CO.PaymentGateway.BankClient.Entities;
using CO.PaymentGateway.Business.Core.Entities;
using CO.PaymentGateway.Business.Core.Enums;
using CO.PaymentGateway.Business.Core.Repositories;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Commands;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Rules;
using CO.PaymentGateway.Encryption.EncryptionClient;
using Microsoft.Extensions.Logging;

namespace CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Commands
{
    public class PaymentProcessCommand : IPaymentProcessCommand
    {
        private readonly IBankHttpClient _bankClient;
        private readonly IPaymentProcessWriteRepository _repository;
        private readonly ILogger<PaymentProcessCommand> _logger;
        private readonly IPaymentRuleEngine _ruleEngine;
        private readonly IEncryptionClient _encryptionClient;

        public PaymentProcessCommand(IPaymentProcessWriteRepository repository, IBankHttpClient bankClient,
            IPaymentRuleEngine ruleEngine, ILogger<PaymentProcessCommand> logger, IEncryptionClient encryptionClient)
        {
            _repository = repository;
            _bankClient = bankClient;
            _ruleEngine = ruleEngine;
            _logger = logger;
            _encryptionClient = encryptionClient;
        }

        public async Task<PaymentProcessResponse> ExecuteAsync(PaymentProcessRequest request)
        {
            _logger.LogInformation($"Validating payment on context {request.ContextId}");
            _ruleEngine.ProcessRules(request);

            _logger.LogInformation("Encrypting credit card data");
            var encryptedData = this._encryptionClient.Encrypt(request.CardStringRepresentation);

            _logger.LogInformation($"Creating request to bank entity");
            var clientResponse = await _bankClient.CreatePaymentAsync(new BankPayment
            {
                Amount = request.Amount,
                Deny = request.CardType == CardType.Other,
                CardEncryptedData = encryptedData
            });

            var entity = new PaymentProcessEntity
            {
                Amount = request.Amount,
                CardNumber = request.CardNumber,
                CardType = request.CardType,
                CVV = request.CVV,
                Currency = request.Currency,
                RegistrationTime = request.RegistrationTime,
                ContextId = request.ContextId,
                BankResponse = clientResponse.BankResponseId,
                BankResponseStatus = clientResponse.BankResponseId == Guid.Empty
                    ? PaymentStatus.NoAnswer
                    : (PaymentStatus)clientResponse.Status
            };

            _logger.LogInformation($"Storing payment process request on database");

            await _repository.WriteAsync(entity);

            return new PaymentProcessResponse
            {
                ContextId = entity.ContextId,
                PaymentAcceptanceStatus = entity.BankResponseStatus,
                PaymentProcessId = entity.Id
            };
        }
    }
}