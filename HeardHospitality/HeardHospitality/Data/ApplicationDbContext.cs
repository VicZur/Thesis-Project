using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HeardHospitality.Models;

namespace HeardHospitality.Data
{
    public class ApplicationDbContext : IdentityDbContext<LoginDetail>
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<Business>().ToTable("Business");
            modelBuilder.Entity<EmployeeExperience>().ToTable("EmployeeExperience");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<EmpSkill>().ToTable("EmpSkill");
            modelBuilder.Entity<JobApplication>().ToTable("JobApplication");
            modelBuilder.Entity<JobInfo>().ToTable("JobInfo");
            modelBuilder.Entity<JobPerk>().ToTable("JobPerk");
            modelBuilder.Entity<LoginDetail>().ToTable("LoginDetail");
            modelBuilder.Entity<Perk>().ToTable("Perk");
            modelBuilder.Entity<Rating>().ToTable("Rating");
            modelBuilder.Entity<Reply>().ToTable("Reply");
            modelBuilder.Entity<Skill>().ToTable("Skill");
        }

    }

}