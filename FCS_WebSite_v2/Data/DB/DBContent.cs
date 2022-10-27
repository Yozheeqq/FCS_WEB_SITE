using FCS_WebSite.Models;
using Microsoft.EntityFrameworkCore;

namespace FCS_WebSite_v2.Data.DB
{
    public class DBContent : DbContext
    {
        public DBContent(DbContextOptions<DBContent> options) : base(options) { }

        public DbSet<Pupil> Pupil { set; get; }
        public DbSet<Teacher> Teacher { set; get; }

    }
}
