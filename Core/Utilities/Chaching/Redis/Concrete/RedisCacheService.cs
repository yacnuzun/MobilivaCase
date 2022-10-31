using Core.Utilities.Chaching.Redis.Abstract;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Chaching.Redis.Concrete
{
    public class RedisCacheService : ICacheManager
    {
        IConfiguration _configuration;
        RedisCacheOptions RedisCacheOptions { get; set; }
        public string ConnectionString { get; set; }
        static ConnectionMultiplexer redisConnectionMultiplexer;
        public RedisCacheService(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration["ConnectionStringsCache:Redis"];
            RedisCacheOptions = new RedisCacheOptions
            {
                Configuration = ConnectionString
            };
            redisConnectionMultiplexer = ConnectionMultiplexer.Connect(ConnectionString);
        }

        public Task<bool> Clear()
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            using (var redisCache = new RedisCache(RedisCacheOptions))
            {

                var valueString = redisCache.GetString(key);
                if (!string.IsNullOrEmpty(valueString))
                {
                    var valueObject = JsonConvert.DeserializeObject<T>(valueString);
                    return (T)valueObject;
                }

                return default;
            }
        }

        public void Set<T>(string key, T model)
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(90)
            };

            using (var redisCache = new RedisCache(RedisCacheOptions))
            {
                var valueString = JsonConvert.SerializeObject(model);
                redisCache.SetString(key, valueString);
            }
        }
    }
}
