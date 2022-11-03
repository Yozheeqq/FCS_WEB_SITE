using System.Collections.Generic;

namespace FCS_WebSite_v2.Data.Models
{
    public enum FormType
    {
        ShortAnswer,
        LongAnswer,
        CheckBox
    }

    public class Form
    {
        private List<FormType> formTypes = new List<FormType>();

        public List<FormType> FormTypes { get => formTypes; }
    }
}
