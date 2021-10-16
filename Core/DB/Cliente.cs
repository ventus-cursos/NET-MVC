using System.Collections.Generic;
using System.Linq;

namespace Ventus.DB
{
    /// <summary>
    /// 
    /// </summary>
    public static class Cliente
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<BE.Cliente> List()
        {
            return Helper.Cache.Cached(() =>
                Helper.BE.GetList<BE.Cliente>(Helper.DB.Query("DB", "ClienteList")));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BE.Cliente Get(int id)
                {
            return List().FirstOrDefault(o => o.ID == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activo"></param>
        /// <returns></returns>
        public static List<BE.Cliente> List(bool activo)
        {
            return List().Where(o => o.Activo == activo).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<BE.Cliente> List(int start, int limit)
        {
            return List(true).OrderBy(o => o.Nombre).ThenBy(o => o.Apellido).Skip(start).Take(limit).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int Save(BE.Cliente o)
        {
            var id = Helper.DB.Execute("DB", "ClienteSave", o);
            Helper.Cache.Clear();
            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            Helper.DB.Execute("DB", "ClienteDelete", id);
            Helper.Cache.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int Count()
            {
            return List(true).Count;
        }
    }
}
