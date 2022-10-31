using FCS_WebSite_v2.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FCS_WebSite.Models
{
    public class Pupil : IPersonable
    {
        public string? Id { set; get; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
