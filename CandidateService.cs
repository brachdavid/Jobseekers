using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Jobseekers
{
    /// <summary>
    /// Třída CandidateService obsahující metody pro manipulaci s kandidáty v databázi
    /// </summary>
    class CandidateService
    {
        /// <summary>
        /// Metoda přidává nového kandidáta do databáze
        /// </summary>
        /// <param name="candidate">Instance kandidáta</param>
        public void AddCandidate(Candidate candidate)
        {
            // Použití using bloku k otevření a automatickému uzavření kontextu databáze
            using (var dbContext = new ApplicationDbContext())
            {
                // Najde existující programovací jazyky v databázi
                var languageIds = candidate.ProgrammingLanguages.Select(pl => pl.Id).ToList();
                var existingLanguages = dbContext.ProgrammingLanguages
                    .Where(pl => languageIds.Contains(pl.Id))
                    .ToList();

                // Přiřadí existující jazyky ke kandidátovi
                candidate.ProgrammingLanguages = existingLanguages;

                // Přidání kandidáta do tabulky Kandidátů v databázi
                dbContext.Candidates.Add(candidate);
                // Uložení změn v databázi
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Metoda získává seznam programovacích jazyků na základě jejich ID
        /// </summary>
        /// <param name="ids">Seznam ID programovacích jazyků</param>
        /// <returns>Seznam programovacích jazyků</returns>
        public List<ProgrammingLanguage> GetProgrammingLanguagesByIds(List<int> ids)
        {
            // Použití using bloku k otevření kontextu databáze
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.ProgrammingLanguages
                                .Where(pl => ids.Contains(pl.Id))
                                .ToList();
            }
        }

        /// <summary>
        /// Metoda získává všechny kandidáty včetně jejich programovacích jazyků
        /// </summary>
        /// <returns>Seznam všech kandidátů</returns>
        public List<Candidate> GetAllCandidates()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.Candidates.Include(c => c.ProgrammingLanguages).ToList();
            }
        }

        /// <summary>
        /// Metoda zobrazí všechny dostupné programovací jazyky
        /// </summary>
        public void GetAllProgrammingLanguages()
        {
            Console.WriteLine("Dostupné programovací jazyky:");
            using (var dbContext = new ApplicationDbContext())
            {
                var languages = dbContext.ProgrammingLanguages.ToList();
                foreach (var lang in languages)
                {
                    Console.WriteLine($"{lang.Id}: {lang.Language}");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Metoda vyhledá kandidáty na základě znalosti programovacího jazyka
        /// </summary>
        /// <param name="languageId">ID programovacího jazyka</param>
        /// <returns>Seznam nalezených kandidátů</returns>
        public List<Candidate> SearchCandidatesByProgrammingLanguageId(int languageId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.Candidates
                         .Include(c => c.ProgrammingLanguages) // Zajistí, že se načtou i programovací jazyky
                         .Where(c => c.ProgrammingLanguages.Any(pl => pl.Id == languageId))
                         .ToList();
            }
        }

        /// <summary>
        /// Metoda smaže kandidáta na základě ID
        /// </summary>
        /// <param name="candidateId">ID kandidáta, který má být smazán</param>
        public void DeleteCandidateById(int candidateId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var candidate = dbContext.Candidates.Find(candidateId);
                if (candidate != null)
                {
                    dbContext.Candidates.Remove(candidate);
                    dbContext.SaveChanges();
                    Console.WriteLine($"Kandidát byl úspěšně vymazán.");
                }
                else
                {
                    Console.WriteLine($"Kandidát s tímto ID nebyl nalezen.");
                }
            }
        }

        /// <summary>
        /// Smaže všechny kandidáty z databáze a resetuje ID
        /// </summary>
        public void DeleteAllCandidates()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var allCandidates = dbContext.Candidates.ToList();
                dbContext.Candidates.RemoveRange(allCandidates);
                dbContext.SaveChanges();
                dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Candidates', RESEED, 0)");
                Console.WriteLine("Všichni kandidáti byli úspěšně vymazáni a zároveň bylo resetováno ID.");
            }
        }
    }
}
