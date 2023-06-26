using Npgsql;
using NpgsqlTypes;
using OdontoBackend.Domain.Contracts;
using OdontoBackend.Domain.Models;
using OdontoBackend.Infrastructure.Context;
using OdontoBackend.Infrastructure.Contracts.Context;
using OdontoBackend.Infrastructure.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static OdontoBackend.Infrastructure.Contracts.Entities.Packages;

namespace OdontoBackend.Infrastructure.Repository
{
    public class CatalogoRepository : ICatalogoRepository
    {
        protected readonly IContextDatabase _context;
        protected String error = null!;
        public CatalogoRepository(IContextDatabase context)
        {
            _context = context;
        }
        public Task<IQueryable<Catalogo>> GetCatalogoByCodUni(Catalogo request)
        {
            // var connection = _context.GetConnection();
            using (var connection = _context.GetConnection())
            {
                //NpgsqlCommand command = new NpgsqlCommand("SELECT 1 cod_catalogo,2 cod_uni_catalogo;", connection);
                //var  count = command.ExecuteScalar();
                //NpgsqlDataAdapter da = new NpgsqlDataAdapter();
                //DataSet ds = new DataSet();
                ////Debug.WriteLine("{0}\n", count);
                //NpgsqlTransaction tran = connection.BeginTransaction();
                ////using (conn = new NpgsqlConnection(dbconstr))
                ////{
                //    //conn.Open();
                //    using (da = new NpgsqlDataAdapter())
                //    {

                //        da.SelectCommand!.CommandType = CommandType.StoredProcedure;
                //        da.SelectCommand.CommandText = "CALL usp_bind";
                //        da.SelectCommand.Connection = connection;


                //        using (ds = new DataSet())
                //        {
                //            da.Fill(ds);
                //        }
                //    }
                //conn.Close();
                // }

                //// Define a command to call show_cities() procedure
                //NpgsqlCommand command = new NpgsqlCommand("g_catalogos.prueba3", connection);
                //command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.AddWithValue("aa", NpgsqlDbType.Refcursor).Direction = ParameterDirection.Output;
                //// Execute the procedure and obtain a result set
                //NpgsqlDataReader dr = command.ExecuteReader();

                //// Output rows 
                //while (dr.Read())
                //    Debug.WriteLine("{0} \t", dr[0]);

                ////tran.Commit();
                //// Start a transaction as it is required to work with cursors in PostgreSQL
                //NpgsqlTransaction tran = connection.BeginTransaction();

                //// Define a command to call stored procedure show_cities_multiple
                //NpgsqlCommand command = new NpgsqlCommand("select * from g_catalogos.show_cities_multiple()", connection);
                ////command.CommandType = CommandType.StoredProcedure;

                //// Execute the stored procedure and obtain the first result set
                //NpgsqlDataReader dr = command.ExecuteReader();

                //// Output the rows of the first result set
                //while (dr.Read())
                //    Console.Write("{0}\t{1} \n", dr[0], dr[1]);

                //// Switch to the second result set
                //dr.NextResult();

                //// Output the rows of the second result set
                //while (dr.Read())
                //    Console.Write("{0}\t{1} \n", dr[0], dr[1]);

                ////tran.Commit();
                ////connection.Open();
                //NpgsqlTransaction tran = connection.BeginTransaction();

                ////NpgsqlCommand command = new NpgsqlCommand("g_catalogos.show_cities_multiple", connection);
                ////command.CommandType = CommandType.StoredProcedure;
                ////NpgsqlDataReader dr1 = command.ExecuteReader();

                ////// Output the rows of the first result set
                ////while (dr1.Read())
                ////    Console.Write("{0}\t{1} \n", dr1[0]);
                //NpgsqlCommand command = new NpgsqlCommand("g_catalogos.prueba3", connection);
                //command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.AddWithValue("aa", NpgsqlDbType.Refcursor).Direction = ParameterDirection.Output;
                //// Execute the procedure and obtain a result set
                ////NpgsqlDataReader dr1 = command.ExecuteReader();

                ////// Output rows 
                ////while (dr1.Read())
                ////    Debug.WriteLine("{0} \t", dr1[0]);
                ////DataSet ds = new DataSet();
                ////NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);
                ////da.Fill(ds);

                //command.ExecuteNonQuery();

                //command.CommandText = "fetch all in \"<unnamed portal 1>\"";
                //command.CommandType = CommandType.Text;

                //NpgsqlDataReader dr = command.ExecuteReader();
                //DataTable dt = new DataTable();
                //dt.Load(dr);
                //while (dr.Read())
                //{
                //    // do what you want with data, convert this to json or...
                //    Console.WriteLine(dr[0]);
                //}
                //dr.Close();

                //tran.Commit();
                ////connection.Close();


                Debug.WriteLine(connection);
                NpgsqlTransaction tran = connection.BeginTransaction();
                var response = Enumerable.Empty<Catalogo>().AsQueryable();
                using (NpgsqlCommand cmd = connection.CreateCommand())
                {
                    //cmd.BindByName = true;
                    cmd.CommandText = UtilsContextDatabase.ToDescriptionString(Packages.pkg.g_catalogos) + "prueba3";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add("I_COD_UNI_CATALOGO", DbType.Int64).Value = request.COD_UNI_CATALOGO;
                    //cmd.Parameters.Add(new NpgsqlParameter("_itemID", NpgsqlDbType.Integer)).Value = 1;//request.COD_UNI_CATALOGO;
                    //{ Direction = ParameterDirection.Output });
                    //cmd.Parameters.Add(new NpgsqlParameter("_result_one", NpgsqlDbType.Refcursor) { Direction = ParameterDirection.InputOutput });
                    //cmd.Parameters.Add(new NpgsqlParameter("aa", NpgsqlDbType.Refcursor)).Direction = ParameterDirection.InputOutput;
                    //cmd.Parameters.Add(new NpgsqlParameter("f1", NpgsqlDbType.Numeric) { Direction = ParameterDirection.Output });
                    //cmd.Parameters.Add(new NpgsqlParameter("f2", NpgsqlDbType.Varchar, 500) { Direction = ParameterDirection.Output });
                    //cmd.Parameters.Add("O_CURSOR", DbType.RefCursor).Direction = ParameterDirection.Output;
                    //cmd.Parameters.Add("aa", NpgsqlDbType.Refcursor).Direction = ParameterDirection.Output;
                    //NpgsqlParameter p = new NpgsqlParameter();
                    //p.ParameterName = "aa";
                    //p.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Refcursor;
                    //p.Direction = ParameterDirection.InputOutput;
                    //p.Value = "aa";
                    //cmd.Parameters.Add(p);
                    cmd.Parameters.Add(new NpgsqlParameter("paa", NpgsqlDbType.Refcursor) { Direction = ParameterDirection.InputOutput, Value = "cursora" });
                    ///cmd.Parameters.AddWithValue("aa", NpgsqlDbType.Refcursor).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    //cmd.CommandText = "fetch all in \"<unnamed portal 1>\"";
                    cmd.CommandText = "fetch all in \"cursora\"";
                    cmd.CommandType = CommandType.Text;
                    response = _context.ExecuteList<Catalogo>(cmd, ref error);
                    //_context.CloseConnection(connection);
                    error = "";// cmd.Parameters["O_ERROR"].Value != null ? (string)cmd.Parameters["O_ERROR"].Value! : error = "";

                    if (error?.Length > 0) { tran.Rollback(); throw new Exception(error); };
                }
                tran.Commit();
                _context.CloseConnection(connection);

                //connection.Close();
                return Task.FromResult(response.Count() > 0 ? response : default!);
            }
        }


    }
}

