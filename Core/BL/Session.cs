namespace Ventus.BL
{
    /// <summary>
    /// 
    /// </summary>
    public static class Session
    {
        /// <summary>
        /// 
        /// </summary>
        public static BE.Usuario Usuario
        {
            get { return Helper.Session.Get() as BE.Usuario; }
            set { Helper.Session.Set(value); }
        }
    }
}
