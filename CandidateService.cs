using Microsoft.EntityFrameworkCore;

namespace Jobseekers
{
    /// <summary>
    /// Třída CandidateService s primárním konstruktorem obsahující metody pro manipulaci s kandidáty v databázi
    /// </summary>
    public class CandidateService(ApplicationDbContext dbContext)
    {
        /// <summary>
        /// Soukromá proměnná pro ukládání instance databázového kontextu. Díky modifikátoru readonly nebude hodnota této proměnné po konstrukci třídy změněna
        /// </summary>
        private readonly ApplicationDbContext _dbContext = dbContext;

        /// <summary>
        /// Asynchronní metoda přidává nového kandidáta do databáze.
        /// </summary>
        /// <param name="candidate">Instance kandidáta</param>
        public async Task AddCandidateAsync(Candidate candidate)
        {
            // Najde existující programovací jazyky v databázi
            var languageIds = candidate.ProgrammingLanguages.Select(pl => pl.Id).ToList();
            var existingLanguages = await _dbContext.ProgrammingLanguages
                                                   .Where(pl => languageIds.Contains(pl.Id))
                                                   .ToListAsync();

            // Přiřadí existující jazyky ke kandidátovi
            candidate.ProgrammingLanguages = existingLanguages;

            // Přidání kandidáta do tabulky Kandidátů v databázi
            _dbContext.Candidates.Add(candidate);
            // Uložení změn v databázi
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronní metoda získává seznam programovacích jazyků na základě jejich ID.
        /// </summary>
        /// <param name="ids">Seznam ID programovacích jazyků</param>
        /// <returns>Seznam programovacích jazyků</returns>
        public async Task<List<ProgrammingLanguage>> GetProgrammingLanguagesByIdsAsync(List<int> ids)
        {
                return await _dbContext.ProgrammingLanguages
                                      .Where(pl => ids.Contains(pl.Id))
                                      .ToListAsync();
        }

        /// <summary>
        /// Asynchronní metoda získává všechny kandidáty včetně jejich programovacích jazyků.
        /// </summary>
        /// <returns>Seznam všech kandidátů</returns>
        public async Task<List<Candidate>> GetAllCandidatesAsync()
        {
                return await _dbContext.Candidates.Include(c => c.ProgrammingLanguages).ToListAsync();
        }

        /// <summary>
        /// Asynchronní metoda zobrazí všechny dostupné programovací jazyky
        /// </summary>
        public async Task GetAllProgrammingLanguagesAsync()
        {
            Console.WriteLine("Dostupné programovací jazyky:");
                var languages = await _dbContext.ProgrammingLanguages.ToListAsync();
                foreach (var lang in languages)
                {
                    Console.WriteLine($"{lang.Id}: {lang.Language}");
                }
                Console.WriteLine();
        }

        /// <summary>
        /// Asynchronní metoda vyhledá kandidáty na základě znalosti programovacího jazyka.
        /// </summary>
        /// <param name="languageId">ID programovacího jazyka</param>
        /// <returns>Seznam nalezených kandidátů</returns>
        public async Task<List<Candidate>> SearchCandidatesByProgrammingLanguageIdAsync(int languageId)
        {
                return await _dbContext.Candidates
                    .Include(c => c.ProgrammingLanguages) // Zajistí, že se načtou i programovací jazyky
                    .Where(c => c.ProgrammingLanguages.Any(pl => pl.Id == languageId))
                    .ToListAsync();
        }

        /// <summary>
        /// Asynchronní metoda smaže kandidáta na základě ID.
        /// </summary>
        /// <param name="candidateId">ID kandidáta, který má být smazán</param>
        public async Task DeleteCandidateByIdAsync(int candidateId)
        {
                var candidate = await _dbContext.Candidates.FindAsync(candidateId);
                if (candidate != null)
                {
                    _dbContext.Candidates.Remove(candidate);
                    await _dbContext.SaveChangesAsync();
                    Console.WriteLine("Kandidát byl úspěšně vymazán.");
                }
                else
                {
                    Console.WriteLine("Kandidát s tímto ID nebyl nalezen.");
                }
        }

        /// <summary>
        /// Asynchronní metoda smaže všechny kandidáty z databáze a resetuje ID.
        /// </summary>
        public async Task DeleteAllCandidatesAsync()
        {
                var allCandidates = await _dbContext.Candidates.ToListAsync();
                _dbContext.Candidates.RemoveRange(allCandidates);
                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Candidates', RESEED, 0)");
                Console.WriteLine("Všichni kandidáti byli úspěšně vymazáni a zároveň bylo resetováno ID.");
        }
    }
}
