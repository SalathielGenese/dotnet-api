namespace TestProgrammationConformit.Domains.Models
{
    public abstract class Model<TId>
    {
        public abstract TId Id { get; set; }
    }
}
