using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProgrammationConformit.Domains.Models
{
    public class Event : Model<int>
    {
        public override int Id { get; set; }
        [Required] public int StakeholderId { set; get; }

        [Required] public string Description { get; set; }
        [Required] [MaxLength(100)] public string Title { get; set; }

        public Stakeholder Stakeholder { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
