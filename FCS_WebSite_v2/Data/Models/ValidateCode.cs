using System.Collections.Generic;

namespace FCS_WebSite_v2.Data.Models
{
    public static class ValidateCode
    {
        private static SortedSet<string> _codes = new SortedSet<string>()
        {
            "ASDFGH", "QWERTY"
        };

        public static SortedSet<string> Codes
        {
            get => _codes;
        }
    }
}
