using OnlineCv.WebUI.Models.Entity;
using System.Data.Entity;

namespace OnlineCv.WebUI.Models
{
    public class OnlineCvDbContext:DbContext 
    {
        public OnlineCvDbContext()
            :base("name=cString")
        {

        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<PersonDetails> PersonDetails { get; set; }
        public DbSet<ProfessionalExperience> ProfessionalExperiences { get; set; }
        public DbSet<AcademicBackground> AcademicBackground { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Bio> Bio { get; set; }
        public DbSet<EmailModel> EmailModel { get; set; }
        public DbSet<SocialProfiles> SocialProfiles { get; set; }
        public DbSet<ErrorHistory> ErrorHistory { get; set; }
    }
}