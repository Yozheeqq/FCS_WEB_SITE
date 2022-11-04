using FCS_WebSite_v2.Data.DB;
using FCS_WebSite_v2.Data.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FCS_WebSite_v2.Controllers
{
    [Route("form/")]
    public class FormController : Controller
    {

        [Route("{id}")]
        public IActionResult Index(string id)
        {
            return View();
        }

        [HttpPost]
        [Route("myforms/")]
        public IActionResult MyForms()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Edit(IFormCollection fc)
        {
            Form form = new Form()
            {
                // id генерируется автоматически
                EventId = "tstevent", // тоже брать ивент из контекста
                CreatorId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value
            };
            DBObjects.InitialForm(form);
            return Redirect($"/form/edit/{form.Id}");
        }

        private DateTime ParseDate(string input)
        {
            var parsed = input.Split('T');
            var date = parsed[0];
            var time = parsed[1];
            var dateParse = date.Split('-').Select(x => int.Parse(x)).ToList();
            var timeParse = time.Split(':').Select(x => int.Parse(x)).ToList();
            return new DateTime(dateParse[0], dateParse[1],
                dateParse[2], timeParse[0],
                timeParse[1], 0);
        }
        
        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit([FromRoute] string id)
        {
            var form = DBObjects.GetForm().Where(x => x.Id == id).First();
            return View(form);
        }
    }
}
