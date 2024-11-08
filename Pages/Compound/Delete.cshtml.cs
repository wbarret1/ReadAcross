using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReadAcross.Data;
using ReadAcross.Models;

namespace ReadAcross.Pages_Compound
{
    public class DeleteModel : PageModel
    {
        private readonly ReadAcross.Data.ReadAcrossContext _context;

        public DeleteModel(ReadAcross.Data.ReadAcrossContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Compound Compound { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compound = await _context.Compound.FirstOrDefaultAsync(m => m.Id == id);

            if (compound == null)
            {
                return NotFound();
            }
            else
            {
                Compound = compound;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compound = await _context.Compound.FindAsync(id);
            if (compound != null)
            {
                Compound = compound;
                _context.Compound.Remove(Compound);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
