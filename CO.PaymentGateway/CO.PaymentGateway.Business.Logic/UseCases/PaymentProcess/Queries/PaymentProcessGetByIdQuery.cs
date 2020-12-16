using System.Threading.Tasks;
using CO.PaymentGateway.Business.Core.Entities;
using CO.PaymentGateway.Business.Core.Repositories;
using CO.PaymentGateway.Business.Core.UseCases.Common;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Queries;
using CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Queries.Helper;
using CO.PaymentGateway.Cache;
using Microsoft.Extensions.Logging;

namespace CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Queries
{
    public class PaymentProcessGetByIdQuery : IPaymentProcessGetByIdQuery, ICachedQuery
    {
        private readonly ICOMemoryCache _memoryCache;
        private readonly IPaymentProcessReadRepository _repository;
        private readonly ILogger<PaymentProcessGetByIdQuery> _logger;

        public PaymentProcessGetByIdQuery(IPaymentProcessReadRepository repository, ICOMemoryCache memoryCache, ILogger<PaymentProcessGetByIdQuery> logger)
        {
            _repository = repository;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public string GetCacheKey(string propertyToKey)
        {
            return $"GetPaymentById-{propertyToKey}";
        }

        public async Task<PaymentProcessEntity> ExecuteAsync(GetByIdQueryRequest request)
        {
            _logger.LogInformation($"Executing get process payment with id {request.Id}");

            object cached;

            var inCache = _memoryCache.TryGetFromCache(
                () => GetCacheKey(request.ToString()),
                out cached);

            if (!inCache)
            {
                _logger.LogInformation("Payment process not in cache, retrieving from origin");
                var origin = await _repository.GetByIdAsync(request.Id);
                
                if (origin == null)
                    return origin;

                CreditCardMask.Mask(ref origin);
                _memoryCache.WriteInCache(
                    () => GetCacheKey(request.ToString()),
                    origin);

                return origin;
            }
            else
            {
                _logger.LogInformation($"Payment process retrieved from cache");
                return (PaymentProcessEntity)cached;
            }
        }
    }
}