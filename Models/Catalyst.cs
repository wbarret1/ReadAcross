namespace ReadAcross.Models
{
    public class Catalyst
    {
        public Catalyst()
        {
            AppNamedreaction = new HashSet<NamedReaction>();
        }

        public long Id { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<NamedReaction> AppNamedreaction { get; set; }
    }
}
