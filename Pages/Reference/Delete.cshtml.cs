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
    public class DeleteModel : PageModel
    {
        private readonly ReadAcross.Data.ReadAcrossContext _context;

        public DeleteModel(ReadAcross.Data.ReadAcrossContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reference Reference { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reference = await _context.Reference.FirstOrDefaultAsync(m => m.Id == id);

            if (reference == null)
            {
                return NotFound();
            }
            else
            {
                Reference = reference;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reference = await _context.Reference.FindAsync(id);
            if (reference != null)
            {
                Reference = reference;
                _context.Reference.Remove(Reference);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
