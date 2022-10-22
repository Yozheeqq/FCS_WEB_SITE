using Microsoft.AspNetCore.Mvc;

namespace FCS_WebSite.Controllers
{
    public class TestController : Controller
    {
        // 
        // GET: /HelloWorld/

        public IActionResult Index()
        {
            return View();
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        public IActionResult Welcome(string name, int number = 1)
        {
            ViewData["Message"] = $"Hello {name}";
            ViewData["Number"] = number;

            return View();
        }
    }
}
