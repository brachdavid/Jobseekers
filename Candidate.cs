using System.ComponentModel.DataAnnotations;

namespace Jobseekers
{
    /// <summary>
    /// Třída Candidate reprezentuje entitu kandidáta v databázi
    /// </summary>
    public class Candidate
    {
        /// <summary>
        /// Unikátní identifikátor kandidáta
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Křestní jméno kandidáta
        /// </summary>
        public string FirstName { get; set; } = "";
        /// <summary>
        /// Příjmení kandidáta
        /// </summary>
        public string LastName { get; set; } = "";
        /// <summary>
        /// Datum narození kandidáta
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Město, v němž kandidát bydlí
        /// </summary>
        public string City { get; set; } = "";
        /// <summary>
        /// Telefonní číslo kandidáta
        /// </summary>
        public string PhoneNumber { get; set; } = "";
        /// <summary>
        /// E-mailová adresa kandidáta
        /// </summary>
        public string Email { get; set; } = "";
        /// <summary>
        /// Seznam programovacích jazyků, které kandidát ovládá
        /// </summary>
        public List<ProgrammingLanguage> ProgrammingLanguages { get; set; } = [];
    }
}
