using System;

namespace Ventus.BE
{
    /// <summary>
    /// 
    /// </summary>
    public class Orden : Catalog
    {
        /// <summary>
        /// 
        /// </summary>
        public Orden()
        {
            Cliente = new Cliente();
            FechaOrden = DateTime.Today;
        }

        /// <summary>
        /// 
        /// </summary>
        public Cliente Cliente { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime FechaOrden { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Total { get; set; }

        #region Composición

        /// <summary>
        /// 
        /// </summary>
        internal int IDCliente
        {
            get { return Cliente.ID; }
            set { Cliente = DB.Cliente.Get(value); }
        }

        #endregion
    }
}
