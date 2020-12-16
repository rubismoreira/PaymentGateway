using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CO.PaymentGateway.Cache
{
    public static class COMemoryCacheRegistration
    {
        public static void ConfigureErmServiceSubscription(this IServiceCollection serviceCollection)
        {
            
            serviceCollection.AddMemoryCache();
        }
    }
}