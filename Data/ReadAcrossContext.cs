using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadAcross.Models;

namespace ReadAcross.Data
{
    public class ReadAcrossContext : DbContext
    {
        public ReadAcrossContext (DbContextOptions<ReadAcrossContext> options)
            : base(options)
        {
        }

        public DbSet<ReadAcross.Models.Catalyst> Catalyst { get; set; } = default!;
        public DbSet<ReadAcross.Models.Compound> Compound { get; set; } = default!;
        public DbSet<ReadAcross.Models.FunctionalGroup> FunctionalGroup { get; set; } = default!;
        public DbSet<ReadAcross.Models.NamedReaction> NamedReaction { get; set; } = default!;
        public DbSet<ReadAcross.Models.Reactant> Reactant { get; set; } = default!;
        public DbSet<ReadAcross.Models.Reference> Reference { get; set; } = default!;
        public DbSet<ReadAcross.Models.Solvent> Solvent { get; set; } = default!;
    }
}
