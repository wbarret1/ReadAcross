using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReadAcross.Data;
using ReadAcross.Models;

namespace ReadAcross.Pages_Reference
{
    public class EditModel : PageModel
    {
        private readonly ReadAcross.Data.ReadAcrossContext _context;

        public EditModel(ReadAcross.Data.ReadAcrossContext context)
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

            var reference =  await _context.Reference.FirstOrDefaultAsync(m => m.Id == id);
            if (reference == null)
            {
                return NotFound();
            }
            Reference = reference;
           ViewData["FunctionalGroupId"] = new SelectList(_context.FunctionalGroup, "Id", "Name");
           ViewData["ReactionId"] = new SelectList(_context.NamedReaction, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Reference).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReferenceExists(Reference.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ReferenceExists(long id)
        {
            return _context.Reference.Any(e => e.Id == id);
        }
    }
}
