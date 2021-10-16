using System.Collections.Generic;
using System.Linq;

namespace Ventus.DB
{
    /// <summary>
    /// 
    /// </summary>
    public static class Ciudad
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<BE.Ciudad> List()
        {
            return Helper.Cache.Cached(() =>
                Helper.BE.GetList<BE.Ciudad>(Helper.DB.Query("DB", "CiudadList")));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activo"></param>
        /// <returns></returns>
        public static List<BE.Ciudad> List(bool activo)
        {
            return List().Where(o => o.Activo == activo).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<BE.Ciudad> List(int start, int limit)
        {
            return List(true).Skip(start).Take(limit).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BE.Ciudad Get(int id)
        {
            return List().FirstOrDefault(o => o.ID == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int Count()
        {
            return List(true).Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public static void Save(BE.Ciudad o)
        {
            Helper.DB.Execute("DB", "CiudadSave", o);
            Helper.Cache.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            Helper.DB.Execute("DB", "CiudadDelete", id);
            Helper.Cache.Clear();
        }
    }
}
