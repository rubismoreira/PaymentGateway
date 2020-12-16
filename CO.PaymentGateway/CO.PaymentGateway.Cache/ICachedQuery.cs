namespace CO.PaymentGateway.Cache
{
    public interface ICachedQuery
    {
        string GetCacheKey(string propertyToKey);
    }
}