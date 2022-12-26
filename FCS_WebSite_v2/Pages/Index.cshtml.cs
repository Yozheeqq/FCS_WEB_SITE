using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FCS_WebSite_v2.Data.DB;
using FCS_WebSite_v2.Data.Forms;
using System.Security.Claims;

namespace FCS_WebSite_v2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<Event> Events { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            Events = DBObjects.GetEvents().Where(x => x.IsAvailable == 1).ToList();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        
        public IActionResult OnPost(string eventId)
        {
            if(!HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var userEvents = DBObjects.GetUserEvents().Where(x => x.UserId == userId && 
                x.EventId == eventId);
            if(userEvents.Any())
            {
                // Если уже зарегался
                return Page();
            }
            var registerForm = DBObjects.GetForm().Where(x =>
                x.EventId == eventId && x.IsRegistration == 1
            ).FirstOrDefault();

            // Если нет формы регистрации на меро
            if(registerForm == null)
            {
                return NotFound();
            }

            return Redirect($"form/fill/{registerForm.Id}");
        }
    }
}