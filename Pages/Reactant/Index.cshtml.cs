using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReadAcross.Data;
using ReadAcross.Models;

namespace ReadAcross.Pages_Reactant
{
    public class IndexModel : PageModel
    {
        private readonly ReadAcross.Data.ReadAcrossContext _context;

        public IndexModel(ReadAcross.Data.ReadAcrossContext context)
        {
            _context = context;
        }

        public IList<Reactant> Reactant { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Reactant = await _context.Reactant.ToListAsync();
        }
    }
}
