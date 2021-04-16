using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TestProgrammationConformit.Domains.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Stakeholder: Model<int>
    {
        public override int Id { get; set; }
        [Required] public string Name { get; set; }
    }
}
