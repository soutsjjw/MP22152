using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Redis;

namespace SampleAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemoryCacheController : ControllerBase
{
    private readonly IDistributedCache _redisCache;
    private readonly ILogger<MemoryCacheController> _logger;
    private IMemoryCache _cache { get; set; }

    public MemoryCacheController(ILogger<MemoryCacheController> logger, IMemoryCache cache, IDistributedCache redisCache)
    {
        _logger = logger;
        _cache = cache;
        _redisCache = redisCache;
    }

    [HttpGet("")]
    public async Task<ActionResult<Dictionary<string, string>>> Get()
    {
        DateTime cacheEntry;

        // 嘗試取得指定的 Cache
        if (!_cache.TryGetValue("CacheKey", out cacheEntry))
        {
            // 指定的 Cache 不存在，所以給予一個新的值
            cacheEntry = DateTime.Now;

            // 設定 Cache 選項
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                // 設定 Cache 保存時間，如果有存取到就會刷新保存時間
                .SetSlidingExpiration(TimeSpan.FromSeconds(60));

            // 把資料儲存進 Cache 中
            _cache.Set("CacheKey", cacheEntry, cacheEntryOptions);
        }

        var redisCache = "";
        try
        {
            if (await _redisCache.GetStringAsync("RedisCacheKey") == null)
            {
                redisCache = DateTime.Now.ToString();
                var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
                await _redisCache.SetStringAsync("RedisCacheKey", redisCache, options);
            }
            else
            {
                redisCache = await _redisCache.GetStringAsync("RedisCacheKey");
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }

        return new Dictionary<string, string>
        {
            { "現在時間", DateTime.Now.ToString() },
            { "快取時間", cacheEntry.ToString() },
            { "快取時間(Redis)", redisCache },
        };
    }
}
