using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobseekers
{
    class ProgrammingLanguage
    {
        [Key]
        public int Id { get; set; }
        public string Language { get; set; } = "";
        public List<Candidate> Candidates { get; set; } = new List<Candidate>();
    }
}
