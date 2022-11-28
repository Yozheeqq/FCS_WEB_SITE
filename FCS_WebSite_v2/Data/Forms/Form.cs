using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCS_WebSite_v2.Data.Forms
{
    public class Form
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? EventId { get; set; }
        public string? CreatorId { get; set; }

        // 1 - true
        // 0 - false

        public int IsRegistration { get; set; }

    }
}
