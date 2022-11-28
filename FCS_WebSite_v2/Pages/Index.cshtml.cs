using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FCS_WebSite_v2.Data.DB;
using FCS_WebSite_v2.Data.Forms;

namespace FCS_WebSite_v2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<Event> Events { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            Events = DBObjects.GetEvents().Where(x => x.IsAvailable == 1).ToList();

            return Page();
        }

        
        public IActionResult OnPost(string eventId)
        {
            var registerForm = DBObjects.GetForm().Where(x =>
                x.EventId == eventId && x.IsRegistration == 1
            ).First();

            return Redirect($"form/fill/{registerForm.Id}");
        }
    }
}