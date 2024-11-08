using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReadAcross.Data;
using ReadAcross.Models;

namespace ReadAcross.Pages_Solvent
{
    public class DetailsModel : PageModel
    {
        private readonly ReadAcross.Data.ReadAcrossContext _context;

        public DetailsModel(ReadAcross.Data.ReadAcrossContext context)
        {
            _context = context;
        }

        public Solvent Solvent { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solvent = await _context.Solvent.FirstOrDefaultAsync(m => m.Id == id);
            if (solvent == null)
            {
                return NotFound();
            }
            else
            {
                Solvent = solvent;
            }
            return Page();
        }
    }
}
