using FCS_WebSite_v2.Data.DB;
using FCS_WebSite_v2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace FCS_WebSite.Controllers
{
    [Route("login/")]
    public class LogInController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(IFormCollection userData)
        {
            var isPupil = DBObjects.GetPupil().SingleOrDefault(x =>
                x.Email == userData["email"].ToString() && x.Password ==
                    Hasher.Hash(userData["password"].ToString()));
            if (isPupil != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
