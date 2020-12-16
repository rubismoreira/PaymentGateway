using System.Threading.Tasks;
using CO.PaymentGateway.Business.Core.Entities;

namespace CO.PaymentGateway.Business.Core.Repositories
{
    public interface IPaymentProcessWriteRepository
    {
        Task WriteAsync(PaymentProcessEntity entity);
    }
}