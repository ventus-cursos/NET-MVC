using System;
using System.Diagnostics;

namespace Ventus.Helper
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// **********************
    /// *** DE USO INTERNO ***
    /// **********************
    /// NO MODIFICAR A MENOS QUE ESTE ABSOLUTAMENTE SEGURO(A) DE LO QUE HACE
    /// </remarks>
    internal static class Method
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        internal static string Caller(string ns)
        {
            var levels = 0;
            do
            {
                var stackTrace = new StackTrace(levels, false);
                if (stackTrace.FrameCount == 0)
                    break;
                var methodBase = stackTrace.GetFrame(0).GetMethod();
                if (methodBase.DeclaringType == null)
                    break;
                if (methodBase.DeclaringType.Namespace != null && methodBase.DeclaringType.Namespace.Equals(ns))
                    return methodBase.DeclaringType.Name + '.' + methodBase.Name;
                levels++;
            } while (true);
            throw new ApplicationException("Caller method in " + ns + " namespace not found.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        internal static string JoinArgs(object[] args)
        {
            return string.Join("_", Array.ConvertAll(args,
                                                     o => o is Enum
                                                              ? Convert.ToString(Convert.ToInt32(o))
                                                              : Convert.ToString(o)));
        }
    }
}
