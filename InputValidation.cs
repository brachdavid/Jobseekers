using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobseekers
{
    /// <summary>
    /// Třída InputValidation obsahuje metody pro validaci uživatelských vstupů
    /// </summary>
    class InputValidation
    {
        /// <summary>
        /// Metoda vrací zvalidovaný název (křestní jméno, příjmení, město atd.)
        /// </summary>
        /// <param name="userInput">Uživatelský vstup</param>
        /// <returns>Zvalidovaný názevp</returns>
        public string GetValidatedName(string userInput)
        {
            while (string.IsNullOrWhiteSpace(userInput = Console.ReadLine()?.Trim() ?? "")
           || !IsFirstLetterUppercase(userInput)
           || ContainsDigitsOrSpecialCharacters(userInput))
            {
                Console.WriteLine("Něco se pokazilo! Nesmíte používat číslice ani speciální znaky, zároveň musíte začínat velkým písmenem.");
                Console.Write("Zkuste to znovu: ");
            }
            return userInput;
        }

        /// <summary>
        /// Metoda vrací zvalidovaný datum v požadovaném formátu a zároveň hlídá, aby byl kandidát plnoletý
        /// </summary>
        /// <param name="userInput">Uživatelský vstup</param>
        /// <returns>Zvalidovaný datum narození</returns>
        public DateTime GetValidatedDate(string userInput)
        {
            DateTime validDate;

            while (string.IsNullOrWhiteSpace(userInput = Console.ReadLine()?.Trim() ?? "")
                   || !DateTime.TryParseExact(userInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out validDate)
                   || !IsCandidateAdult(validDate))
            {
                Console.WriteLine("Ajajaj, někde se stala chyba! Datum musí být ve formátu dd.MM.yyyy a kandidát musí být starší 18 let.");
                Console.Write("Zkuste to znovu: ");
            }
            return validDate;
        }

        /// <summary>
        /// Metoda vrací zvalidované telefonní číslo v požadovaném formátu
        /// </summary>
        /// <param name="userInput">Uživatelský vstup</param>
        /// <returns>Zvalidované telefonní číslop</returns>
        public string GetValidatedPhoneNumber(string userInput)
        {
            while (string.IsNullOrWhiteSpace(userInput = Console.ReadLine()?.Trim() ?? "") || !IsValidPhoneNumber(userInput))
            {
                Console.WriteLine("Neplatné telefonní číslo! Číslo musí být ve formátu +420 xxx xxx xxx a obsahovat pouze číslice.");
                Console.Write("Zkuste to znovu: ");
            }
            return userInput;
        }

        /// <summary>
        /// Metoda vrací zvalidovaný e-mail
        /// </summary>
        /// <param name="userInput">Uživatelský vstup</param>
        /// <returns>Zvalidovaný e-mail</returns>
        public string GetValidatedEmail(string userInput)
        {
            while (string.IsNullOrWhiteSpace(userInput = Console.ReadLine()?.Trim() ?? "") || !IsValidEmail(userInput))
            {
                Console.WriteLine("Neplatný e-mail! Zadejte e-mailovou adresu ve formátu example@example.com.");
                Console.Write("Zkuste to znovu: ");
            }
            return userInput;
        }

        /// <summary>
        /// Metoda vrací zvalidované ID
        /// </summary>
        /// <param name="userInput">Uživatelský vstup</param>
        /// <returns>Zvalidované ID</returns>
        public int GetValidatedId(int userInput)
        {
            while (!int.TryParse(Console.ReadLine(), out userInput))
            {
                Console.WriteLine("Neplatné ID. Zadejte prosím znovu:");
            }
            return userInput;
        }

        // Metoda ověřuje ID programovacích jazyků zadaných uživatelem
        public List<int> GetValidatedProgrammingLanguageIds()
        {
            List<int> ids = new List<int>();
            bool isValid = false;

            while (!isValid)
            {
                try
                {
                    var input = Console.ReadLine()!.Trim();
                    ids = input.Split(',')
                               .Select(id => int.Parse(id.Trim()))
                               .ToList();

                    isValid = true; // Vstup je platný, ukončíme cyklus
                }
                catch (FormatException)
                {
                    Console.WriteLine("Chyba: Zadejte prosím pouze čísla oddělená čárkou. Zkuste to znovu.");
                }
            }

            return ids;
        }

        /// <summary>
        /// Metoda vrací odpověď na otázku, jestli uživatelský vstup začíná velkým písmenem
        /// </summary>
        /// <param name="userInput">Uživatelský vstup</param>
        private bool IsFirstLetterUppercase(string userInput)
        {
            return char.IsUpper(userInput[0]);
        }

        /// <summary>
        /// Metoda vrací odpověď na otázku, jestli uživatelský vstup obsahuje speciální znaky
        /// </summary>
        /// <param name="userInput">Uživatelský vstup</param>
        private bool ContainsDigitsOrSpecialCharacters(string userInput)
        {
            foreach (char symbol in userInput)
            {
                if (char.IsDigit(symbol) || char.IsSymbol(symbol) || char.IsPunctuation(symbol))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Metoda vrací odpověď na otázku, jestli je kandidát starší osmnácti let
        /// </summary>
        /// <param name="birthDate"></param>
        private bool IsCandidateAdult(DateTime birthDate)
        {
            return birthDate.AddYears(18) <= DateTime.Now;
        }

        /// <summary>
        /// Metoda vrací odpověď na otázku, jestli zadané telefonní číslo odpovídá požadovaného formátu
        /// </summary>
        /// <param name="phoneNumber">Telefonní číslo</param>
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Regulární výraz pro formát +420 xxx xxx xxx
            string pattern = @"^\+420 \d{3} \d{3} \d{3}$";
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, pattern);
        }

        /// <summary>
        /// Metoda vrací odpověď na otázku, jestli zadaný e-mail odpovídá požadovanému formátu
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool IsValidEmail(string email)
        {
            // Regulární výraz pro kontrolu formátu e-mailu
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
        }
    }
}
