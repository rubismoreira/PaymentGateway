using CO.PaymentGateway.Business.Core.Entities;
using System.Threading.Tasks;

namespace CO.PaymentGateway.Business.Core.Repositories
{
    public interface IPaymentProcessWriteRepository
    {
        Task WriteAsync(PaymentProcessEntity entity); 

    }
}
