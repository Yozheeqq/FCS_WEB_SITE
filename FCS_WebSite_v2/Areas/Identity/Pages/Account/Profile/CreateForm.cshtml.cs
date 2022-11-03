using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FCS_WebSite_v2.Data.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FCS_WebSite_v2.Areas.Identity.Pages.Account.Profile
{
    public class CreateFormModel : PageModel
    {
        private List<FormType> formTypes = new List<FormType>();

        public void OnPost()
        {
        }

        public void AddShortAnswer()
        {
            formTypes.Add(FormType.ShortAnswer);
        }
    }
}
