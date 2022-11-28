using FCS_WebSite.Models;
using FCS_WebSite_v2.Data.Forms;
using FCS_WebSite_v2.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using FCS_WebSite_v2.Data.Models;

namespace FCS_WebSite_v2.Data.DB
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<UserEvent>().HasKey(table => new
            {
                table.UserId, table.EventId
            });
        }

        public DbSet<Pupil> Pupil { set; get; }
        public DbSet<Teacher> Teacher { set; get; }
        public DbSet<User> User { set; get; }
        public DbSet<Event> Event { set; get; }
        public DbSet<Form> Form { set; get; }
        public DbSet<FormQuestion> FormQuestion { set; get; }
        public DbSet<QuestionType> QuestionType { set; get; }
        public DbSet<FormQuestionAnswer> FormQuestionAnswer { set; get; }
        public DbSet<UserEvent> UserEvent { set; get; }
    }
}