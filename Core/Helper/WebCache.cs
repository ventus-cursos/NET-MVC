using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Ventus.Helper
{
    internal class WebCache : ICache
    {
        public object Get(string key)
        {
            return HttpContext.Current.Cache.Get(key);
        }

        public void Set(string key, object value)
        {
            HttpContext.Current.Cache.Add(key, value, null, DateTime.MaxValue,
                BL.Config.CacheTimeout, CacheItemPriority.Normal, null);
        }

        public void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        public string[] AllKeys
        {
            get { return (from DictionaryEntry o in HttpContext.Current.Cache select (string)o.Key).ToArray(); }
        }

        public bool IsAvailable
        {
            get { return HttpContext.Current != null; }
        }
    }
}
