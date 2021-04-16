using System.ComponentModel.DataAnnotations;

namespace TestProgrammationConformit.Domains.Models
{
    public class Comment : Model<int>
    {
        public override int Id { get; set; }
        [Required] public int EventId { get; set; }
        [Required] public int StakeholderId { set; get; }

        [Required] public string Description { get; set; }
    }
}
