using Microsoft.AspNetCore.Mvc;

namespace OdontoBackend.Services.Api.Controllers
{
    public class MailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
