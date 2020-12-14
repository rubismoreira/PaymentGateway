using CO.PaymentGateway.Business.Core.Entities;
using CO.PaymentGateway.Business.Core.Repositories;
using CO.PaymentGateway.Business.Core.UseCases.Common;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Queries;
using CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Queries.Helper;
using System.Threading.Tasks;

namespace CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Queries
{
    public class PaymentProcessGetByIdQuery : IPaymentProcessGetByIdQuery
    {
        private readonly IPaymentProcessReadRepository _repository;

        public PaymentProcessGetByIdQuery(IPaymentProcessReadRepository repository)
        {
            this._repository = repository;
        }

        public async Task<PaymentProcessEntity> ExecuteAsync(GetByIdQueryRequest request)
        {
            var paymentProcessEntity = await _repository.GetByIdAsync(request.Id);
            CreditCardMask.Mask(ref paymentProcessEntity);

            return paymentProcessEntity;
        }
    }
}
