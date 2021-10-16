using System.Text.RegularExpressions;
using System.Web;

namespace Ventus.Helper
{
    /// <summary>
    /// Funciones para el uso de la sesión.
    /// </summary>
    /// <remarks>
    /// **********************
    /// *** DE USO INTERNO ***
    /// **********************
    /// NO MODIFICAR A MENOS QUE ESTE ABSOLUTAMENTE SEGURO(A) DE LO QUE HACE
    /// </remarks>
    internal static class Session
    {
        private static readonly Regex RePrefix = new Regex(@"^Session\.[gs]et_(.+)", RegexOptions.Compiled);

        private static string Key
        {
            get { return RePrefix.Match(Method.Caller("Ventus.BL")).Groups[1].Value; }
        }

        internal static void Set(object value)
        {
            HttpContext.Current.Session[Key] = value;
        }

        internal static object Get()
        {
            return HttpContext.Current.Session[Key];
        }
    }
}
