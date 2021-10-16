using System;
using System.Configuration;

namespace Ventus.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public static class Config
    {
        internal static string GetString()
        {
            return GetString(string.Empty);
        }

        internal static string GetString(string defaultValue)
        {
            return GetString(false, defaultValue);
        }

        internal static string GetString(bool throwOnNull)
        {
            return GetString(throwOnNull, null);
        }

        internal static string GetString(bool throwOnNull, string defaultValue)
        {
            var s = ConfigurationManager.AppSettings[Key];
            if (s != null || !throwOnNull)
                return s ?? defaultValue;
            throw new ApplicationException(Key + " undefined");
        }

        internal static TimeSpan GetTimeSpan(int defaultValue)
        {
            return new TimeSpan(0, GetInt(defaultValue), 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        internal static int GetInt(int defaultValue)
        {
            var s = GetString();
            int value;
            return !string.IsNullOrEmpty(s) && int.TryParse(s, out value) ? value : defaultValue;
        }

        internal static bool GetBool(bool defaultValue)
        {
            var s = GetString();
            bool value;
            return !string.IsNullOrEmpty(s) && bool.TryParse(s, out value) ? value : defaultValue;
        }

        internal static DateTime GetDateTime(DateTime defaultValue)
        {
            var s = GetString();
            DateTime d;
            return !string.IsNullOrEmpty(s) && DateTime.TryParse(s, out d) ? d : defaultValue;
        }

        private static string Key
        {
            get { return Method.Caller("Ventus.BL").Split('.')[1].Replace("get_", ""); }
        }
    }
}
