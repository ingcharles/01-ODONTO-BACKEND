using Npgsql;
using NpgsqlTypes;
using OdontoBackend.Domain.Contracts;
using OdontoBackend.Domain.Models;
using OdontoBackend.Infrastructure.Context;
using OdontoBackend.Infrastructure.Contracts.Context;
using OdontoBackend.Infrastructure.Contracts.Entities;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static OdontoBackend.Infrastructure.Contracts.Entities.Packages;

namespace OdontoBackend.Infrastructure.Repository
{
    public class AuthRepository : IUserRepository
    {
        protected readonly IContextDatabase _context;
        protected String error = null!;
        public AuthRepository(IContextDatabase context)
        {
            _context = context;
        }

        public Task<IQueryable<User>> GetUserByCiPas(User request)
        {

            using (var scope = new TransactionScope())
            {
                using (var connection = _context.GetConnection())
                {
                    using (NpgsqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = UtilsContextDatabase.ToDescriptionString(Packages.pkg.esq_usuarios) + "get_user_by_cod_pas";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("i_dni_usuario", NpgsqlDbType.Varchar).Value = request.dni_usuario;
                        cmd.Parameters.Add("i_pas_usuario", NpgsqlDbType.Varchar).Value = request.pas_usuario;
                        cmd.Parameters.Add(new NpgsqlParameter("o_cursor", NpgsqlDbType.Refcursor) { Direction = ParameterDirection.InputOutput, Value = "o_cursor_one" });
                        cmd.ExecuteNonQuery();





                    }
                    var response = Enumerable.Empty<User>().AsQueryable();
                    using (NpgsqlCommand cmd1 = connection.CreateCommand())
                    {
                        //cmd.CommandText = "fetch all in \"<unnamed portal 1>\"";
                        cmd1.CommandText = "fetch all in \"o_cursor_one\"";
                        cmd1.CommandType = CommandType.Text;


                         response = _context.ExecuteList<User>(cmd1, ref error);
                        scope.Complete();
                        //_context.CloseConnection(connection);
                        //error = (OracleString)cmd.Parameters["O_ERROR"].Value != null ? (string)(OracleString)cmd.Parameters["O_ERROR"].Value
                        //    : "";
                        //if (error?.Length > 0) throw new Exception(error);
                        if (error?.Length > 0)
                        {
                            request.mensaje_logica = error;
                        }
                        //result = response.Count() > 0 ? response.Append(request) : result1.Append(request);
                    }
                    return Task.FromResult(response.Append(request));
                    //return Task.FromResult(response.Count() > 0 ? response : request);
                    // return Task.FromResult(response.Count() > 0 ? response : default!);
                    //     error = (OracleString)cmd.Parameters["O_ERROR"].Value != null ? (string)(OracleString)cmd.Parameters["O_ERROR"].Value
                    //: "";
                    //if (error?.Length > 0)
                    //{
                    //    request.mensaje_logica = error;
                    //}
                    //else
                    //{
                    //    request.CLASE_CRCA.COD_CRCA = (OracleDecimal?)cmd.Parameters["O_COD_CRCA"].Value != null ? (Int64)(OracleDecimal?)cmd.Parameters["O_COD_CRCA"].Value : null;
                    //}
                    ////throw new Exception(error);
                    //result = response is true ? result.Append(request) : default!;


                }

            }
            //public Task<IQueryable<User>> getUserByCiPas(User request)
            //{

            //    using (var scope = new TransactionScope())
            //    {
            //        using (var connection = _context.GetConnection())
            //        {
            //            using (NpgsqlCommand cmd = connection.CreateCommand())
            //            {
            //                cmd.CommandText = UtilsContextDatabase.ToDescriptionString(Packages.pkg.esq_usuarios) + "get_user_by_cod_pas";
            //                cmd.CommandType = CommandType.StoredProcedure;
            //                cmd.Parameters.Add("i_dni_usuario", NpgsqlDbType.Varchar).Value = request.dni_usuario;
            //                cmd.Parameters.Add("i_pas_usuario", NpgsqlDbType.Varchar).Value = request.pas_usuario;
            //                cmd.Parameters.Add(new NpgsqlParameter("o_cursor", NpgsqlDbType.Refcursor) { Direction = ParameterDirection.InputOutput, Value = "o_cursor_one" });
            //                cmd.ExecuteNonQuery();





            //            }
            //            var response = Enumerable.Empty<User>().AsQueryable();
            //            using (NpgsqlCommand cmd1 = connection.CreateCommand())
            //            {
            //                //cmd.CommandText = "fetch all in \"<unnamed portal 1>\"";
            //                cmd1.CommandText = "fetch all in \"o_cursor_one\"";
            //                cmd1.CommandType = CommandType.Text;


            //                response = _context.ExecuteList<User>(cmd1, ref error);
            //                scope.Complete();
            //                return Task.FromResult(response.Count() > 0 ? response : default!);
            //           //     error = (OracleString)cmd.Parameters["O_ERROR"].Value != null ? (string)(OracleString)cmd.Parameters["O_ERROR"].Value
            //           //: "";
            //           //     if (error?.Length > 0)
            //           //     {
            //           //         request.mensaje_logica = error;
            //           //     }
            //           //     else
            //           //     {
            //           //         request.CLASE_CRCA.COD_CRCA = (OracleDecimal?)cmd.Parameters["O_COD_CRCA"].Value != null ? (Int64)(OracleDecimal?)cmd.Parameters["O_COD_CRCA"].Value : null;
            //           //     }
            //           //     //throw new Exception(error);
            //           //     result = response is true ? result.Append(request) : default!;
            //            }

            //        }

            //    }

            //using (var connection = _context.GetConnection())
            //{

            //    Debug.WriteLine(connection);
            //    NpgsqlTransaction tran = connection.BeginTransaction();


            //        var response = Enumerable.Empty<User>().AsQueryable();
            //        //connection.Close();
            //        using (NpgsqlCommand cmd = connection.CreateCommand())
            //        {
            //            //cmd.BindByName = true;
            //            cmd.CommandText = UtilsContextDatabase.ToDescriptionString(Packages.pkg.esq_usuarios) + "get_user_by_cod_pas";
            //            cmd.CommandType = CommandType.StoredProcedure;
            //            cmd.Parameters.Add("i_dni_usuario", NpgsqlDbType.Varchar).Value = request.dni_usuario;
            //            cmd.Parameters.Add("i_pas_usuario", NpgsqlDbType.Varchar).Value = request.pas_usuario;
            //            //cmd.Parameters.Add(new NpgsqlParameter("_itemID", NpgsqlDbType.Integer)).Value = 1;//request.COD_UNI_User;
            //            //{ Direction = ParameterDirection.Output });
            //            //cmd.Parameters.Add(new NpgsqlParameter("_result_one", NpgsqlDbType.Refcursor) { Direction = ParameterDirection.InputOutput });
            //            //cmd.Parameters.Add(new NpgsqlParameter("aa", NpgsqlDbType.Refcursor)).Direction = ParameterDirection.InputOutput;
            //            //cmd.Parameters.Add(new NpgsqlParameter("f1", NpgsqlDbType.Numeric) { Direction = ParameterDirection.Output });
            //            //cmd.Parameters.Add(new NpgsqlParameter("f2", NpgsqlDbType.Varchar, 500) { Direction = ParameterDirection.Output });
            //            //cmd.Parameters.Add("O_CURSOR", DbType.RefCursor).Direction = ParameterDirection.Output;
            //            //cmd.Parameters.Add("aa", NpgsqlDbType.Refcursor).Direction = ParameterDirection.Output;
            //            //NpgsqlParameter p = new NpgsqlParameter();
            //            //p.ParameterName = "aa";
            //            //p.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Refcursor;
            //            //p.Direction = ParameterDirection.InputOutput;
            //            //p.Value = "aa";
            //            //cmd.Parameters.Add(p);
            //            cmd.Parameters.Add(new NpgsqlParameter("o_cursor", NpgsqlDbType.Refcursor) { Direction = ParameterDirection.InputOutput, Value = "o_cursor_one" });
            //            ///cmd.Parameters.AddWithValue("aa", NpgsqlDbType.Refcursor).Direction = ParameterDirection.Output;
            //            cmd.ExecuteNonQuery();

            //            //cmd.CommandText = "fetch all in \"<unnamed portal 1>\"";
            //            cmd.CommandText = "fetch all in \"o_cursor_one\"";
            //            cmd.CommandType = CommandType.Text;
            //        cmd.Transaction = tran;
            //        tran.Commit();
            //        //cmd.ExecuteNonQuery();
            //        response = _context.ExecuteList<User>(cmd, ref error);

            //        //    //_context.CloseConnection(connection);
            //        //    //error = "";// cmd.Parameters["O_ERROR"].Value != null ? (string)cmd.Parameters["O_ERROR"].Value! : error = "";

            //        //    if (error?.Length > 0)
            //        //    {
            //        //        //tran.Rollback(); throw new Exception(error);
            //        //    }
            //        //    else
            //        //    {
            //        //       // tran.Commit();
            //        //    };

            //        }
            //    //tran.Commit();
            //    return default;// Task.FromResult(response.Count() > 0 ? response : default!);
            //        //your codes having errors





            //    //connection.Dispose();


            //    //connection.Close();
            //    _context.CloseConnection(connection);

            //    //connection.Close();


            //}
        }


    }
}

