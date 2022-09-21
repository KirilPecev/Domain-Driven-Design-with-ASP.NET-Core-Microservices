namespace CarRentalSystem.Infrastructure.Common.Persistence
{
    using Application.Common;
    using Application.Common.Contracts;

    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Options;

    internal class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache memoryCache;
        private readonly CacheSettings cacheConfig;
        private readonly MemoryCacheEntryOptions cacheOptions;

        public MemoryCacheService(IMemoryCache memoryCache, IOptions<CacheSettings> cacheConfig)
        {
            this.memoryCache = memoryCache;
            this.cacheConfig = cacheConfig.Value;

            if (this.cacheConfig != null)
            {
                cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.UtcNow.AddHours(this.cacheConfig.AbsoluteExpirationInHours),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(this.cacheConfig.SlidingExpirationInMinutes)
                };
            }
        }

        public bool TryGet<T>(string cacheKey, out T value) => memoryCache.TryGetValue(cacheKey, out value);

        public T Set<T>(string cacheKey, T value) => memoryCache.Set(cacheKey, value, cacheOptions);

        public void Remove(string cacheKey) => memoryCache.Remove(cacheKey);
    }
}
