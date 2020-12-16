using System.Threading.Tasks;
using CO.PaymentGateway.Business.Core.Entities;
using CO.PaymentGateway.Business.Core.Repositories;
using CO.PaymentGateway.Data.EFContext;

namespace CO.PaymentGateway.Data.Repositories.PaymentProcess
{
    public class PaymentProcessWriteRepository : IPaymentProcessWriteRepository
    {
        private readonly PaymentContext _context;

        public PaymentProcessWriteRepository(PaymentContext context)
        {
            _context = context;
        }

        public async Task WriteAsync(PaymentProcessEntity entity)
        {
            _context.ProcessPaymentEntities.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}