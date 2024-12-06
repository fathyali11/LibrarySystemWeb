using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace LibrarySystem.Services.Services.Cashing
{
    public class CashServices(IDistributedCache distributedCache) : ICacheServices
    {
        private readonly IDistributedCache _distributedCache = distributedCache;

        public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            };

            var json = JsonSerializer.Serialize(value);
            await _distributedCache.SetStringAsync(key, json, options,cancellationToken);
        }
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            var json=await _distributedCache.GetStringAsync(key,cancellationToken);
            return json is null ? default : JsonSerializer.Deserialize<T>(json);
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _distributedCache.RemoveAsync(key, cancellationToken);
        }

        
    }
}
