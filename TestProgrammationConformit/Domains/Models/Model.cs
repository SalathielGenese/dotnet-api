using System;
using System.ComponentModel.DataAnnotations;

namespace TestProgrammationConformit.Domains.Models
{
    public abstract class Model<TId>
    {
        public abstract TId Id { get; set; }
        [Required] public DateTime CreatedAt { get; set; }
        [Required] public DateTime UpdatedAt { get; set; }
    }
}
