using Microsoft.AspNetCore.Mvc;
using FCS_WebSite_v2.Data.Forms;
using System.Security.Claims;
using FCS_WebSite_v2.Data.DB;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FCS_WebSite_v2.Controllers
{
    [Route("profile/")]
    public class ProfileController : Controller
    {

        private string? CurrentEventId { get; set; }
        
        public IActionResult Index()
        {
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            // Все меро, куда зарегался ученик
            var availableEvents = DBObjects.GetUserEvents().Where(x =>
                x.UserId == userId
            ).Select(x => x.EventId);

            // Все доступные формы для ученика
            var availableForms = DBObjects.GetForm().Where(x => 
            availableEvents.Contains(x.EventId) && x.IsRegistration == 0).ToList();

            var events = DBObjects.GetEvents().ToList();
            var allForms = new List<Form>();
            


            var customForm = from e in events
                       join af in availableForms on e.Id equals af.EventId
                       orderby af.EventId
                       select new
                       {
                           EventName = e.Name,
                           FormName = af.Name,
                           FormId = af.Id
                       };

            var allPupils = DBObjects.GetPupil().OrderBy(x => x.LastName).ToList();

            var model = (customForm, events, allForms, allPupils);

            return View(model);
        }

        [HttpGet]
        [Route("myforms/")]
        public IActionResult MyForms()
        {
            var forms = LoadUserForms();
            return View(forms);
        }

        private List<Form> LoadUserForms()
        {
            var forms = DBObjects.GetForm().Where(x=> x.Name != null && x.CreatorId ==
                ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value).ToList();
            return forms;
        }

        [HttpPost]
        [Route("/create_new_event")]
        public IActionResult CreateNewEvent(IFormCollection fc)
        {
            string eventName = fc["eventName"];
            Event event_ = new Event()
            {
                Name = eventName,
                CreatorId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value,
                IsAvailable = 1
            };
            DBObjects.InitialEvent(event_);
            return Redirect("/profile");
        }

        [HttpGet]
        [Route("{eventName}")]
        public IActionResult FormWithCurrentEvent(string eventName)
        {
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var availableEvents = DBObjects.GetUserEvents().Where(x =>
                x.UserId == userId
            ).Select(x => x.EventId);
            var availableForms = DBObjects.GetForm().Where(x =>
            availableEvents.Contains(x.EventId) && x.IsRegistration == 0).ToList();
            var events = DBObjects.GetEvents().ToList();
            string eventId = DBObjects.GetEvents().Where(x => x.Name == eventName).
                    First().Id;
            ViewBag.CurrentEventId = eventId;
            var forms = DBObjects.GetForm().Where(x => x.EventId == eventId).ToList();

            var allPupils = DBObjects.GetPupil().OrderBy(x => x.LastName).ToList();

            var model = (availableForms, events, forms, allPupils);

            return View("Index", model);
        }
    }
}
