using Microsoft.AspNetCore.Mvc;
using System.IO;
using FCS_WebSite.Models;
using FCS_WebSite.Services;
using System.Text.Json;

namespace FCS_WebSite.Controllers
{
    public class RegistrationController : Controller
    {
        [RequireHttps]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Pupil()
        {

            return View();
        }

        [HttpPost]
        public IActionResult RegistrationPupil(string email, string password, string passwordConfirm)
        {
            if(password != passwordConfirm)
            {
                return View("Error");
            }
            Pupil pupil = new Pupil
            {
                Email = email,
                Password = password,
                LastName = "Khasanov",
                FirstName = "Anvar",
                Id = 0
            };
            using (StreamWriter sw = new StreamWriter(@"C:\Users\anvar\Desktop\study\output.txt"))
            {
                sw.Write(JsonSerializer.Serialize(pupil));
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Teacher()
        {
            return View();
        }
    }
}
