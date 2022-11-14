using HeardHospitality.Models;
using System;
using System.Linq;
using HeardHospitality.Data;

namespace HeardHospitality.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Skill.Any())
            {
                return;   // DB has been seeded
            }

            var skills = new Skill[]
            {
                new Skill() {SkillDescription="Test"}
            };
            foreach (Skill s in skills)
            {
                context.Skill.Add(s);
            }
            context.SaveChanges();
        }
    }
}
