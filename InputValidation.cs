using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobseekers
{
    class InputValidation
    {
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

        public string GetValidatedPhoneNumber(string userInput)
        {
            while (string.IsNullOrWhiteSpace(userInput = Console.ReadLine()?.Trim() ?? "") || !IsValidPhoneNumber(userInput))
            {
                Console.WriteLine("Neplatné telefonní číslo! Číslo musí být ve formátu +420 xxx xxx xxx a obsahovat pouze číslice.");
                Console.Write("Zkuste to znovu: ");
            }
            return userInput;
        }

        public string GetValidatedEmail(string userInput)
        {
            while (string.IsNullOrWhiteSpace(userInput = Console.ReadLine()?.Trim() ?? "") || !IsValidEmail(userInput))
            {
                Console.WriteLine("Neplatný e-mail! Zadejte e-mailovou adresu ve formátu example@example.com.");
                Console.Write("Zkuste to znovu: ");
            }
            return userInput;
        }

        public int GetValidatedId(int userInput)
        {
            while (!int.TryParse(Console.ReadLine(), out userInput))
            {
                Console.WriteLine("Neplatné ID. Zadejte prosím znovu:");
            }
            return userInput;
        }

        private bool IsFirstLetterUppercase(string userInput)
        {
            return char.IsUpper(userInput[0]);
        }

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

        private bool IsCandidateAdult(DateTime birthDate)
        {
            return birthDate.AddYears(18) <= DateTime.Now;
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Regulární výraz pro formát +420 xxx xxx xxx
            string pattern = @"^\+420 \d{3} \d{3} \d{3}$";
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, pattern);
        }

        private bool IsValidEmail(string email)
        {
            // Regulární výraz pro kontrolu formátu e-mailu
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
        }
    }
}
