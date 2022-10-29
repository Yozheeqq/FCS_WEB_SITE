using FCS_WebSite_v2.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace FCS_WebSite.Models
{
    public class Teacher : IPersonable
    {
        public string? Code { get; set; }
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

    }
}
