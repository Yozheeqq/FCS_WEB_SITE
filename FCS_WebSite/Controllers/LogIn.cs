using Microsoft.AspNetCore.Mvc;

namespace FCS_WebSite.Controllers
{
    public class LogIn : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CheckCorrect(string email, string password)
        {

            return Ok();
        }
    }
}
