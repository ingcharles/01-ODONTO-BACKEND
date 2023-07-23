using OdontoBackend.Common.Logs;
using Microsoft.AspNetCore.Mvc;
using OdontoBackend.Aplicacion.Services.Contracts;
using OdontoBackend.Aplication.Entities.Queries;
using OdontoBackend.Aplication.Entities.Validators;
using Swashbuckle.AspNetCore.Annotations;
using OdontoBackend.Aplication.Entities.Commands;
using Microsoft.AspNetCore.Authorization;
using OdontoBackend.Aplicacion.Services;
using OdontoBackend.Domain.Models;
using OdontoBackend.Aplicacion.Mappers.Contracts;
using OdontoBackend.Domain.Contracts;
using OdontoBackend.Aplicacion.ViewModels;

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
            var response = await  this._serviceUser.GetUserByCiPas(Task.FromResult(request));
            if (response.Count(x => x.mensajeLogica == null) > 0)
            {
                var user = new User();
                user.cod_usuario = response.FirstOrDefault()?.codigoUsuario;
                user.RefreshTokens = response.FirstOrDefault()?.RefreshTokens;
                var token = await  this._tokenService.GenerateTokensAsync(user);
                var reponseToken = new TokenResponseViewModel();
                if (token!=null)
                 reponseToken = new TokenResponseViewModel
                {
                    accessToken = token.Item1,
                    refreshToken = token.Item2
                };
               
                
                return Ok(new { data = response.FirstOrDefault(), token = reponseToken, message = Messages._201Find, statusCode = StatusCodes.Status200OK, ok = true });
            }
            else
            {
                return Ok(new { message = response.FirstOrDefault()?.mensajeLogica, statusCode = StatusCodes.Status404NotFound, ok = false });
            }
        }
        [Authorize]
        [HttpPost]
        [Route("SaveRegisterUser")]
        [Produces("Application/Json", Type = typeof(object))]//UserViewModel
        [SwaggerOperation(Summary = "Obtener información del login de un usuario")]
        public IActionResult SaveRegisterUser([FromBody] UserCommand request)
        {
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            var response = _serviceUser.SaveRegisterUser(Task.FromResult(request));
            if (response.Result.Count(x => x.mensajeLogica == null) > 0)
            {
                return Ok(new { data = response.Result.FirstOrDefault(), message = Messages._201Created, statusCode = StatusCodes.Status200OK, ok = true });
            }
            else
            {
                return Ok(new { message = response.Result.FirstOrDefault()?.mensajeLogica, statusCode = StatusCodes.Status404NotFound, ok = false });
            }
        }


    }

}
