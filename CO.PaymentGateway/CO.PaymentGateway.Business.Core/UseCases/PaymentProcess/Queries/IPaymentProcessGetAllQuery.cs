using CO.PaymentGateway.Business.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Queries
{
    public interface IPaymentProcessGetAllQuery
    {
        Task<IEnumerable<PaymentProcessEntity>> ExecuteAsync();
    }
}
