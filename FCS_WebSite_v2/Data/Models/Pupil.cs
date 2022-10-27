namespace FCS_WebSite.Models
{
    public class Pupil : IPersonable
    {
        public static int s_id = 0;
        private int id = 0;

        public string? Email
        {
            set; get;
        }

        public string? Password
        {
            set; get;
        }
        public string? FirstName
        {
            set; get;
        }
        public string? LastName
        {
            set; get;
        }
        public int Id
        {
            set
            {
                id = s_id++;
            }
            get => id;
        }
    }
}
