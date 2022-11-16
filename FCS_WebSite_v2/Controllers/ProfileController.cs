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
        public IActionResult Index()
        {
            return View();
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
    }
}
