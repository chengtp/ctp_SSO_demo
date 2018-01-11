using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Cors;
using SSOServiceApi.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SSOServiceApi.Controllers
{
    [EnableCors("any")] //设置跨域处理的 代理
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private IMemoryCache _memoryCache;
        public HomeController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value"+id.ToString();
        }

        // GET api/values/5
        [HttpGet("test")]
        public string test()
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [Route("ValidateToken")]
        public bool ValidateToken(UserTotenInput user)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(user.UserCode) && !string.IsNullOrEmpty(user.Toten))
            {
                //判断 toten 是否有效
                if (_memoryCache.Get<string>(user.UserCode) == user.Toten)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }

            return flag;
        }

        //退出，删除
        [HttpPost]
        [Route("KeepToken")]
        public bool KeepToken(UserTotenInput user)
        {
            bool flag = true;
            if (_memoryCache.Get<string>(user.UserCode) == user.Toten)
            {
                _memoryCache.Remove(user.UserCode);
            }
            else
            {
                flag = false;
            }

            return flag;

        }



        [HttpPost]
        [Route("CheckAndGetToten")]
        public bool CheckAndGetToten([FromBody]UserTotenInput user)
        {
            bool flag = false;

            if (!string.IsNullOrEmpty(user.UserCode) && !string.IsNullOrEmpty(user.Toten))
            {
                string result;
                if (!_memoryCache.TryGetValue(user.UserCode, out result))
                {

                    //设置相对过期时间2分钟
                    _memoryCache.Set(user.UserCode, user.Toten, new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2)));
                    ////设置绝对过期时间2分钟
                    //_memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                    //    .SetAbsoluteExpiration(TimeSpan.FromMinutes(2)));
                    ////移除缓存
                    //_memoryCache.Remove(cacheKey);
                    ////缓存优先级 （程序压力大时，会根据优先级自动回收）
                    //_memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                    //    .SetPriority(CacheItemPriority.NeverRemove));
                    ////缓存回调 10秒过期会回调
                    //_memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                    //    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10))
                    //    .RegisterPostEvictionCallback((key, value, reason, substate) =>
                    //    {
                    //        Console.WriteLine($"键{key}值{value}改变，因为{reason}");
                    //    }));
                    ////缓存回调 根据Token过期
                    //var cts = new CancellationTokenSource();
                    //_memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                    //    .AddExpirationToken(new CancellationChangeToken(cts.Token))
                    //    .RegisterPostEvictionCallback((key, value, reason, substate) =>
                    //    {
                    //        Console.WriteLine($"键{key}值{value}改变，因为{reason}");
                    //    }));
                    //cts.Cancel();

                    flag = true;
                }

            }

            return flag;
        }
    }
}
