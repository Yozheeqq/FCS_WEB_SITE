using Microsoft.AspNetCore.Identity;

namespace FCS_WebSite.Models
{
    public interface IPersonable
    {
        string? FirstName { get; set; }
        string? LastName { get; set; }
        string? Email { get; set; }
        string? Password { get; set; }
    }
}
