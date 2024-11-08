using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReadAcross.Data;
using ReadAcross.Models;

namespace ReadAcross.Pages_Reference
{
    public class IndexModel : PageModel
    {
        private readonly ReadAcross.Data.ReadAcrossContext _context;

        public IndexModel(ReadAcross.Data.ReadAcrossContext context)
        {
            _context = context;
        }

        public IList<Reference> Reference { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Reference = await _context.Reference
                .Include(r => r.FunctionalGroup)
                .Include(r => r.Reaction).ToListAsync();
        }
    }
}
