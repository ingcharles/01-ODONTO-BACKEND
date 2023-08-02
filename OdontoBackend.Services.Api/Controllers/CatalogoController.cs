using Microsoft.AspNetCore.Mvc;
using OdontoBackend.Aplicacion.Services.Contracts;
using OdontoBackend.Aplication.Entities.Queries;
using OdontoBackend.Aplication.Entities.Validators;
using Swashbuckle.AspNetCore.Annotations;

namespace OdontoBackend.Services.Api.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class CatalogoController : Controller
    {

        private readonly ICatalogoService _serviceCatalogo;
        //CatalogoCommandValidator? _validatorCatalogo;
        //CatalogoQueryValidator? _validatorQueryCatalogo;
        public CatalogoController(ICatalogoService serviceCatalogo)
        {
            _serviceCatalogo = serviceCatalogo;
        }

        [HttpPost]
        [Route("GetCatalogoByCodUni")]
        [Produces("Application/Json", Type = typeof(object))]//CatalogoViewModel
        [SwaggerOperation(Summary = "Obtener información de un cátalogo por código único")]
        public IActionResult GetCatalogoByCodUni([FromBody] CatalogoByCodUniQuery request)
        {
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            var response = _serviceCatalogo.GetCatalogoByCodUni(Task.FromResult(request));
            return Ok(response.Result?.FirstOrDefault() ?? default!);
        }


    }
}
