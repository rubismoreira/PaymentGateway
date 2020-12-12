using CO.PaymentGateway.Business.Core.Entities;
using CO.PaymentGateway.Business.Core.Repositories;
using CO.PaymentGateway.Data.EFContext;
using System.Threading.Tasks;

namespace CO.PaymentGateway.Data.Repositories.PaymentProcess
{
    public class PaymentProcessWriteRepository : IPaymentProcessWriteRepository
    {
        private readonly PaymentContext _context;

        public PaymentProcessWriteRepository(PaymentContext context)
        {
            this._context = context;
        }

        public async Task WriteAsync(PaymentProcessEntity entity)
        {
            _context.ProcessPaymentEntities.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}
