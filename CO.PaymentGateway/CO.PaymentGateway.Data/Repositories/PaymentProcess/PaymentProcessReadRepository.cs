using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CO.PaymentGateway.Business.Core.Entities;
using CO.PaymentGateway.Business.Core.Enums;
using CO.PaymentGateway.Business.Core.Repositories;
using CO.PaymentGateway.Data.EFContext;
using Microsoft.EntityFrameworkCore;

namespace CO.PaymentGateway.Data.Repositories.PaymentProcess
{
    public class PaymentProcessReadRepository : IPaymentProcessReadRepository
    {
        private readonly PaymentContext _context;

        public PaymentProcessReadRepository(PaymentContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentProcessEntity>> GetAllAsync()
        {
            return await _context.ProcessPaymentEntities.ToListAsync();
        }

        public async Task<int> GetNumberOfDenialsForCreditCardInAContextAsync(Guid contextId, string creditCardNumber)
        {
            return _context.ProcessPaymentEntities
                .Count(x => x.ContextId == contextId
                            && x.CardNumber == creditCardNumber
                            && x.BankResponseStatus == PaymentStatus.Denied);
        }

        public async Task<PaymentProcessEntity> GetByIdAsync(int id)
        {
            return await _context.ProcessPaymentEntities.FindAsync(id);
        }
    }
}