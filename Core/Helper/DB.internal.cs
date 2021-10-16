using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace Ventus.Helper
{
    public partial class DB
    {
        private static readonly Hashtable ParamCache = Hashtable.Synchronized(new Hashtable());

        private static SqlConnection GetConnection(string db)
        {
            try
            {
                var config = ConfigurationManager.ConnectionStrings[db];
                var connString = config.ConnectionString;
                //connString = Util.Crypto.Decrypt(connString);
                var cn = new SqlConnection(connString);
                for (var retry = 0; retry < BL.Config.SqlConnectRetries; retry++)
                {
                    try
                    {
                        cn.Open();
                        break;
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.Message);
                        Log.Error(ex);
                    }
                }
                if (cn.State == ConnectionState.Open)
                    return cn;
                throw new ApplicationException("Demasiados reintentos");
            }
            catch (NullReferenceException ex)
            {
                throw new ApplicationException(string.Format("Database '{0}' undefined.", db), ex);
            }
            catch (FormatException ex)
            {
                throw new ApplicationException(string.Format("Error in database '{0}' definition.", db), ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("No se pudo conectar a la base de datos '{0}'.", db), ex);
            }
        }

        private static object[] GetParams(string db, SqlTransaction xact, string sp, object o)
        {
            if (o == null)
                return new object[] { null };
            var type = o.GetType();
            if (type.IsEnum)
                return new object[] { Convert.ToInt32(o) };
            if (type.Namespace.Equals("System"))
                return new[] { o };
            SqlConnection cn = null;
            if (xact == null) cn = GetConnection(db);
            var sqlParams = DiscoverParameters(cn, xact, sp);
            var props = BE.Properties(type).Where(q => q.CanRead).ToList();
            foreach (var param in sqlParams)
            {
                var prop = props.FirstOrDefault(p => param.ParameterName.Equals("@" + p.Name));
                if (prop != null) param.Value = prop.GetValue(o, null);
            }
            var args = sqlParams.Select(p => p.Value).ToArray();
            return args;
        }

        private static SqlCommand PrepareCommand(SqlConnection cn, SqlTransaction xact, string sp, object[] args)
        {
            var cmd = new SqlCommand(sp) { CommandType = CommandType.StoredProcedure, CommandTimeout = 0 };
            if (xact != null)
            {
                cmd.Transaction = xact;
                cn = xact.Connection;
            }
            cmd.Connection = cn;
            if (args == null || args.Length == 0) return cmd;
            var param = DiscoverParameters(cn, xact, sp);
            if (param != null)
            {
                if (param.Length != args.Length)
                    throw new ArgumentException("Parameter count does not match Parameter Value count.");

                for (int i = 0, j = param.Length; i < j; i++)
                {
                    var value = args[i] as IDbDataParameter;
                    if (value != null)
                    {
                        var paramInstance = value;
                        param[i].Value = paramInstance.Value ?? DBNull.Value;
                    }
                    else if (args[i] == null)
                        param[i].Value = DBNull.Value;
                    else
                    {
                        param[i].Value = args[i];
                        var array = args[i] as Array;
                        if (array != null && array.GetValue(0) is byte)
                            param[i].SqlDbType = SqlDbType.Image;
                    }
                }
            }
            if (param != null)
            {
                foreach (var p in param)
                {
                    if (p == null) continue;
                    if ((p.Direction == ParameterDirection.InputOutput ||
                         p.Direction == ParameterDirection.Input) &&
                        p.Value == null)
                    {
                        p.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(p);
                }
            }
            return cmd;
        }

        private static void HandleOutput(object[] parameterValues, SqlParameterCollection param)
        {
            if (param.Count != 0)
            {
                for (var i = 0; i < param.Count; i++)
                {
                    if (param[i].Direction == ParameterDirection.InputOutput ||
                        param[i].Direction == ParameterDirection.Output)
                    {
                        parameterValues[i] = param[i].Value;
                    }
                }
            }
        }

        private static SqlParameter[] DiscoverParameters(SqlConnection cn, SqlTransaction xact, string sp)
        {
            var hashKey = cn.ConnectionString + ":" + sp;
            var param = ParamCache[hashKey] as SqlParameter[];
            if (param == null)
            {
                var cmd = new SqlCommand(sp, cn)
                {
                    CommandTimeout = 0,
                    Transaction = xact,
                    CommandType = CommandType.StoredProcedure
                };
                SqlCommandBuilder.DeriveParameters(cmd);
                cmd.Parameters.RemoveAt(0); //remove @RETURN_VALUE
                param = new SqlParameter[cmd.Parameters.Count];
                cmd.Parameters.CopyTo(param, 0);
                foreach (var p in param)
                    p.Value = DBNull.Value;
                ParamCache[hashKey] = param;
            }
            var clone = new SqlParameter[param.Length];
            for (var i = 0; i < param.Length; i++)
                clone[i] = (SqlParameter)((ICloneable)param[i]).Clone();
            return clone;
        }
    }
}
