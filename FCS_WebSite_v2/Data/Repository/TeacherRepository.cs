using FCS_WebSite.Models;
using FCS_WebSite_v2.Data.DB;
using Microsoft.EntityFrameworkCore;

namespace FCS_WebSite_v2.Data.Repository
{
    public class TeacherRepository
    {
        private readonly ApplicationDbContext dBContent;

        public TeacherRepository(ApplicationDbContext dBContent)
        {
            this.dBContent = dBContent;
        }

        public IEnumerable<Teacher> Pupils => dBContent.Teacher.Include(teacher => teacher.Id);

        public Teacher? GetObjPupil(int id) =>
            dBContent.Teacher.FirstOrDefault(teacher => teacher.Id == id);

    }
}
