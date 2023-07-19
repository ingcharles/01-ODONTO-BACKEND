using OdontoBackend.Common.Logs;
using Microsoft.AspNetCore.Mvc;
using OdontoBackend.Aplicacion.Services.Contracts;
using OdontoBackend.Aplication.Entities.Queries;
using OdontoBackend.Aplication.Entities.Validators;
using Swashbuckle.AspNetCore.Annotations;

namespace OdontoBackend.Services.Api.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class AuthController : Controller
    {

        private readonly IUserService _serviceUser;
        UserQueryValidator? _validatorUser;
        //UserQueryValidator? _validatorQueryUser;
        public AuthController(IUserService serviceUser)
        {
            _serviceUser = serviceUser;
        }

        [HttpPost]
        [Route("GetUserByCiPas")]
        [Produces("Application/Json", Type = typeof(object))]//UserViewModel
        [SwaggerOperation(Summary = "Obtener información del login de un usuario")]
        public IActionResult getUserByCiPas([FromBody] UserByCiPasQuery request)
        {
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            var response = _serviceUser.GetUserByCiPas(Task.FromResult(request));
            //return Ok(response.Result?.FirstOrDefault() ?? default!);
            if (response.Result.Count(x => x.mensajeLogica == null) > 0)
            {

                return Ok(new { data = response.Result.FirstOrDefault(), message = Messages._201Find, statusCode = StatusCodes.Status200OK, ok = true });
            }
            else
            {
                return Ok(new { message = response.Result.FirstOrDefault()?.mensajeLogica, statusCode = StatusCodes.Status404NotFound, ok = false });
            }
        }


    }

}
