using System.ComponentModel.DataAnnotations;

namespace TestProgrammationConformit.Domains.Models
{
    public class Stakeholder
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
    }
}