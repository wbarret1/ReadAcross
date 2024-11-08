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

namespace ReadAcross.Pages_NamedReaction
{
    public class EditModel : PageModel
    {
        private readonly ReadAcross.Data.ReadAcrossContext _context;

        public EditModel(ReadAcross.Data.ReadAcrossContext context)
        {
            _context = context;
        }

        [BindProperty]
        public NamedReaction NamedReaction { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var namedreaction =  await _context.NamedReaction.FirstOrDefaultAsync(m => m.Id == id);
            if (namedreaction == null)
            {
                return NotFound();
            }
            NamedReaction = namedreaction;
           ViewData["CatalystId"] = new SelectList(_context.Catalyst, "Id", "Id");
           ViewData["FunctionalGroupId"] = new SelectList(_context.FunctionalGroup, "Id", "Name");
           ViewData["SolventId"] = new SelectList(_context.Set<Solvent>(), "Id", "Id");
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

            _context.Attach(NamedReaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NamedReactionExists(NamedReaction.Id))
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

        private bool NamedReactionExists(long id)
        {
            return _context.NamedReaction.Any(e => e.Id == id);
        }
    }
}
