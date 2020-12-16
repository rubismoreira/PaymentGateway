using System.Threading.Tasks;
using CO.PaymentGateway.Business.Core.Entities;
using CO.PaymentGateway.Business.Core.Repositories;
using CO.PaymentGateway.Business.Core.UseCases.Common;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Queries;
using CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Queries.Helper;
using CO.PaymentGateway.Cache;

namespace CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Queries
{
    public class PaymentProcessGetByIdQuery : IPaymentProcessGetByIdQuery, ICachedQuery
    {
        private readonly ICOMemoryCache _memoryCache;
        private readonly IPaymentProcessReadRepository _repository;

        public PaymentProcessGetByIdQuery(IPaymentProcessReadRepository repository, ICOMemoryCache memoryCache)
        {
            _repository = repository;
            _memoryCache = memoryCache;
        }

        public string GetCacheKey(string propertyToKey)
        {
            return $"GetPaymentById-{propertyToKey}";
        }

        public async Task<PaymentProcessEntity> ExecuteAsync(GetByIdQueryRequest request)
        {
            object cached;

            var inCache = _memoryCache.TryGetFromCache(
                () => GetCacheKey(request.ToString()),
                out cached);

            if (!inCache)
            {
                var origin = await _repository.GetByIdAsync(request.Id);
                CreditCardMask.Mask(ref origin);
                _memoryCache.WriteInCache(
                    () => GetCacheKey(request.ToString()),
                    origin);

                return origin;
            }

            return (PaymentProcessEntity) cached;
        }
    }
}