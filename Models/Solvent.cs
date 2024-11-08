﻿namespace ReadAcross.Models
{
    public class Solvent
    {
        public Solvent()
        {
            AppNamedreaction = new HashSet<NamedReaction>();
        }

        public long Id { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<NamedReaction> AppNamedreaction { get; set; }
    }
}
