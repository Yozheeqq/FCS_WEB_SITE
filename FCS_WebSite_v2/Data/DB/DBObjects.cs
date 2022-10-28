using FCS_WebSite.Models;
using Microsoft.EntityFrameworkCore;

namespace FCS_WebSite_v2.Data.DB
{
    public class DBObjects
    {
        public static ApplicationDbContext Content
        {
            get;
            set;
        }

        public static void InitialPupil(Pupil pupil)
        {
            Content.Pupil.Add(pupil);
            Content.SaveChanges();
        }

        public static void InitialTeacher(Teacher teacher)
        {
            Content.Teacher.Add(teacher);
            Content.SaveChanges();
        }

        public static DbSet<Pupil> GetPupil()
        {
            return Content.Pupil;
        }

        public static DbSet<Teacher> GetTeacher()
        {
            return Content.Teacher;
        }
    }
}
