using FCS_WebSite.Models;
using FCS_WebSite_v2.Data.DB;
using Microsoft.EntityFrameworkCore;

namespace FCS_WebSite_v2.Data.Repository
{
    public class PupilRepository
    {
        private readonly DBContent dBContent;

        public PupilRepository(DBContent dBContent)
        {
            this.dBContent = dBContent;
        }

        public IEnumerable<Pupil> Pupils => dBContent.Pupil.Include(pupil => pupil.Id);

        public Pupil? GetObjPupil(int id) => 
            dBContent.Pupil.FirstOrDefault(pupil => pupil.Id == id);

    }
}
