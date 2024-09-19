using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Jobseekers
{
    /// <summary>
    /// Třída ApplicationDbContext slouží pro správu databázových operací
    /// </summary>
    class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// DbSet pro uchovávání dat kandidátů v databázi
        /// </summary>
        public DbSet<Candidate> Candidates { get; set; }
        /// <summary>
        /// DbSet pro uchovávání dat programovacích jazyků
        /// </summary>
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

        /// <summary>
        /// Konfigurace připojení k databázi
        /// </summary>
        /// <param name="optionsBuilder">Objekt pro konfiguraci možností připojení</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Zajištění připojení k lokální databázi SQL Server (LocalDB)
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=JobseekersDB;Trusted_Connection=True;");
        }

        /// <summary>
        /// Konfigurace vztahů a inicializace předdefinovaných dat při vytváření modelu
        /// </summary>
        /// <param name="modelBuilder">Objekt pro definici databázového modelu</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfigurace vztahu N:N mezi kandidáty a programovacími jazyky s vlastní tabulkou
            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.ProgrammingLanguages)
                .WithMany(p => p.Candidates)
                .UsingEntity(j => j.ToTable("CandidateProgrammingLanguages"));

            // Seedování (inicializace) předdefinovaných dat programovacích jazyků do databáze
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
