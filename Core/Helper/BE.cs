using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Ventus.Helper
{
    /// <summary>
    /// Funciones para transformar DataSets en Business Entities.
    /// </summary>
    /// <remarks>
    /// NO MODIFICAR A MENOS QUE ESTE ABSOLUTAMENTE SEGURO(A) DE LO QUE HACE
    /// </remarks>
    public static partial class BE
    {
        internal static T CopyAs<T>(object source) where T : new()
        {
            var target = new T();
            foreach (var sourceProperty in source.GetType().GetProperties())
            {
                var targetProperty = typeof(T).GetProperty(sourceProperty.Name);
                if (targetProperty.CanWrite)
                    targetProperty.SetValue(target, sourceProperty.GetValue(source, null), null);
            }
            return target;
        }

        /// <summary>
        /// Convierte el primer DataTable de un DataSet en un List&lt;T&gt;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<T> GetList<T>(DataSet ds) where T : class, new()
        {
            return (from DataRow dr in ds.Tables[0].Rows
                    select GetFields<T>(dr)).ToList();
        }

        /// <summary>
        /// Convierte el primer renglón de la primera tabla de un DataSet en un objeto de la clase T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static T GetOne<T>(DataSet ds) where T : class, new()
        {
            return ds.Tables[0].Rows.Count == 1 ? GetFields<T>(ds.Tables[0].Rows[0]) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static Type[] BackTrack(string type)
        {
            return BackTrack(Types.FirstOrDefault(o => o.Name.Equals(type)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        internal static Type[] BackTrack(Type t)
        {
            var l = new List<Type>(new[] { t });
            if (t != null && XRef.ContainsKey(t))
            {
                var o = XRef[t].ToList();
                foreach (var u in o)
                    l.AddRange(BackTrack(u));
                o.AddRange(l.Where(p => !o.Contains(p)));
                return o.ToArray();
            }
            return l.ToArray();
        }

        internal static PropertyInfo[] Properties(Type t)
        {
            var k = "Properties." + t.FullName;
            var o = Cache.Get(k) as PropertyInfo[];
            if (o != null) return o;
            o = t.GetProperties()
                .Union(t.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance))
                .Where(p => p.CanWrite).ToArray();
            Cache.Set(k, o);
            return o;
        }
    }
}