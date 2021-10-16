using System;

namespace Ventus.BL
{
    /// <summary>
    /// 
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool Debug
        {
            get { return Helper.Config.GetBool(false); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static TimeSpan CacheTimeout
        {
            get { return Helper.Config.GetTimeSpan(20); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static decimal SqlConnectRetries
        {
            get { return Helper.Config.GetInt(3); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int RowsPerPage
        {
            get { return Helper.Config.GetInt(10); }
        }
    }
}
