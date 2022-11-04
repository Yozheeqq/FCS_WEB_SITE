using Microsoft.AspNetCore.Identity;

namespace FCS_WebSite_v2.Data.Forms
{
    public class Form
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? EventId { get; set; }
        public string? CreatorId { get; set; }

    }
}
