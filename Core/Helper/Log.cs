using System;
using System.Diagnostics;

namespace Ventus.Helper
{
    /// <summary>
    /// Writes diagnostics text to the EventLog and Log files.
    /// </summary>
    public static class Log
    {
        private const string Source = "Ventus";
        private const string LogName = "Application";

        static Log()
        {
            try
            {
                if (!EventLog.SourceExists(Source))
                    EventLog.CreateEventSource(Source, LogName);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Source + "--" + ex.Message + "--" + DateTime.Now);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public static void Information(string msg)
        {
            try
            {
                Trace.WriteLine(msg);
                EventLog.WriteEntry(Source, msg, EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Source + "--" + ex.Message + "--" + DateTime.Now);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public static void Error(Exception ex)
        {
            Error(null, ex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Error(string msg, Exception ex = null)
        {
            try
            {
                if (msg == null)
                    msg = string.Empty;
                if (!string.IsNullOrEmpty(msg) && ex != null)
                    msg += Environment.NewLine;
                msg += ex;
                Trace.WriteLine(msg);
                EventLog.WriteEntry(Source, msg, EventLogEntryType.Error);
            }
            catch (Exception exs)
            {
                Trace.WriteLine(exs.Source + "--" + exs.Message + "--" + DateTime.Now);
            }
        }
    }
}
