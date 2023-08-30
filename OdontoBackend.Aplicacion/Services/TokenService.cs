using Microsoft.AspNetCore.Http;

using OdontoBackend.Aplicacion.Entities;
using OdontoBackend.Aplicacion.Services.Contracts;

using OdontoBackend.Aplicacion.Token.Helper;
using OdontoBackend.Aplicacion.ViewModels.User;
using OdontoBackend.Aplication.Entities.Commands;
using OdontoBackend.Aplication.Entities.Queries;
using OdontoBackend.Aplication.Entities.Queries.User;
using OdontoBackend.Aplication.Entities.Validators;
using OdontoBackend.Common.Logs;
using OdontoBackend.Domain.Models;
using OdontoBackend.Services.Api.Configurations.Token.Helper;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplicacion.Services
{
    public class TokenService : ITokenService
    {
        //private HashSet<RefreshToken> refreshDbContext;
        //private readonly TasksDbContext tasksDbContext;
        //public TokenService(TasksDbContext tasksDbContext)
        //{
        //    this.tasksDbContext = tasksDbContext;
        //}
        private readonly IUserService _serviceUser;

        //UserQueryValidator? _validatorQueryUser;

        public TokenService(IUserService serviceUser)
        {
            _serviceUser = serviceUser;
        }
        public async Task<Tuple<string, string>> GenerateTokensAsync(UserViewModel user)
        {
            var accessToken = await TokenHelper.GenerateAccessToken(user);
            var refreshToken = await TokenHelper.GenerateRefreshToken();

           
            if (user == null)

                return null;
            
            var salt = PasswordHelper.GetSecureSalt();
            var refreshTokenHashed = PasswordHelper.HashUsingPbkdf2(refreshToken, salt);
    
            //if (user.refreshTokens != null && user.refreshTokens.Any())
            //{

            //    await RemoveRefreshTokenAsync(user);
            //}
                user.refreshTokens = new List<RefreshToken>();

            user.refreshTokens?.Add(new RefreshToken
            {
                ExpiryDate = DateTime.Now.AddDays(30),
                Ts = DateTime.Now,
                UserId = user.codigoUsuario,
                TokenHash = refreshTokenHashed,
                TokenSalt = Convert.ToBase64String(salt)
            });
            var userRecord = await _serviceUser.UpdateTokensCodUsuario(Task.FromResult(user));
            var token = new Tuple<string, string>(accessToken, refreshToken);
                return token;
            
        }
  

        public async Task<bool> RemoveRefreshTokenAsync(UserViewModel user)
        {
            //var user1 = new UserByCodUsuarioQuery();
            //user1.codigoUsuario = user.cod_usuario;
            ////var userRecord = await tasksDbContext.Users.Include(o => o.RefreshTokens).FirstOrDefaultAsync(e => e.cod_usuario == user.cod_usuario);
            //var userRecord = await _serviceUser.GetUserByCod(Task.FromResult(user1));
            ////var userRecord = _serviceUser.GenerateTokensCodUsuario(Task.FromResult(user1));
            //if (userRecord == null)
            ////if (userRecord.Result.Count(x => x.mensaje_logica == null) <= 0)
            //{
            //    return false;
            //}
            //if (userRecord.FirstOrDefault().RefreshTokens != null)//&& userRecord.RefreshTokens.Any()
            //{
            //    var currentRefreshToken = userRecord.FirstOrDefault();//.RefreshTokens.First();
            //    await RemoveRefreshTokenAsync(currentRefreshToken);
            // tasksDbContext.RefreshTokens.Remove(currentRefreshToken);
            var userRecord = await _serviceUser.UpdateTokensCodUsuario(Task.FromResult(user));
            //}
            return false;
        }
        public async Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            //var refreshToken = await tasksDbContext.RefreshTokens.FirstOrDefaultAsync(o => o.UserId == refreshTokenRequest.UserId);
            var user1 = new UserByCodQuery();
            user1.codigoUsuario = refreshTokenRequest.codigoUsuario;
            var refreshToken = await _serviceUser.GetUserByCod(Task.FromResult(user1));
            var response = new ValidateRefreshTokenResponse();
            if (refreshToken.Count() == 0)
            {
                response.Success = false;
                response.Error = "Sesión inválida o usuario ya cerrado";
                response.ErrorCode = "R02";
                return response;
            }
            var refreshTokenToValidateHash = PasswordHelper.HashUsingPbkdf2(refreshTokenRequest.refreshToken, Convert.FromBase64String(refreshToken.FirstOrDefault().refreshTokens.SingleOrDefault().TokenSalt));
            if (refreshToken.FirstOrDefault().refreshTokens.SingleOrDefault().TokenHash != refreshTokenToValidateHash)
            {
                response.Success = false;
                response.Error = "Token de actualización no válido";
                response.ErrorCode = "R03";
                return response;
            }

            if (refreshToken.FirstOrDefault().refreshTokens.SingleOrDefault().ExpiryDate < DateTime.Now)
            {
                response.Success = false;
                response.Error = "El token de actualización ha caducado";
                response.ErrorCode = "R04";
                return response;
            }
            response.Success = true;
            response.UserId = refreshToken.FirstOrDefault().codigoUsuario;
            response.nombreUsuario = refreshToken.FirstOrDefault().nombreUsuario;
            return response;
        }
    }
}
