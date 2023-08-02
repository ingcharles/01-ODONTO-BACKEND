using OdontoBackend.Common.Logs;
using Microsoft.AspNetCore.Mvc;
using OdontoBackend.Aplicacion.Services.Contracts;
using OdontoBackend.Aplication.Entities.Validators;
using Swashbuckle.AspNetCore.Annotations;
using OdontoBackend.Aplication.Entities.Commands;
using Microsoft.AspNetCore.Authorization;
using OdontoBackend.Aplicacion.Services;
using OdontoBackend.Domain.Models;
using OdontoBackend.Aplicacion.Mappers.Contracts;
using OdontoBackend.Domain.Contracts;
using OdontoBackend.Aplicacion.ViewModels;
using OdontoBackend.Aplication.Entities.Queries.User;
using Newtonsoft.Json.Linq;

namespace OdontoBackend.Services.Api.Controllers
{

    [Route("[controller]")]
    [ApiController]

    public class AuthController : Controller
    {

        private readonly IUserService _serviceUser;
        private readonly ITokenService _tokenService;
        //UserQueryValidator? _validatorUser;
        //UserQueryValidator? _validatorQueryUser;
 
        //public AuthController(IUserService serviceUser)
        //{
        //    _serviceUser = serviceUser;
        //}

       
        public AuthController(IUserService serviceUser, ITokenService tokenService)
        {
            this._serviceUser = serviceUser;
            this._tokenService = tokenService;
        }

        [HttpPost]
        [Route("GetUserByCiPas")]
        [Produces("Application/Json", Type = typeof(object))]//UserViewModel
        [SwaggerOperation(Summary = "Obtener información del login de un usuario")]
        public async Task<IActionResult> getUserByCiPas([FromBody] UserByCiPasQuery request)
        {
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            var response = await  _serviceUser.GetUserByCiPas(Task.FromResult(request));
            if (response.Count(x => x.mensajeLogica == null) > 0)
            {
                var user = new UserViewModel();
                user.codigoUsuario = response.FirstOrDefault()?.codigoUsuario;
                user.nombreUsuario = response.FirstOrDefault()?.nombreUsuario;
                //user.refreshTokens = response.FirstOrDefault()?.refreshTokens;
                var token = await  _tokenService.GenerateTokensAsync(user);
                var reponseToken = new TokenResponseViewModel();
                if (token!=null)
                 reponseToken = new TokenResponseViewModel
                {
                    accessToken = token.Item1,
                    refreshToken = token.Item2
                };

       
                return Ok(new { data = new { response.FirstOrDefault()?.codigoUsuario, response.FirstOrDefault()?.nombreUsuario }, token = reponseToken, message = Messages._201Find, statusCode = StatusCodes.Status200OK, ok = true });
            }
            else
            {
                return Ok(new { message = response.FirstOrDefault()?.mensajeLogica, statusCode = StatusCodes.Status404NotFound, ok = false });
            }
        }

        
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            //return Ok(new { message = "aass", statusCode = StatusCodes.Status400BadRequest, ok = false });
            if (refreshTokenRequest == null || string.IsNullOrEmpty(refreshTokenRequest.refreshToken) || refreshTokenRequest.codigoUsuario == 0)
            {
                return Ok(new { message = "Faltan detalles del token de actualización", statusCode = StatusCodes.Status400BadRequest, ok = false });

                //return BadRequest(new TokenResponse
                //{
                //    Error = "Missing refresh token details",
                //    ErrorCode = "R01"
                //});
            }
            var validateRefreshTokenResponse = await _tokenService.ValidateRefreshTokenAsync(refreshTokenRequest);
            if (!validateRefreshTokenResponse.Success)
            {
                //return UnprocessableEntity(validateRefreshTokenResponse);
                return Ok(new { message = validateRefreshTokenResponse.Error, statusCode = StatusCodes.Status422UnprocessableEntity, ok = false });
            }
            var user = new UserViewModel();
            user.codigoUsuario = validateRefreshTokenResponse.UserId;

            var tokenResponse = await _tokenService.GenerateTokensAsync(user);
            var reponseToken = new TokenResponseViewModel();
            if (tokenResponse != null)
                reponseToken = new TokenResponseViewModel
                {
                    accessToken = tokenResponse.Item1,
                    refreshToken = tokenResponse.Item2
                };

            return Ok(new { data = new { codigoUsuario = validateRefreshTokenResponse.UserId, nombreUsuario = validateRefreshTokenResponse.nombreUsuario }, token = reponseToken, message = Messages._201Find, statusCode = StatusCodes.Status200OK, ok = true });


        }


        [Authorize]
        [HttpPost]
        [Route("SaveRegisterUser")]
        [Produces("Application/Json", Type = typeof(object))]//UserViewModel
        [SwaggerOperation(Summary = "Obtener información del login de un usuario")]
        public IActionResult SaveRegisterUser([FromBody] UserCommand request)
        {
            return Ok(new { data = "bien", message = Messages._201Created, statusCode = StatusCodes.Status200OK, ok = true });
        
            //if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            //var response = _serviceUser.SaveRegisterUser(Task.FromResult(request));
            //if (response.Result.Count(x => x.mensajeLogica == null) > 0)
            //{
            //    return Ok(new { data = response.Result.FirstOrDefault(), message = Messages._201Created, statusCode = StatusCodes.Status200OK, ok = true });
            //}
            //else
            //{
            //    return Ok(new { message = response.Result.FirstOrDefault()?.mensajeLogica, statusCode = StatusCodes.Status404NotFound, ok = false });
            //}
        }

        [HttpPost]
        [Route("GetUserByCod")]
        [Produces("Application/Json", Type = typeof(object))]//UserViewModel
        [SwaggerOperation(Summary = "Obtener información del login de un usuario")]
        public async Task<IActionResult> GetUserByCod([FromBody] UserByCodQuery request)
        {
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            var response = await _serviceUser.GetUserByCod(Task.FromResult(request));
            if (response.Count(x => x.mensajeLogica == null) > 0)
            {
             
                return Ok(new { data = response.FirstOrDefault()?.codigoUsuario, message = Messages._201Find, statusCode = StatusCodes.Status200OK, ok = true });
            }
            else
            {
                return Ok(new { message = response.FirstOrDefault()?.mensajeLogica, statusCode = StatusCodes.Status404NotFound, ok = false });
            }
        }


    }

}
