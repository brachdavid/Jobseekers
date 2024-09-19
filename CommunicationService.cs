using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobseekers
{
    class CommunicationService
    {
        public CandidateService CandidateService { get; private set; }
        public InputValidation InputValidation { get; private set; }


        public CommunicationService()
        {
            CandidateService = new CandidateService();
            InputValidation = new InputValidation();
        }

        public void RunProgram()
        {
            char choice = '0';
            while (choice != '6')
            {
                PrintMenu();
                choice = Console.ReadKey().KeyChar;
                Console.WriteLine();
                ProcessChoice(choice);
            }
        }
        public void ProcessChoice(char choice)
        {
            switch (choice)
            {
                case '1':
                    AddCandidate();
                    break;
                case '2':
                    DisplayAllCandidates();
                    break;
                case '3':
                    SearchCandidatesByProgrammingLanguageId();
                    break;
                case '4':
                    DeleteCandidateById();
                    break;
                case '5':
                    DeleteAllCandidates();
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
        public void PrintMenu()
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

        public void AddCandidate()
        {
            Console.Clear();
            Console.WriteLine("-------------------- Přidání nového kandidáta --------------------\n");
            string firstName = EnterFirstName();
            string lastName = EnterLastName();
            DateTime birthDate = EnterBirthDate();
            string city = EnterCityName();
            string phoneNumber = EnterPhoneNumber();
            string email = EnterEmail();
            List<int> selectedIds = GetProgrammingLanguagesFromUser();
            List<ProgrammingLanguage> selectedLanguages = CandidateService.GetProgrammingLanguagesByIds(selectedIds);

            Candidate candidate = new Candidate
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                City = city,
                PhoneNumber = phoneNumber,
                Email = email,
                ProgrammingLanguages = selectedLanguages
            };
            CandidateService.AddCandidate(candidate);
            Console.WriteLine("Uchazeč přidán." + "\n------------------------------------------" + "\nPro návrat do hlavního menu stiskněte libovolnou klávesu.");
        }
        private List<int> GetProgrammingLanguagesFromUser()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                CandidateService.GetAllProgrammingLanguages();

                Console.WriteLine("Zadej ID programovacího jazyka (pokud chceš zadat více programovacích jazyků, odděl je, prosím, čárkou):");
                var selectedIds = Console.ReadLine()!.Split(',').Select(int.Parse).ToList();
                return selectedIds;
            }
        }

        public void DisplayAllCandidates()
        {
            Console.Clear();
            Console.WriteLine("-------------------- Všichni kandidáti v databázi --------------------\n");
            var candidates = CandidateService.GetAllCandidates();
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

        public void SearchCandidatesByProgrammingLanguageId()
        {
            Console.Clear();
            Console.WriteLine("-------------------- Hledání kandidáta podle konkrétního programovacího jazyka --------------------\n");
            CandidateService.GetAllProgrammingLanguages();
            int languageId = EnterProgrammingLanguageId(); // Záskání ID programovacího jazyka

            List<Candidate> foundCandidates = CandidateService.SearchCandidatesByProgrammingLanguageId(languageId); // Vyhledání kandidátů podle ID jazyka

            if (foundCandidates.Any())
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
                Console.WriteLine("Nebyl nalezen žádný kandidát s tímto programovacím jazykem. Přijde vám to divné? Zkontrolujte, jestli vámi zadané ID skutečně existuje.");
            }
            Console.WriteLine("Pro návrat do hlavního menu stiskněte libovolnou klávesu.");
        }
        public void DeleteCandidateById()
        {
            Console.Clear();
            Console.WriteLine("-------------------- Vymazání kandidáta podle zvoleného ID --------------------\n");
            Console.WriteLine("Dostupní kandidáti" + "\n------------------------------------------");
            DisplayAllCandidatesWithIds();
            int candidateId = 0;
            Console.WriteLine("------------------------------------------" + "\nZadejte ID kandidáta, kterého chcete smazat:");
            candidateId = InputValidation.GetValidatedId(candidateId);
            CandidateService.DeleteCandidateById(candidateId);
            Console.WriteLine("------------------------------------------" + "\nPro návrat do hlavního menu stiskněte libovolnou klávesu.");
        }

        public void DeleteAllCandidates()
        {
            Console.Clear();
            Console.WriteLine("-------------------- Vymazání všech kandidátů uložených v databázi --------------------\n");
            Console.WriteLine("Opravdu chcete vymazat všechny uložené kandidáty? (ano/ne)");
            string confirmation = Console.ReadLine()!.ToLower();
            if (confirmation == "ano")
            {
                CandidateService.DeleteAllCandidates();
            }
            else
            {
                Console.WriteLine("Akce byla zrušena.");
            }
            Console.WriteLine("------------------------------------------" + "\nPro návrat do hlavního menu stiskněte libovolnou klávesu.");
        }

        public string EnterFirstName()
        {
            Console.Write("Zadejte křestní jméno kandidáta: ");
            string firstName = "";
            return InputValidation.GetValidatedName(firstName);
        }
        public string EnterLastName()
        {
            Console.Write("Zadejte příjmení kandidáta: ");
            string lastName = "";
            return InputValidation.GetValidatedName(lastName);
        }
        public DateTime EnterBirthDate()
        {
            Console.Write("Zadejte datum narození kandidáta (ve formátu yyyy-mm-dd): ");
            string birthDate = "";
            return InputValidation.GetValidatedDate(birthDate);
        }
        public string EnterCityName()
        {
            Console.Write("Zadejte název města nebo vesnice, kde momentálně žijete: ");
            string city = "";
            return InputValidation.GetValidatedName(city);
        }
        public string EnterPhoneNumber()
        {
            Console.Write("Zadejte telefonní číslo ve formátu +420 xxx xxx xxx: ");
            string phoneNumber = "";
            return InputValidation.GetValidatedPhoneNumber(phoneNumber);
        }
        public string EnterEmail()
        {
            Console.Write("Zadejte e-mailovou adresu ve formátu example@example.com: ");
            string email = "";
            return InputValidation.GetValidatedEmail(email);
        }

        public int EnterProgrammingLanguageId()
        {
            Console.Write("Zadejte id programovacího jazyka: ");
            int userInput = 0;
            return InputValidation.GetValidatedId(userInput);
        }
        public void DisplayAllCandidatesWithIds()
        {
            var candidates = CandidateService.GetAllCandidates();
            foreach (var candidate in candidates)
            {
                Console.WriteLine($"ID: {candidate.Id} Jméno a příjmení: {candidate.FirstName} {candidate.LastName}");
            }
        }
    }
}
