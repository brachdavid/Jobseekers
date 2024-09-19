using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Jobseekers
{
    class ApplicationDbContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=JobseekersDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.ProgrammingLanguages)
                .WithMany(p => p.Candidates)
                .UsingEntity(j => j.ToTable("CandidateProgrammingLanguages"));

            modelBuilder.Entity<ProgrammingLanguage>().HasData(
                new ProgrammingLanguage { Id = 1, Language = "C#" },
                new ProgrammingLanguage { Id = 2, Language = "Java" },
                new ProgrammingLanguage { Id = 3, Language = "JavaScript" },
                new ProgrammingLanguage { Id = 4, Language = "PHP" },
                new ProgrammingLanguage { Id = 5, Language = "C" },
                new ProgrammingLanguage { Id = 6, Language = "C++" },
                new ProgrammingLanguage { Id = 7, Language = "Kotlin" }
            );
        }
    }

}
