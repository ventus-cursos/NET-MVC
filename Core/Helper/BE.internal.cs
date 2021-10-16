using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Ventus.Helper
{
    /// <summary>
    /// Funciones para transformar DataSets en Business Entities.
    /// </summary>
    /// <remarks>
    /// **********************
    /// *** DE USO INTERNO ***
    /// **********************
    /// NO MODIFICAR A MENOS QUE ESTE ABSOLUTAMENTE SEGURO(A) DE LO QUE HACE
    /// </remarks>
    public partial class BE
    {
        private static Type[] _types;
        private static Dictionary<Type, HashSet<Type>> _xref;

        /// <summary>
        /// Convierte un DataRow en un objeto de la clase T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static T GetFields<T>(DataRow dr) where T : class, new()
        {
            var t = typeof(T);
            var o = (T)Activator.CreateInstance(t);
            foreach (var p in Properties(t).Where(q => q.CanWrite))
            {
                if (dr.Table.Columns.Contains(p.Name))
                {
                    var value = dr[p.Name];
                    if (value is DBNull || value == null)
                        value = p.PropertyType.IsClass ? null : Activator.CreateInstance(p.PropertyType);
                    if (p.PropertyType.IsEnum)
                        p.SetValue(o, Convert.ToInt32(value), null);
                    else
                    {
                        var type = p.PropertyType;
                        if (type.IsGenericType
                            && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                            type = new NullableConverter(p.PropertyType).UnderlyingType;
                        p.SetValue(o, Convert.ChangeType(value, type), null);
                    }
                }
            }
            return o;
        }

        internal static Type[] Types
        {
            get
            {
                if (_types != null && _types.Length != 0) return _types;
                _types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsBE()).ToArray();
                return _types;
            }
        }

        private static Dictionary<Type, HashSet<Type>> XRef
        {
            get
            {
                if (_xref != null) return _xref;
                _xref = Types.ToDictionary(t => t, t => new HashSet<Type>());
                foreach (var t in Types)
                {
                    foreach (var p in t.GetProperties())
                    {
                        var u = p.PropertyType;
                        if (u != t && u.IsBE() && _xref.ContainsKey(u))
                            _xref[u].Add(t);
                    }
                }
                var d = _xref.Where(o => o.Value.Count == 0).Select(o => o.Key).ToList();
                foreach (var t in d)
                    _xref.Remove(t);
                return _xref;
            }
        }

        private static bool IsBE(this Type t)
        {
            return t.IsClass && t.Namespace != null && t.Namespace.Equals("Ventus.BE");
        }
    }
}
