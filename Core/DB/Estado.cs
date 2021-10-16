using System.Collections.Generic;
using System.Linq;

namespace Ventus.DB
{
    /// <summary>
    /// 
    /// </summary>
    public static class Estado
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<BE.Estado> List()
        {
            return Helper.Cache.Cached(() =>
                Helper.BE.GetList<BE.Estado>(Helper.DB.Query("DB", "EstadoList")));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BE.Estado Get(int id)
                {
            return List().FirstOrDefault(o => o.ID == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activo"></param>
        /// <returns></returns>
        public static List<BE.Estado> List(bool activo)
        {
            return List().Where(o => o.Activo == activo).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<BE.Estado> List(int start, int limit)
        {
            return List(true).Skip(start).Take(limit).ToList();
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
        /// <param name="o"></param>
        /// <returns></returns>
        public static int Save(BE.Estado o)
            {
            var id = Helper.DB.Execute("DB", "EstadoSave", o);
            Helper.Cache.Clear();
            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            Helper.DB.Execute("DB", "EstadoDelete", id);
            Helper.Cache.Clear();
        }
    }
}
