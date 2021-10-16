using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ventus
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDmy(this DateTime? date)
        {
            return date != null && date != default(DateTime)
                ? date.Value.ToString("dd/MM/yyyy")
                : string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDmy(this DateTime date)
        {
            return ((DateTime?)date).ToDmy();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime? FromDmy(this string date)
        {
            return !string.IsNullOrEmpty(date)
                ? (DateTime?)DateTime.ParseExact(date, "dd/MM/yyyy", new CultureInfo("es-MX"))
                : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string NewIfZero(this int id)
        {
            return id != 0 ? id.ToString() : "«Nuevo»";
        }

        /// <summary>
        /// Verifica si el parámetro "find" es subcadena de "text", ignorando
        /// mayúsculas/minúsculas y acentos.
        /// </summary>
        /// <param name="text">Texto donde buscar.</param>
        /// <param name="find">Texto a buscar dentro de "text".</param>
        /// <returns></returns>
        public static bool Matches(this string text, string find)
        {
            return text.RemoveDiacritics()
                .IndexOf(find.RemoveDiacritics(), StringComparison.CurrentCultureIgnoreCase) != -1;
        }

        /// <summary>
        /// Elimina las marcas diacríticas (acentos, diéresis, tilde) del texto dado.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string RemoveDiacritics(this string text)
        {
            return string.Concat(
                text.Normalize(NormalizationForm.FormD)
                    .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) !=
                                 UnicodeCategory.NonSpacingMark)
                ).Normalize(NormalizationForm.FormC);
        }

        private static readonly Regex ReWord = new Regex(@"\w+", RegexOptions.Compiled);

        public static string[] Tokenize(this string text)
        {
            return (from Match m in ReWord.Matches(text) select m.Value).ToArray();
        }
    }
}
