using System;
using System.Threading.Tasks;
using CO.PaymentGateway.BankClient.Client;
using CO.PaymentGateway.BankClient.Entities;
using CO.PaymentGateway.BankClient.Enums;
using CO.PaymentGateway.Business.Core.Entities;
using CO.PaymentGateway.Business.Core.Enums;
using CO.PaymentGateway.Business.Core.PaymentProcessException;
using CO.PaymentGateway.Business.Core.Repositories;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Commands;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Rules;
using CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Commands;
using CO.PaymentGateway.Encryption.EncryptionClient;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CO.PaymentGateway.Tests.Logic.UseCases.PaymentProcess.Commands
{
    [TestFixture]
    public class PaymentProcessCommandTest
    {
        private PaymentProcessCommand _processCommand;
        
        private Mock<IBankHttpClient> _bankHttpClientMock;
        private Mock<IPaymentProcessWriteRepository> _paymentProcessWriteRepositoryMock;
        private Mock<ILogger<PaymentProcessCommand>> _loggerMock;
        private Mock<IPaymentRuleEngine> _paymentRuleEngineMock;
        private Mock<IEncryptionClient> _encryptionClientMock;
        private PaymentProcessRequest _defaultRequest;

        [SetUp]
        public void Setup()
        {
            _bankHttpClientMock = new Mock<IBankHttpClient>();
            _paymentProcessWriteRepositoryMock = new Mock<IPaymentProcessWriteRepository>();
            _loggerMock = new Mock<ILogger<PaymentProcessCommand>>();
            _paymentRuleEngineMock = new Mock<IPaymentRuleEngine>();
            _encryptionClientMock = new Mock<IEncryptionClient>();

            _defaultRequest = new PaymentProcessRequest
            {
                Amount = 5,
                Currency = Currency.USD,
                CardNumber = "4024007143288669",
                CardType = CardType.Visa,
                ContextId = Guid.NewGuid(),
                ExpirationMonth = 12,
                ExpirationYear = 2070,
                CardHolderName = "Darth Vader",
                CVV = "322",
                RegistrationTime = DateTime.Now
            };
            
            _processCommand = new PaymentProcessCommand(
                _paymentProcessWriteRepositoryMock.Object,
                _bankHttpClientMock.Object,
                _paymentRuleEngineMock.Object,
                _loggerMock.Object,
                _encryptionClientMock.Object);
        }

        [Test]
        public async Task PaymentProcessCommandExecuteAsync_HasValidRequest_ReturnsSuccessResponse()
        {
            //Arrange
            var favorableBankResponse = new BankResponse
            {
                Status = BankResponseStatus.Approved,
                BankResponseId = Guid.NewGuid()
            };
            
            _encryptionClientMock.Setup(enc => enc.Encrypt(It.IsAny<string>())).Returns("encrypted");
            _bankHttpClientMock.Setup(bank => bank.CreatePaymentAsync(It.IsAny<BankPayment>()))
                .Returns(Task.FromResult(favorableBankResponse));
            
            //Act
            var response = await this._processCommand.ExecuteAsync(_defaultRequest);

            //Assert
            _paymentRuleEngineMock.Verify(m=> m.ProcessRules(It.IsAny<PaymentProcessRequest>()), Times.Once);
            _paymentProcessWriteRepositoryMock.Verify(wre => wre.WriteAsync(It.IsAny<PaymentProcessEntity>()), Times.Once);
            Assert.AreEqual(response.ContextId, _defaultRequest.ContextId);
            Assert.AreEqual(response.PaymentAcceptanceStatus, PaymentStatus.Approved);
        }
        
        
        [Test]
        public async Task PaymentProcessCommandExecuteAsync_OneValidationFailsOnValidationEngine_ThrowsCOPaymentException()
        {
            //Arrange
            _paymentRuleEngineMock.Setup(m=> m.ProcessRules(It.IsAny<PaymentProcessRequest>())).Throws<COPaymentException>();
            
            //Assert
            var ex = Assert.ThrowsAsync<COPaymentException>(() => _processCommand.ExecuteAsync(this._defaultRequest));
        }
        
        [Test]
        public async Task PaymentProcessCommandExecuteAsync_FailsCommunicationWithBankClient_WritesOnDbNotPossibleToConnect()
        {
            //Arrange
            var badBankResponse = new BankResponse
            {
                BankResponseId = Guid.Empty
            };
            
            _encryptionClientMock.Setup(enc => enc.Encrypt(It.IsAny<string>())).Returns("encrypted");
            _bankHttpClientMock.Setup(bank => bank.CreatePaymentAsync(It.IsAny<BankPayment>()))
                .Returns(Task.FromResult(badBankResponse));
            
            //Act
            var response = await this._processCommand.ExecuteAsync(_defaultRequest);

            //Assert
            _paymentRuleEngineMock.Verify(m=> m.ProcessRules(It.IsAny<PaymentProcessRequest>()), Times.Once);
            _paymentProcessWriteRepositoryMock.Verify(wre => wre.WriteAsync(It.IsAny<PaymentProcessEntity>()), Times.Once);
            Assert.AreEqual(response.ContextId, _defaultRequest.ContextId);
            Assert.AreEqual(response.PaymentAcceptanceStatus, PaymentStatus.NoAnswer);
        }
    }
}