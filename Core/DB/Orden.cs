using System.Collections.Generic;
using System.Linq;

namespace Ventus.DB
{
    /// <summary>
    /// 
    /// </summary>
    public static class Orden
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<BE.Orden> List(int start, int limit)
        {
            var list = Helper.BE.GetList<BE.Orden>(Helper.DB.Query("DB", "OrdenList", start, limit))
                .Select(o => Get(o.ID)).ToList();
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BE.Orden Get(int id)
        {
            return Helper.Cache.Cached(() =>
                Helper.BE.GetOne<BE.Orden>(Helper.DB.Query("DB", "OrdenGet", id)),
                id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int Count()
        {
            return Helper.DB.Execute("DB", "OrdenCount", 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int Save(BE.Orden o)
        {
            var id = Helper.DB.Execute("DB", "OrdenSave", o);
            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            Helper.DB.Execute("DB", "OrdenDelete", id);
        }
    }
}
