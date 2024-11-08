using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReadAcross.Data;
using ReadAcross.Models;

namespace ReadAcross.Pages_Catalyst
{
    public class DeleteModel : PageModel
    {
        private readonly ReadAcross.Data.ReadAcrossContext _context;

        public DeleteModel(ReadAcross.Data.ReadAcrossContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Catalyst Catalyst { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalyst = await _context.Catalyst.FirstOrDefaultAsync(m => m.Id == id);

            if (catalyst == null)
            {
                return NotFound();
            }
            else
            {
                Catalyst = catalyst;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalyst = await _context.Catalyst.FindAsync(id);
            if (catalyst != null)
            {
                Catalyst = catalyst;
                _context.Catalyst.Remove(Catalyst);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
