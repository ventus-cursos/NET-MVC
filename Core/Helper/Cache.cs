using System;
using System.Linq;

namespace Ventus.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public static class Cache
    {
        static Cache()
        {
            Storage = new WebCache();
        }

        private static readonly ICache Storage;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Cached<T>(Func<T> fn) where T : class
        {
            return Cached(fn, new object[] { });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fn"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static T Cached<T>(Func<T> fn, params object[] p) where T : class
        {
            if (!Storage.IsAvailable) return fn();
            var k = Key(p);
            var o = Get(k) as T;
            if (o != null) return o;
            o = fn();
            Set(k, o);
            return o;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        internal static void Set(string key, object value)
        {
            if (key == null || value == null) return;
            Storage.Set(key, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string[] AllKeys()
        {
            return Storage.AllKeys;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return Storage.Get(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            Storage.Remove(key);
        }

        /// <summary>
        /// Elimina inmediatamente elementos del cache.
        /// </summary>
        /// <param name="args"></param>
        public static void Clear(params object[] args)
        {
            var k = JoinArgs(args);
            foreach (var key in AllKeys().Where(key => key.StartsWith(k)))
                Remove(key);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Clear()
        {
            var m = Method.Caller("Ventus.DB");
            var backTrack = BE.BackTrack(m.Split('.')[0]);
            foreach (var t in backTrack.Where(o => o != null))
                Clear(t.Name + ".");
        }

        /// <summary>
        /// Genera la llave para el elemento del Cache.
        /// Incluye el nombre del método y los parámetros.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static string Key(params object[] args)
        {
            return Method.Caller("Ventus.DB") + "_" + JoinArgs(args);
        }

        private static string JoinArgs(object[] args)
        {
            return string.Join("_", Array.ConvertAll(args,
                o => o is Enum ? Convert.ToString(Convert.ToInt32(o)) : Convert.ToString(o)));
        }
    }
}
