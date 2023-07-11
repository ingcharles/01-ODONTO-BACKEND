using Microsoft.AspNetCore.Mvc;
using OdontoBackend.Aplicacion.Services.Contracts;
using OdontoBackend.Aplication.Entities.Queries;
using OdontoBackend.Aplication.Entities.Validators;
using Swashbuckle.AspNetCore.Annotations;

namespace OdontoBackend.Services.Api.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IUserService _serviceUser;
        UserQueryValidator? _validatorUser;
        //UserQueryValidator? _validatorQueryUser;
        public UserController(IUserService serviceUser)
        {
            _serviceUser = serviceUser;
        }

        [HttpPost]
        [Route("GetUserByCodPas")]
        [Produces("Application/Json", Type = typeof(object))]//UserViewModel
        [SwaggerOperation(Summary = "Obtener información del login de un usuario")]
        public IActionResult GetUserByCodPas([FromBody] UserByCodPasQuery request)
        {
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            var response = _serviceUser.GetUserByCodPas(Task.FromResult(request));
            return Ok(response.Result?.FirstOrDefault() ?? default!);
        }


    }

}
