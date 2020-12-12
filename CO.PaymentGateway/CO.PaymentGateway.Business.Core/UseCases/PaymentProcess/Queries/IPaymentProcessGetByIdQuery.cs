using CO.PaymentGateway.Business.Core.Entities;
using CO.PaymentGateway.Business.Core.UseCases.Common;
using System.Threading.Tasks;

namespace CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Queries
{
    public interface IPaymentProcessGetByIdQuery
    {
        Task<PaymentProcessEntity> ExecuteAsync(GetByIdQueryRequest request);
    }
}
