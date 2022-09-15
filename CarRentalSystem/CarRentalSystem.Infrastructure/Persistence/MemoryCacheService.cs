namespace CarRentalSystem.Infrastructure.Persistence
{
    using Application.Contracts;

    using Infrastructure.Configuration;

    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Options;

    internal class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache memoryCache;
        private readonly CacheConfiguration cacheConfig;
        private readonly MemoryCacheEntryOptions cacheOptions;

        public MemoryCacheService(IMemoryCache memoryCache, IOptions<CacheConfiguration> cacheConfig)
        {
            this.memoryCache = memoryCache;
            this.cacheConfig = cacheConfig.Value;

            if (this.cacheConfig != null)
            {
                this.cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.UtcNow.AddHours(this.cacheConfig.AbsoluteExpirationInHours),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(this.cacheConfig.SlidingExpirationInMinutes)
                };
            }
        }

        public bool TryGet<T>(string cacheKey, out T value) => this.memoryCache.TryGetValue(cacheKey, out value);

        public T Set<T>(string cacheKey, T value) => this.memoryCache.Set(cacheKey, value, this.cacheOptions);

        public void Remove(string cacheKey) => this.memoryCache.Remove(cacheKey);
    }
}
