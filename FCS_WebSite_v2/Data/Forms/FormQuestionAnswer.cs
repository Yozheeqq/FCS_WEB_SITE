using System.ComponentModel.DataAnnotations.Schema;

namespace FCS_WebSite_v2.Data.Forms
{
    public class FormQuestionAnswer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? QuestionId { get; set; }
        public string? Answers { get; set; }
        public DateTime FormTime { get; set; }
    }
}
