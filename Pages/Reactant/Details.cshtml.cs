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
    public class DetailsModel : PageModel
    {
        private readonly ReadAcross.Data.ReadAcrossContext _context;

        public DetailsModel(ReadAcross.Data.ReadAcrossContext context)
        {
            _context = context;
        }

        public Reactant Reactant { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reactant = await _context.Reactant.FirstOrDefaultAsync(m => m.Id == id);
            if (reactant == null)
            {
                return NotFound();
            }
            else
            {
                Reactant = reactant;
            }
            return Page();
        }
    }
}
