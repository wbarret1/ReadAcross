namespace ReadAcross.Models
{
    public class Reference
    {
        public long Id { get; set; }
        public string? Risdata { get; set; }
        public long? FunctionalGroupId { get; set; }
        public long? ReactionId { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Functional Group")]
        public virtual FunctionalGroup? FunctionalGroup { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Named Reaction")]
        public virtual NamedReaction? Reaction { get; set; }
    }
}
