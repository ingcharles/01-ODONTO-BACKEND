using Microsoft.Extensions.Configuration;
using Npgsql;
using OdontoBackend.Infrastructure.Contracts.Context;
using OdontoBackend.Infrastructure.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Infrastructure.Context
{
    public sealed class ContextDatabase : IContextDatabase
    {
        private readonly IConfiguration _configuration;
        public ContextDatabase(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void CloseConnection(NpgsqlConnection conn)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Dispose();
                conn.Close();
            }
        }


        public IQueryable<T>  ExecuteList<T>(/*Packages.pkg pkg,*/ NpgsqlCommand comando, ref string err)
        {

            

            // using (comando.Connection = GetConnection())
            //{
            //comando.CommandText = /*UtilsContextDatabase.ToDescriptionString(pkg) +*/ comando.CommandText;
                NpgsqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    var _dataTable = new DataTable();
                    _dataTable.Load(reader);
                    var result = UtilsContextDatabase.ConvertToList<T>(_dataTable).AsQueryable();
                //if (comando.Connection.State == ConnectionState.Open)
                //{
                //    CloseConnection(comando.Connection);
                //    comando.Dispose();
                //}
                
                return result;
                }
                else
                {
                err = "Registro no encontrado";
                //if (comando.Connection.State == ConnectionState.Open)
                //{
                //    CloseConnection(comando.Connection);
                //    comando.Dispose();
                //    NpgsqlConnection.ClearPool(comando.Connection);
                //}
                return new List<T>().AsQueryable();
                }
            //}
        }
        public IQueryable<IQueryable<T>> ExecuteLists<T>(Packages.pkg pkg, NpgsqlCommand comando, ref string err)
        {
            var ds = new DataSet();
            using (comando.Connection = GetConnection())
            {
                comando.CommandText = UtilsContextDatabase.ToDescriptionString(pkg) + comando.CommandText;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(comando);
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    var result = new List<IQueryable<T>>();
                    for (int i = 0; i < ds.Tables.Count; i++)
                        result.Add(UtilsContextDatabase.ConvertToList<T>(ds.Tables[i]).AsQueryable());
                    if (comando.Connection.State == ConnectionState.Open)
                    {
                        CloseConnection(comando.Connection);
                        comando.Dispose();
                    }
                    return result.AsQueryable();
                }
                else
                {
                    if (comando.Connection.State == ConnectionState.Open)
                    {
                        CloseConnection(comando.Connection);
                        comando.Dispose();
                    }
                    return default;
                }
            }
        }

        public bool ExecuteNoQuery(Packages.pkg pkg, NpgsqlCommand comando, ref string err)
        {
            bool band = false;
            using (comando.Connection = GetConnection())
            {
                comando.CommandText = UtilsContextDatabase.ToDescriptionString(pkg) + comando.CommandText;
                comando.ExecuteNonQuery();
                if (comando.Connection.State == ConnectionState.Open)
                {
                    CloseConnection(comando.Connection);
                    comando.Dispose();
                }
                band = true;
            }
            return band;
        }

        public NpgsqlConnection GetConnection()
        {
            var connectionString = _configuration.GetSection("ConnectionStrings").GetSection("PostgresDb").Value;
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
    public static class UtilsContextDatabase
    {
        public static string ToDescriptionString(Packages.pkg value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                    {
                        PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                        Type t = Nullable.GetUnderlyingType(pI.PropertyType) ?? pI.PropertyType;
                        object? safeValue = null;
                        if (row[pro.Name] == null || row[pro.Name] is DBNull)
                        {
                            safeValue = null;
                        }
                        else
                        {
                            safeValue = Convert.ChangeType(row[pro.Name], t);
                        }
                        //if (row[pro.Name] is DBNull)
                        //{
                        //    ab = 4;
                        //}
                        //else
                        //{
                        //    ab = 5;
                        //}

                        //object? safeValue = (row[pro.Name] == null) ? (row[pro.Name] is DBNull ?  null : Convert.ChangeType(row[pro.Name], t)): Convert.ChangeType(row[pro.Name], t);
                        pro.SetValue(objT, safeValue, null);
                        //pro.SetValue(objT, row[pro.Name] == DBNull.Value ? 
                        //    null : 
                        //    Convert.ChangeType(row[pro.Name], pI.PropertyType), null);
                    }
                }
                return objT;
            }).ToList();
        }
    }
}
