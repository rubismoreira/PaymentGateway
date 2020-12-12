using CO.PaymentGateway.Business.Core.Entities;
using CO.PaymentGateway.Business.Core.Repositories;
using CO.PaymentGateway.Data.EFContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CO.PaymentGateway.Data.Repositories.PaymentProcess
{
    public class PaymentProcessReadRepository : IPaymentProcessReadRepository
    {
        private readonly PaymentContext _context;
       
        public PaymentProcessReadRepository(PaymentContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<PaymentProcessEntity>> GetAllAsync()
        {
            return await _context.ProcessPaymentEntities.ToListAsync();
        }

        public async Task<PaymentProcessEntity> GetByIdAsync(int id)
        {
            return await _context.ProcessPaymentEntities.FindAsync(id);
        }
    }
}
