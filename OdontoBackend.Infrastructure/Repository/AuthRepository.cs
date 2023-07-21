﻿using Npgsql;
using NpgsqlTypes;
using OdontoBackend.Domain.Contracts;
using OdontoBackend.Domain.Models;
using OdontoBackend.Infrastructure.Context;
using OdontoBackend.Infrastructure.Contracts.Context;
using OdontoBackend.Infrastructure.Contracts.Entities;
using Oracle.ManagedDataAccess.Client;
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

        }
        public Task<IQueryable<UserCommandFrom>> SaveRegisterUser(User request)
        {

            using (var scope = new TransactionScope())
            {
                using (var connection = _context.GetConnection())
                {
                   
                    var result = Enumerable.Empty<UserCommandFrom>().AsQueryable();
                    UserCommandFrom resultResponse = new UserCommandFrom();
                    using (NpgsqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = UtilsContextDatabase.ToDescriptionString(Packages.pkg.esq_usuarios) + "save_register_user";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("i_dni_usuario", NpgsqlDbType.Varchar).Value = request.dni_usuario;
                        cmd.Parameters.Add("i_pas_usuario", NpgsqlDbType.Varchar).Value = request.pas_usuario;
                        cmd.Parameters.Add("i_mai_usuario", NpgsqlDbType.Varchar).Value = request.mai_usuario;
                        cmd.Parameters.Add("i_nom_usuario", NpgsqlDbType.Varchar).Value = request.nom_usuario;
                        cmd.Parameters.Add("i_ape_usuario", NpgsqlDbType.Varchar).Value = request.ape_usuario;
                        cmd.Parameters.Add("i_lic_agr_usuario", NpgsqlDbType.Boolean).Value = request.lic_agr_usuario;
                        cmd.Parameters.Add("i_is_pro_usuario", NpgsqlDbType.Boolean).Value = request.is_pro_usuario;
                        cmd.Parameters.Add("i_is_cli_usuario", NpgsqlDbType.Boolean).Value = request.is_cli_usuario;
                        //cmd.Parameters.Add("cur_splogin", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(new NpgsqlParameter("o_cod_usuario", NpgsqlDbType.Bigint) { Direction = ParameterDirection.InputOutput, Value = 0 });
                        cmd.Parameters.Add(new NpgsqlParameter("o_error", NpgsqlDbType.Varchar) { Direction = ParameterDirection.InputOutput, Value = "" });
                        int response = cmd.ExecuteNonQuery();
                        //error = (OracleString)cmd.Parameters["o_error"].Value! != null ? (string)(OracleString)cmd.Parameters["o_error"].Value! : "";
                        //codigoUsuario = (OracleDecimal?)cmd.Parameters["O_COD_CRCA"].Value != null ? (Int64)(OracleDecimal?)cmd.Parameters["O_COD_CRCA"].Value : null;
                        // var response = _context.ExecuteNoQuery(Packages.pkg.PKG_CRCA, cmd, ref error);
                        //_context.CloseConnection(connection);
                        scope.Complete();
                        connection.Close();
                        error = (string)cmd.Parameters["o_error"].Value!;
                   //: "";
                        if (error?.Length > 0)
                        {
                            resultResponse.mensaje_logica = error;
                        }
                        else
                        {
                            resultResponse.cod_usuario = (Int64)cmd.Parameters["o_cod_usuario"].Value!;
                            //request.CLASE_CRCA.NUM_CRCA = (OracleString?)cmd.Parameters["O_NUM_CRCA"].Value != null ? (string)(OracleString?)cmd.Parameters["O_NUM_CRCA"].Value : null;
                        }
                        //throw new Exception(error);
                        result = response == -1 ? result.Append(resultResponse) : default!;
                        //error = (OracleString)cmd.Parameters["O_ERROR"].Value != null ? (string)(OracleString)cmd.Parameters["O_ERROR"].Value
                        //    : "";
                        //if (error?.Length > 0)
                        //{
                        //    request.MENSAJE_LOGICA = error;
                        //}
                        ////throw new Exception(error);
                        //result = response is true ? result.Append(request) : default!;
                        //var response = Enumerable.Empty<UserCommandFrom>().AsQueryable();

                        //response = _context.ExecuteList<UserCommandFrom>(cmd1, ref error);
                        //scope.Complete();

                        //if (error?.Length > 0)
                        //    tasks.mensaje_logica = error;
                        //else
                        //    tasks.cod_usuario = codigoUsuario;

                        //response = response == null ? response!.Append(tasks) : default!;
                    }
                    
                    return Task.FromResult(result);
                }
            }
        }


        //public Task<IQueryable<UserCommandFrom>> SaveRegisterUser(User request)
        //{

        //    using (var scope = new TransactionScope())
        //    {
        //        using (var connection = _context.GetConnection())
        //        {
        //            UserCommandFrom tasks = new UserCommandFrom();
        //            Int64? codigoUsuario = null;
        //            using (NpgsqlCommand cmd = connection.CreateCommand())
        //            {
        //                cmd.CommandText = UtilsContextDatabase.ToDescriptionString(Packages.pkg.esq_usuarios) + "save_register_user";
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.Add("i_dni_usuario", NpgsqlDbType.Varchar).Value = request.dni_usuario;
        //                cmd.Parameters.Add("i_pas_usuario", NpgsqlDbType.Varchar).Value = request.pas_usuario;
        //                cmd.Parameters.Add("i_nom_usuario", NpgsqlDbType.Varchar).Value = request.nom_usuario;
        //                cmd.Parameters.Add("i_ape_usuario", NpgsqlDbType.Varchar).Value = request.ape_usuario;
        //                cmd.Parameters.Add("i_lic_agr_usuario", NpgsqlDbType.Boolean).Value = request.lic_agr_usuario;
        //                cmd.Parameters.Add("i_is_pro_usuario", NpgsqlDbType.Boolean).Value = request.is_pro_usuario;
        //                cmd.Parameters.Add("i_is_cli_usuario", NpgsqlDbType.Boolean).Value = request.is_cli_usuario;
        //                cmd.Parameters.Add(new NpgsqlParameter("o_cursor", NpgsqlDbType.Refcursor) { Direction = ParameterDirection.InputOutput, Value = "o_cursor_one" });
        //                cmd.Parameters.Add(new NpgsqlParameter("o_error", NpgsqlDbType.Varchar) { Direction = ParameterDirection.Output });
        //                cmd.ExecuteNonQuery();
        //                error = (OracleString)cmd.Parameters["o_error"].Value! != null ? (string)(OracleString)cmd.Parameters["o_error"].Value! : "";
        //                codigoUsuario = (OracleDecimal?)cmd.Parameters["O_COD_CRCA"].Value != null ? (Int64)(OracleDecimal?)cmd.Parameters["O_COD_CRCA"].Value : null;

        //            }
        //            var response = Enumerable.Empty<UserCommandFrom>().AsQueryable();

        //            using (NpgsqlCommand cmd1 = connection.CreateCommand())
        //            {

        //                cmd1.CommandText = "fetch all in \"o_cursor_one\"";
        //                cmd1.CommandType = CommandType.Text;


        //                response = _context.ExecuteList<UserCommandFrom>(cmd1, ref error);
        //                scope.Complete();

        //                if (error?.Length > 0)
        //                    tasks.mensaje_logica = error;
        //                else
        //                    tasks.cod_usuario = codigoUsuario;

        //                response = response == null ? response!.Append(tasks) : default!;
        //            }
        //            return Task.FromResult(response);
        //        }
        //    }
        //}
    }
}


