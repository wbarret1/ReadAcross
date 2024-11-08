using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadAcross.Data;
using ReadAcross.Models;

namespace ReadAcross.Pages_NamedReaction
{
    public class CreateModel : PageModel
    {
        private readonly ReadAcross.Data.ReadAcrossContext _context;

        public CreateModel(ReadAcross.Data.ReadAcrossContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CatalystId"] = new SelectList(_context.Catalyst, "Id", "Id");
        ViewData["FunctionalGroupId"] = new SelectList(_context.FunctionalGroup, "Id", "Name");
        ViewData["SolventId"] = new SelectList(_context.Set<Solvent>(), "Id", "Id");
            return Page();
        }

        [BindProperty]
        public NamedReaction NamedReaction { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.NamedReaction.Add(NamedReaction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
