using CO.PaymentGateway.Business.Core.Entities;
using CO.PaymentGateway.Business.Core.UseCases.Common;

namespace CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Queries
{
    public interface IPaymentProcessGetByIdQuery : IQuery<GetByIdQueryRequest, PaymentProcessEntity>
    {
    }
}