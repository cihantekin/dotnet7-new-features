using Microsoft.Extensions.Caching.Memory;

namespace dotnet7_new_features.Cache
{
    public class MemoryCacheStatistics
    {
        public MemoryCacheStatistics() 
        {
            var cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 40,
                TrackStatistics = true,
            });

            cache.Set("key", "value", new MemoryCacheEntryOptions
            {
                Size = 10,
            });

            cache.Get("key");
            cache.Get("key");
            cache.Get("key");
            cache.Get("key");
            cache.Get("key");
            cache.Get("missing");

            var stats = cache.GetCurrentStatistics();

            Console.WriteLine($"Current entry count: {stats.CurrentEntryCount}");
            Console.WriteLine($"Current estimated size: {stats.CurrentEstimatedSize}");
            Console.WriteLine($"Total hits: {stats.TotalHits}");
            Console.WriteLine($"Total misses: {stats.TotalMisses}");
        }
    }
}
