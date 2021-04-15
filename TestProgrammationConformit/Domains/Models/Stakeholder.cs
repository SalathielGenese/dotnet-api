using System.ComponentModel.DataAnnotations;

namespace TestProgrammationConformit.Domains.Models
{
    public class Stakeholder: Model<int>
    {
        public override int Id { get; set; }
        [Required] public string Name { get; set; }
    }
}
