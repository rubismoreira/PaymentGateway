using System;
using CO.PaymentGateway.Business.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CO.PaymentGateway.Business.Core.Repositories
{
    public interface IPaymentProcessReadRepository
    {
        Task<PaymentProcessEntity> GetByIdAsync(int id);

        Task<IEnumerable<PaymentProcessEntity>> GetAllAsync();

        Task<int> GetNumberOfDenialsForCreditCardInAContextAsync(Guid contextId, string creditCardNumber);
    }
}
