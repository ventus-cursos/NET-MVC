using System;
using System.Drawing;

namespace Ventus.BE
{
    /// <summary>
    /// 
    /// </summary>
    public class Cliente : Catalog
    {
        /// <summary>
        /// 
        /// </summary>
        public Cliente()
        {
            Apellido = string.Empty;
            Ciudad = new Ciudad();
        }

        /// <summary>
        /// 
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Apellido { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? FechaNacimiento { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Ciudad Ciudad { get; set; }

        /// <summary>
        /// Foto en formato "Raw Image" (arreglo de bytes)
        /// </summary>
        public byte[] Foto { get; set; }

        #region Composición

        /// <summary>
        ///
        /// </summary>
        internal int IDCiudad
        {
            get { return Ciudad.ID; }
            set { Ciudad = DB.Ciudad.Get(value); }
        }

        #endregion

        #region Conversion

        /// <summary>
        /// 
        /// </summary>
        public string NombreCompleto
        {
            get { return Nombre + " " + Apellido; }
        }

        /// <summary>
        /// Foto en formato Image (para manipularse en código)
        /// </summary>
        public Image FotoImage
        {
            get { return Foto.ToImage(); }
            set { Foto = value.ToByteArray(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return NombreCompleto;
        }

        #endregion
    }
}