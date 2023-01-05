using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Rehberlik.Models.SqlDat
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Rehberlik;Integrated Security=False;");
            optionsBuilder.UseSqlServer("data source=DESKTOP-VHM3JVU; initial catalog=Rehberlik;User ID=sa; Password=Guga97; TrustServerCertificate=True");

        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInformation> ContactInformation { get; set; }
        public DbSet<CustomerComment> CustomerComments { get; set; }
        public DbSet<DoneAllProject> DoneAllProjects { get; set; }
        public DbSet<OurProject> OurProjects { get; set; }
        public DbSet<OurService> OurServices { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<ThreeStepProgress> ThreeSteps { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
