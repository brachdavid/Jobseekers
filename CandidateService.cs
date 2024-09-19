using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Jobseekers
{
    class CandidateService
    {
        public void AddCandidate(Candidate candidate)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                // Najde existující programovací jazyky v databázi
                var languageIds = candidate.ProgrammingLanguages.Select(pl => pl.Id).ToList();
                var existingLanguages = dbContext.ProgrammingLanguages
                    .Where(pl => languageIds.Contains(pl.Id))
                    .ToList();

                // Přiřadí existující jazyky ke kandidátovi
                candidate.ProgrammingLanguages = existingLanguages;

                // Přidá kandidáta do databáze
                dbContext.Candidates.Add(candidate);
                dbContext.SaveChanges();
            }
        }
        public List<ProgrammingLanguage> GetProgrammingLanguagesByIds(List<int> ids)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.ProgrammingLanguages
                                .Where(pl => ids.Contains(pl.Id))
                                .ToList();
            }
        }

        public List<Candidate> GetAllCandidates()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.Candidates.Include(c => c.ProgrammingLanguages).ToList();
            }
        }

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
