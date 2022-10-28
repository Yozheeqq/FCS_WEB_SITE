using Microsoft.AspNetCore.Identity;

namespace FCS_WebSite_v2.Data.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
