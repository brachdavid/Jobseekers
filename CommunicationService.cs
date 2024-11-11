namespace Jobseekers
{
    /// <summary>
    /// Třída CommunicationService obsahující metody pro komunikaci s uživatelem a
    /// </summary>
    public class CommunicationService
    {
        /// <summary>
        /// Inicializace instance CandidateService
        /// </summary>
        public CandidateService CandidateService { get; private set; }
        /// <summary>
        /// Inicializace instance InputValidation
        /// </summary>
        public InputValidation InputValidation { get; private set; }

        /// <summary>
        /// Konstruktor přijímá instance CandidateService a InputValidation přes Dependency Injection
        /// </summary>
        /// <param name="candidateService">Instance CandidateService, která je injektována</param>
        /// <param name="inputValidation">Instance InputValidation, která je injektována</param>
        public CommunicationService()
        {
            // Pokud nejsou parametry předány, vytvoří se nové instance
            CandidateService = new CandidateService(new ApplicationDbContext());
            InputValidation = new InputValidation();
        }

        /// <summary>
        /// Asynchronní metoda rozbíhá program sloužící ke správě kandidátů
        /// </summary>
        public async Task RunProgramAsync()
        {
            char choice = '0';
            while (choice != '6')
            {
                PrintMenu();
                choice = Console.ReadKey().KeyChar;
                Console.WriteLine();
                await ProcessChoiceAsync(choice);
            }
        }

        /// <summary>
        /// Asynchronní metoda zpracovává uživatelskou volbu
        /// </summary>
        /// <param name="choice"></param>
        public async Task ProcessChoiceAsync(char choice)
        {
            switch (choice)
            {
                case '1':
                    await AddCandidateAsync();
                    break;
                case '2':
                    await DisplayAllCandidatesAsync();
                    break;
                case '3':
                    await SearchCandidatesByProgrammingLanguageIdAsync();
                    break;
                case '4':
                    await DeleteCandidateByIdAsync();
                    break;
                case '5':
                    await DeleteAllCandidatesAsync();
                    break;
                case '6':
                    Console.WriteLine("Děkujeme za použití aplikace a na viděnou zase příště!\nLibovolnou klávesou ukončete program...");
                    break;
                default:
                    Console.WriteLine("Neplatná volba, stiskněte libovolnou klávesu a opakujte volbu.");
                    break;
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Metoda vykreslí hlavní menu celé aplikace
        /// </summary>
        public static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("-------------------- Výběrové řízení - Junior Programátor --------------------\n");
            Console.WriteLine("Vyberte akci:");
            Console.WriteLine("1. Přidat nového kandidáta");
            Console.WriteLine("2. Vypsat všechny kandidáty");
            Console.WriteLine("3. Vyhledat kandidáty podle programovacího jazyka");
            Console.WriteLine("4. Vymazat kandidáta z databáze");
            Console.WriteLine("5. Vymazat všechny kandidáty z databáze");
            Console.WriteLine("6. Ukončit aplikaci");
        }

        /// <summary>
        /// Asynchronní etoda přidává nového kandidáta založeného na uživatelských vstupech
        /// </summary>
        public async Task AddCandidateAsync()
        {
            Console.Clear();
            Console.WriteLine("-------------------- Přidání nového kandidáta --------------------\n");
            string firstName = EnterFirstName();
            string lastName = EnterLastName();
            DateTime birthDate = EnterBirthDate();
            string city = EnterCityName();
            string phoneNumber = EnterPhoneNumber();
            string email = EnterEmail();
            var selectedIds = await GetProgrammingLanguagesFromUserAsync();
            List<ProgrammingLanguage> selectedLanguages = await CandidateService.GetProgrammingLanguagesByIdsAsync(selectedIds);

            Candidate candidate = new()
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                City = city,
                PhoneNumber = phoneNumber,
                Email = email,
                ProgrammingLanguages = selectedLanguages
            };

            await CandidateService.AddCandidateAsync(candidate);
            Console.WriteLine("Uchazeč přidán." + "\n------------------------------------------" + "\nPro návrat do hlavního menu stiskněte libovolnou klávesu.");
        }

        /// <summary>
        /// Asynchronní metoda získává od uživatele programovací jazyky, které uchazeč o zaměstnání ovládá
        /// </summary>
        /// <returns>Seznam programovacích jazyků</returns>
        private async Task<List<int>> GetProgrammingLanguagesFromUserAsync()
        {
                await CandidateService.GetAllProgrammingLanguagesAsync();

                Console.Write("Zadej ID programovacího jazyka (pokud chceš zadat více programovacích jazyků, odděl je čárkou): ");
                var selectedIds = InputValidation.GetValidatedProgrammingLanguageIds();

                return selectedIds;
        }

        /// <summary>
        /// Asynchronní metoda vypíše kompletní seznam kandidátů uložených v databázi
        /// </summary>
        public async Task DisplayAllCandidatesAsync()
        {
            Console.Clear();
            Console.WriteLine("-------------------- Všichni kandidáti v databázi --------------------\n");
            var candidates = await CandidateService.GetAllCandidatesAsync();
            foreach (var candidate in candidates)
            {
                Console.WriteLine($"Jméno: {candidate.FirstName} {candidate.LastName}\n" +
                                  $"Věk: {candidate.BirthDate}\n" +
                                  $"Město: {candidate.City}\n" +
                                  $"Telefon: {candidate.PhoneNumber}\n" +
                                  $"Email: {candidate.Email}\n" +
                                  $"Programovací jazyky: {string.Join(", ", candidate.ProgrammingLanguages.Select(pl => pl.Language))}\n" +
                                  "------------------------------------------");
            }
            Console.WriteLine("Pro návrat do hlavního menu stiskněte libovolnou klávesu.");
        }

        /// <summary>
        /// Asynchronní metoda vypíše seznam kandidátů podle znalosti vybraného programovacího jazyka
        /// </summary>
        public async Task SearchCandidatesByProgrammingLanguageIdAsync()
        {
            Console.Clear();
            Console.WriteLine("-------------------- Hledání kandidáta podle konkrétního programovacího jazyka --------------------\n");
            await CandidateService.GetAllProgrammingLanguagesAsync();
            int languageId = EnterProgrammingLanguageId();

            List<Candidate> foundCandidates = await CandidateService.SearchCandidatesByProgrammingLanguageIdAsync(languageId);

            if (foundCandidates.Count != 0)
            {
                Console.WriteLine("------------------------------------------\nNalezení kandidáti:\n------------------------------------------");
                foreach (var candidate in foundCandidates)
                {
                    Console.WriteLine($"Jméno: {candidate.FirstName} {candidate.LastName}\n" +
                                      $"Věk: {candidate.BirthDate}\n" +
                                      $"Město: {candidate.City}\n" +
                                      $"Telefon: {candidate.PhoneNumber}\n" +
                                      $"Email: {candidate.Email}\n" +
                                      $"Programovací jazyky: {string.Join(", ", candidate.ProgrammingLanguages.Select(pl => pl.Language))}\n" +
                                      "------------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("Nebyl nalezen žádný kandidát s tímto programovacím jazykem.");
            }
            Console.WriteLine("Pro návrat do hlavního menu stiskněte libovolnou klávesu.");
        }

        /// <summary>
        /// Asynchronní metoda vymaže kandidáta podle zvoleného ID
        /// </summary>
        public async Task DeleteCandidateByIdAsync()
        {
            Console.Clear();
            Console.WriteLine("-------------------- Vymazání kandidáta podle zvoleného ID --------------------\n");
            Console.WriteLine("Dostupní kandidáti" + "\n------------------------------------------");
            await DisplayAllCandidatesWithIdsAsync();
            Console.WriteLine("------------------------------------------" + "\nZadejte ID kandidáta, kterého chcete smazat:");
            int candidateId = InputValidation.GetValidatedId();
            await CandidateService.DeleteCandidateByIdAsync(candidateId);
            Console.WriteLine("------------------------------------------" + "\nPro návrat do hlavního menu stiskněte libovolnou klávesu.");
        }

        /// <summary>
        /// Asynchronní metoda vymaže všechny kandidáty v databázi a resetuje ID
        /// </summary>
        public async Task DeleteAllCandidatesAsync()
        {
            Console.Clear();
            Console.WriteLine("-------------------- Vymazání všech kandidátů uložených v databázi --------------------\n");
            Console.WriteLine("Opravdu chcete vymazat všechny uložené kandidáty? (ano/ne)");
            string confirmation = Console.ReadLine()!.ToLower();
            if (confirmation == "ano")
            {
                await CandidateService.DeleteAllCandidatesAsync();
            }
            else
            {
                Console.WriteLine("Akce byla zrušena.");
            }
            Console.WriteLine("------------------------------------------" + "\nPro návrat do hlavního menu stiskněte libovolnou klávesu.");
        }

        /// <summary>
        /// Metoda získává křestní jméno kandidáta
        /// </summary>
        /// <returns>Křestní jméno</returns>
        public static string EnterFirstName()
        {
            Console.Write("Zadejte křestní jméno kandidáta: ");
            return InputValidation.GetValidatedName();
        }

        /// <summary>
        /// Metoda získává příjmení kandidáta
        /// </summary>
        /// <returns>Příjmení</returns>
        public static string EnterLastName()
        {
            Console.Write("Zadejte příjmení kandidáta: ");
            return InputValidation.GetValidatedName();
        }

        /// <summary>
        /// Metoda získává datum narození kandidáta
        /// </summary>
        /// <returns>Datum narození</returns>
        public static DateTime EnterBirthDate()
        {
            Console.Write("Zadejte datum narození kandidáta (ve formátu yyyy-mm-dd): ");
            return InputValidation.GetValidatedDate();
        }


        /// <summary>
        /// Metoda zjistí, v jakém městě kandidát bydlí
        /// </summary>
        /// <returns>Město, ve kterém kandidát bydlí</returns>
        public static string EnterCityName()
        {
            Console.Write("Zadejte název města nebo vesnice, kde momentálně žijete: ");
            return InputValidation.GetValidatedName();
        }

        /// <summary>
        /// Metoda získává telefonní číslo kandidáta
        /// </summary>
        /// <returns>Telefonní číslo</returns>
        public static string EnterPhoneNumber()
        {
            Console.Write("Zadejte telefonní číslo ve formátu +420 xxx xxx xxx: ");
            return InputValidation.GetValidatedPhoneNumber();
        }

        /// <summary>
        /// Metoda získává e-mail kandidáta
        /// </summary>
        /// <returns>E-mail</returns>
        public static string EnterEmail()
        {
            Console.Write("Zadejte e-mailovou adresu ve formátu example@example.com: ");
            return InputValidation.GetValidatedEmail();
        }

        /// <summary>
        /// Metoda získává programovací jazyk
        /// </summary>
        /// <returns>Programovací jazyk</returns>
        public static int EnterProgrammingLanguageId()
        {
            Console.Write("Zadejte id programovacího jazyka: ");
            return InputValidation.GetValidatedId();
        }

        /// <summary>
        /// Asynchronní metoda vypíše kandidáty z databáze, přičemž zobrazí pouze ID, křestní jméno a příjmení
        /// </summary>
        public async Task DisplayAllCandidatesWithIdsAsync()
        {
            var candidates = await CandidateService.GetAllCandidatesAsync();
            foreach (var candidate in candidates)
            {
                Console.WriteLine($"ID: {candidate.Id} Jméno a příjmení: {candidate.FirstName} {candidate.LastName}");
            }
        }
    }
}
