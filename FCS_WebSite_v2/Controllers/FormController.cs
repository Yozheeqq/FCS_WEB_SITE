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
        public IActionResult Create(IFormCollection fc)
        {
            Random rnd = new Random();
            Form form = new Form()
            {
                Id = rnd.Next().ToString(), // заменить
                Name = fc["name"],
                StartDate = new DateTime(2002, 10, 31), // заменить
                EndDate = new DateTime(2002, 11, 12), // заменить
                EventId = "tstevent", // тоже брать ивент из контекста
                CreatorId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value
            };

            DBObjects.InitialForm(form);
            ViewBag.id = form.Id;
            return Redirect($"/form/create/{form.Id}");
        }

        [HttpGet]
        [Route("create/{id}")]
        public IActionResult Create([FromRoute]string id)
        {
           var form =  DBObjects.GetForm().Where(x => x.Id == id).First();
            return View(form);  
        }
    }
}
