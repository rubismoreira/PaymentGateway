using System;

namespace CO.PaymentGateway.Cache
{
    public interface ICOMemoryCache
    {
        bool TryGetFromCache(Func<string> cacheKey, out object cachedResponse);

        void WriteInCache(Func<string> cacheKey, object cachedResponse);
    }
}