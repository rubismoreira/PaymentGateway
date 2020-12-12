using CO.PaymentGateway.Business.Core.Entities;
using CO.PaymentGateway.Business.Core.Repositories;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Commands;
using System;
using System.Threading.Tasks;
using CO.PaymentGateway.Business.Core.Enums;

namespace CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Commands
{
    public class PaymentProcessCommand : IPaymentProcessCommand
    {
        private readonly IPaymentProcessWriteRepository _repository;
        public PaymentProcessCommand(IPaymentProcessWriteRepository repository)
        {
            this._repository = repository;
        }

        public async Task<PaymentProcessResponse> ExecuteAsync(PaymentProcessRequest request)
        {
            PaymentProcessEntity entity = new PaymentProcessEntity()
            {
                Amount = request.Amount,
                CardNumber = request.CardNumber,
                CardType = request.CardType,
                CVV = request.CVV,
                Currency = request.Currency,
                RegistrationTime = request.RegistrationTime
            };

            await this._repository.WriteAsync(entity);

            //Bank Client Execution

            return new PaymentProcessResponse { BankResponse = Guid.NewGuid(), PaymentStatus = PaymentStatus.Approved};
        }
    }
}
