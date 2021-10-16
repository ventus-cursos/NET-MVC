namespace Ventus.BE
{
    /// <summary>
    /// 
    /// </summary>
    public class Estado : Catalog
    {
        /// <summary>
        /// 
        /// </summary>
        public Estado()
        {
            Nombre = string.Empty;
            Abreviatura = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Abreviatura { get; set; }

        #region Conversion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Nombre;
        }

        #endregion
    }
}
