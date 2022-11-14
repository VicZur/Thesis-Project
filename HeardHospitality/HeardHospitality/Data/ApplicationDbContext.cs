using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HeardHospitality.Models;

namespace HeardHospitality.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Business> Business { get; set; }
        public DbSet<EmployeeExperience> EmployeeExperience { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmpSkill> EmpSkill { get; set; }
        public DbSet<JobApplication> JobApplication { get; set; }
        public DbSet<JobInfo> JobInfo { get; set; }
        public DbSet<JobPerk> JobPerk { get; set; }
        public DbSet<LoginDetail> LoginDetail { get; set; }
        public DbSet<Perk> Perk { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Reply> Reply { get; set; }
        public DbSet<Skill> Skill { get; set; }


    }

}