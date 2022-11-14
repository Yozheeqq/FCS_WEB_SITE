using System.ComponentModel.DataAnnotations.Schema;

namespace FCS_WebSite_v2.Data.Forms
{
    public class FormQuestion
    {
        public string? Id { get; set; }
        public string? Content { get; set; }
        public string? FormId { get; set; }
        public string? TypeId { get; set; }
        public string? Answers { get; set; }
    }
}
