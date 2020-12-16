using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace CO.PaymentGateway.Cache
{
    public class COMemoryCache : ICOMemoryCache
    {
        private const string querySourceResponseHeaderKey = "X-Query-Source";
        private readonly IMemoryCache _cache;

        private readonly IHttpContextAccessor _contextAccessor;

        public COMemoryCache(IMemoryCache cache, IHttpContextAccessor contextAccessor)
        {
            _cache = cache;
            _contextAccessor = contextAccessor;
        }

        public bool TryGetFromCache(Func<string> cacheKey, out object cachedResponse)
        {
            var resultOrigin = string.Empty;
            var isInCache = _cache.TryGetValue(cacheKey.Invoke(), out cachedResponse);
            if (isInCache)
                resultOrigin = "cache";
            else
                resultOrigin = "origin";
            _contextAccessor.HttpContext.Response.Headers.Add(querySourceResponseHeaderKey, resultOrigin);
            return isInCache;
        }

        public void WriteInCache(Func<string> cacheKey, object cachedResponse)
        {
            var cacheExpirationOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(1),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };
            _cache.Set(cacheKey.Invoke(), cachedResponse, cacheExpirationOptions);
        }
    }
}