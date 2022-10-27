using Microsoft.AspNetCore.Mvc;
using System.IO;
using FCS_WebSite.Models;
using FCS_WebSite.Services;
using System.Text.Json;

namespace FCS_WebSite.Controllers
{
    public class RegistrationController : Controller
    {
        public RegistrationController(JsonPupil pupil, JsonTeacher teacher)
        {
            _pupilJson = pupil;
            _teacherJson = teacher;
        }

        private JsonPupil _pupilJson
        {
            set;
            get;
        }

        private JsonTeacher _teacherJson
        {
            set;
            get;
        }

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
        public IActionResult Teacher()
        {

            return View();
        }

        [HttpPost]
        public IActionResult RegistrationPupil(IFormCollection fc)
        {
            // string firstName, string lastName, 
            // string email, string password, string passwordConfirm,
            /*
            if (password != passwordConfirm)
            {
                return View("Error");
            }
            Pupil pupil = new Pupil
            {
                Email = email,
                Password = password,
                LastName = lastName,
                FirstName = firstName,
                Id = 0
            };
            
            _pupilJson.WritePerson(pupil);*/
            return Ok();
        }

        [HttpGet]
        public IActionResult RegistrationTeacher(string firstName, string lastName,
            string email, string password, string passwordConfirm, string selfCode)
        {
            Teacher teacher = new Teacher
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                Code = selfCode
            };
            _teacherJson.WritePerson(teacher);
            return Ok();
        }
    }
}
