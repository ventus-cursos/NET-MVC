namespace Ventus.BE
{
    /// <summary>
    /// 
    /// </summary>
    public class Ciudad : Catalog
    {
        /// <summary>
        /// 
        /// </summary>
        public Ciudad()
        {
            Estado = new Estado();
            Activo = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Estado Estado { get; set; }

        #region Setters

        /// <summary>
        /// 
        /// </summary>
        internal int IDEstado
        {
            get { return Estado.ID; }
            set { Estado = DB.Estado.Get(value); }
        }

        #endregion

        #region ToString

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Nombre + ", " + Estado.Abreviatura;
        }

        #endregion
    }
}