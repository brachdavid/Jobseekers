using System.ComponentModel.DataAnnotations;

namespace Jobseekers
{
    public class ProgrammingLanguage
    {
        [Key]
        public int Id { get; set; }
        public string Language { get; set; } = "";
        public List<Candidate> Candidates { get; set; } = [];
    }
}
