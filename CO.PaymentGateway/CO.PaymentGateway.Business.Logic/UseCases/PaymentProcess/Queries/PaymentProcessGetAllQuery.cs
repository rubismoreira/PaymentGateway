using System.Collections.Generic;
using System.Threading.Tasks;
using CO.PaymentGateway.Business.Core.Entities;
using CO.PaymentGateway.Business.Core.Repositories;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Queries;

namespace CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Queries
{
    public class PaymentProcessGetAllQuery : IPaymentProcessGetAllQuery
    {
        private readonly IPaymentProcessReadRepository _repository;

        public PaymentProcessGetAllQuery(IPaymentProcessReadRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<PaymentProcessEntity>> ExecuteAsync()
        {
            return _repository.GetAllAsync();
        }
    }
}