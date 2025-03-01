using BigBrother.Core.Services.Contract;
using Microsoft.AspNetCore.Connections;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BigBrother.Services.Chaches
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<string> GetCacheKeyAsync(string key)
        {
            var cacheresponse = await _database.StringGetAsync(key);
            if (cacheresponse.IsNullOrEmpty) return null;
            return cacheresponse.ToString();
        }

        public async Task SetCacheKeyAsync(string key, object response, TimeSpan expireTime)
        {
            if (response is null) return;
            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await _database.StringSetAsync(key,JsonSerializer.Serialize(response,options), expireTime);
        }
    }
}
