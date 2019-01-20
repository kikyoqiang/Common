using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Common
{
    public class CacheHelper
    {
        public static object Get(string cacheKey)
        {
            return HttpRuntime.Cache[cacheKey];
        }
        public static void Add(string cacheKey, object obj, int cacheMinute)
        {
            HttpRuntime.Cache.Insert(cacheKey, obj, null, DateTime.Now.AddMinutes(cacheMinute),
                Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }
    }
}
