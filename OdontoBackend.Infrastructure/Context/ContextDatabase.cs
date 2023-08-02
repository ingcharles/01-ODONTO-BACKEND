using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using OdontoBackend.Aplicacion.Entities;
using OdontoBackend.Infrastructure.Contracts.Context;
using OdontoBackend.Infrastructure.Contracts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;
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
        public IQueryable<T> ExecuteListWithOneClass<T>(/*Packages.pkg pkg,*/ NpgsqlCommand comando, ref string err, object classes)
        {



            // using (comando.Connection = GetConnection())
            //{
            //comando.CommandText = /*UtilsContextDatabase.ToDescriptionString(pkg) +*/ comando.CommandText;
            NpgsqlDataReader reader = comando.ExecuteReader();
            if (reader.HasRows)
            {
                var _dataTable = new DataTable();
                _dataTable.Load(reader);
                //GenericOne<List<T>> a = new GenericOne<List<T>>();
                //List<RefreshToken> b = new List<RefreshToken>();
                //a.Add(b);
                var result = UtilsContextDatabase.ConvertToListWithOneClass<T>(_dataTable, classes).AsQueryable();
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
        //public IQueryable<IQueryable<T>> ExecuteLists<T>(Packages.pkg pkg, NpgsqlCommand comando, ref string err)
        //{
        //    var ds = new DataSet();
        //    using (comando.Connection = GetConnection())
        //    {
        //        comando.CommandText = UtilsContextDatabase.ToDescriptionString(pkg) + comando.CommandText;
        //        NpgsqlDataAdapter da = new NpgsqlDataAdapter(comando);
        //        da.Fill(ds);
        //        if (ds.Tables.Count > 0)
        //        {
        //            var result = new List<IQueryable<T>>();
        //            for (int i = 0; i < ds.Tables.Count; i++)
        //                result.Add(UtilsContextDatabase.ConvertToList<T>(ds.Tables[i]).AsQueryable());
        //            if (comando.Connection.State == ConnectionState.Open)
        //            {
        //                CloseConnection(comando.Connection);
        //                comando.Dispose();
        //            }
        //            return result.AsQueryable();
        //        }
        //        else
        //        {
        //            if (comando.Connection.State == ConnectionState.Open)
        //            {
        //                CloseConnection(comando.Connection);
        //                comando.Dispose();
        //            }
        //            return default;
        //        }
        //    }
        //}

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
                           
                        if (typeof(IEnumerable).IsAssignableFrom(pI.PropertyType) && pI.PropertyType != typeof(string))
                        {
                            //var ssss = pI.PropertyType;
                            ////CreateArray(itemType, capacity),
                            //List<object> objs = ((IEnumerable)row[pro.Name]).Cast<object>().ToList();
                            //Type containedType = t.GenericTypeArguments.First();
                            //Type keyType = t.GetGenericArguments()[0];

                            //// Create a list of the required type and cast to IList
                            ////Type genericListType = typeof(List<>);
                            ////Type concreteListType = genericListType.MakeGenericType(t);
                            ////IList list = Activator.CreateInstance(concreteListType) as IList;

                            ////// Add values
                            ////for (int i = 0; i < 50; i++)
                            ////{
                            ////    list.Add(Activator.CreateInstance(containedType));
                            ////}
                            //// Check we have a type that implements IList
                            //Type iListType = typeof(IList);
                            //if (!t.GetInterfaces().Contains(iListType))
                            //{
                            //    throw new ArgumentException("No IList", nameof(t));
                            //}

                            // Check we have a a generic type parameter and get it
                            Type elementType = t.GenericTypeArguments.FirstOrDefault();
                            if (elementType == null)
                            {
                                throw new ArgumentException("No Element Type", nameof(t));
                            }

                            // Create a list of the required type and cast to IList
                            //IList list = Activator.CreateInstance(t) as IList;

                            //// Add values
                            //for (int i = 0; i < 1; i++)
                            //{
                            //    list.Add(Activator.CreateInstance(elementType));
                            //    // var a = ;
                            //    //pro.SetValue(elementType, Activator.CreateInstance(elementType), null);
                            //}
                            //var values = Enumerable.Range(1, 1).Select(I => Activator.CreateInstance(t)).ToArray();
                            //object foos = Array.CreateInstance(elementType, 1);
                            //foreach (var item in values)
                            //{
                            //    var r = item;
                            //        // list.Add(item);
                            //}
                               object value = row[pro.Name];
                                //string jsonValue = JsonConvert.SerializeObject(value);
                                //object convertedValue = Convert.ChangeType(value, t);
                                Type listType = Type.GetType("System.Collections.Generic.List`1[DemoGenerics.CustomerInfo]");
                                // Get all the types in your assembly. For testing purpose, I am using Aspose.3D, you can use any.
                                var allTypes = Assembly.GetAssembly(typeof(T)).GetTypes();

                                foreach (var myType in allTypes)
                                {
                                    // Check if this type is subclass of your base class
                                    bool isSubType = myType.IsSubclassOf(typeof(T));

                                    // If it is sub-type, then print its name in Debug window.
                                    if (isSubType)
                                    {
                                        System.Diagnostics.Debug.WriteLine(myType.Name);
                                    }
                                }
                                List<Type> childClasses = allTypes
            .Where(t => t.IsSubclassOf(typeof(T)))
            .ToList();
                                var deserializedObject = JsonConvert.DeserializeObject<List<RefreshToken>>((string)value);
                                safeValue = Convert.ChangeType(deserializedObject, t);
                               // pro.SetValue(objT, safeValue, null);
                                //safeValue = Convert.ChangeType(row[pro.Name], elementType);
                               // pro.SetValue(elementType, deserializedObject, null);
                                // DO something with list which is now an List<t> filled with 50 ts
                                //var a =  list;
                                //Type valueType = t.GetGenericArguments()[1];
                                //safeValue = Convert.ChangeType(row[pro.Name], containedType);
                                //var s = objs.Select(item => Convert.ChangeType(item, containedType)).ToList();
                                //pro.SetValue(objT, row[pro.Name], null);
                                // pro.SetValue(iListType, list, null);
                                //safeValue = Convert.ChangeType(list, iListType);
                                //safeValue = Convert.ChangeType(list, iListType);


                            }
                            else
                            {
                                safeValue = Convert.ChangeType(row[pro.Name], t);
                            }

                            
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
                    else
                    {
                        PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                        Type t = Nullable.GetUnderlyingType(pI.PropertyType) ?? pI.PropertyType;
                    }
                }
                return objT;
            }).ToList();
        }
        public static List<T> ConvertToListWithOneClass<T>(DataTable dt, object clases)
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

                            if (typeof(IEnumerable).IsAssignableFrom(pI.PropertyType) && pI.PropertyType != typeof(string))
                            {
                 
                                // Check we have a a generic type parameter and get it
                                Type? elementType = t.GenericTypeArguments.FirstOrDefault();
                                if (elementType == null)
                                {
                                    throw new ArgumentException("No Element Type", nameof(t));
                                }

                                object value = row[pro.Name];
                            
                                dynamic? deserializedObject = JsonConvert.DeserializeObject((string)value, clases.GetType());
                                //var deserializedObject = JsonConvert.DeserializeObject<RefreshToken > ((string)value);
                                safeValue = Convert.ChangeType(deserializedObject, t);

                            }
                            else
                            {
                                safeValue = Convert.ChangeType(row[pro.Name], t);
                            }


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
                    else
                    {
                        PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                        Type t = Nullable.GetUnderlyingType(pI.PropertyType) ?? pI.PropertyType;
                    }
                }
                return objT;
            }).ToList();
        }
    }
    public class GenericOne<T>
    {
        public T[] Field;
    }
}
