using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Ventus.Helper
{
    /// <summary>
    ///
    /// </summary>
    public static partial class DB
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sp"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int Execute(string db, string sp, params object[] args)
        {
            var t = DateTime.Now;
            using (var cn = GetConnection(db))
            {
                var cmd = PrepareCommand(cn, null, sp, args);
                cmd.ExecuteNonQuery();
                HandleOutput(args, cmd.Parameters);
            }
            if (BL.Config.Debug)
                Trace.WriteLine(sp + ": " + (DateTime.Now - t).TotalMilliseconds);
            return args.Length != 0 ? args[0] as int? ?? 0 : 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xact"></param>
        /// <param name="sp"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int Execute(SqlTransaction xact, string sp, params object[] args)
        {
            var t = DateTime.Now;
            var cmd = PrepareCommand(null, xact, sp, args);
            cmd.ExecuteNonQuery();
            HandleOutput(args, cmd.Parameters);
            if (BL.Config.Debug)
                Trace.WriteLine(sp + ": " + (DateTime.Now - t).TotalMilliseconds);
            return args.Length != 0 ? args[0] as int? ?? 0 : 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db">Identificador de la base de datos.</param>
        /// <param name="sp">Nombre del stored procedure.</param>
        /// <param name="args">Parámetros del stored procedure (en orden).</param>
        /// <returns>Un DataSet con los resultados.</returns>
        public static DataSet Query(string db, string sp, params object[] args)
        {
            var t = DateTime.Now;
            DataSet ds;
            using (var cn = GetConnection(db))
            {
                ds = new DataSet();
                var cmd = PrepareCommand(cn, null, sp, args);
                using (var dataAdapter = new SqlDataAdapter(cmd))
                    dataAdapter.Fill(ds);
                HandleOutput(args, cmd.Parameters);
                cn.Close();
            }
            if (BL.Config.Debug)
                Trace.WriteLine(sp + ": " + (DateTime.Now - t).TotalMilliseconds);
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xact"></param>
        /// <param name="sp"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static DataSet Query(SqlTransaction xact, string sp, params object[] args)
        {
            var t = DateTime.Now;
            var ds = new DataSet();
            var cmd = PrepareCommand(null, xact, sp, args);
            using (var dataAdapter = new SqlDataAdapter(cmd))
                dataAdapter.Fill(ds);
            HandleOutput(args, cmd.Parameters);
            if (BL.Config.Debug)
                Trace.WriteLine(sp + ": " + (DateTime.Now - t).TotalMilliseconds);
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sp"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int Execute(string db, string sp, object o)
        {
            var args = GetParams(db, null, sp, o);
            return Execute(db, sp, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xact"></param>
        /// <param name="sp"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int Execute(SqlTransaction xact, string sp, object o)
        {
            var args = GetParams(null, xact, sp, o);
            return Execute(xact, sp, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sp"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DataSet Query(string db, string sp, object o)
        {
            var args = GetParams(db, null, sp, o);
            return Query(db, sp, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xact"></param>
        /// <param name="sp"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        internal static DataSet Query(SqlTransaction xact, string sp, object o)
        {
            var args = GetParams(null, xact, sp, o);
            return Query(xact, sp, args);
        }

        /// <summary>
        /// Crea una transacción para ejecutar varios comandos SQL dentro del contexto de la misma.
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        /// <remarks>Es necesario ejecutar el método Commit o Rollbak de este objeto al final.</remarks>
        public static SqlTransaction GetTransaction(string db)
        {
            var cn = GetConnection(db);
            return cn.BeginTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ResetCache()
        {
            ParamCache.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        public static void ExecuteAdHoc(string db, string sql)
        {
            var t = DateTime.Now;
            var cn = GetConnection(db);
            var cmd = new SqlCommand(sql, cn);
            cmd.ExecuteNonQuery();
            cn.Close();
            if (BL.Config.Debug)
                Trace.WriteLine("\"" + sql.Substring(0, 30) + "\": " + (DateTime.Now - t).TotalMilliseconds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet QueryAdHoc(string db, string sql)
        {
            var t = DateTime.Now;
            DataSet ds;
            using (var cn = GetConnection(db))
            {
                ds = new DataSet();
                var da = new SqlDataAdapter(sql, cn);
                da.Fill(ds);
                cn.Close();
            }
            if (BL.Config.Debug)
                Trace.WriteLine("\"" + sql.Substring(0, 30) + "\": " + (DateTime.Now - t).TotalMilliseconds);
            return ds;
        }
    }
}
