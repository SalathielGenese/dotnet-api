using System;
using System.ComponentModel.DataAnnotations;

namespace TestProgrammationConformit.Domains.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required] public int EventId { get; set; }
        [Required] public int StakeholderId { set; get; }

        [Required] public DateTime DateTime { get; set; }
        [Required] public string Description { get; set; }
    }
}