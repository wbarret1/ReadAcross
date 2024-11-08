using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadAcross.Data;
using ReadAcross.Models;

namespace ReadAcross.Pages_Reference
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
        ViewData["FunctionalGroupId"] = new SelectList(_context.FunctionalGroup, "Id", "Name");
        ViewData["ReactionId"] = new SelectList(_context.NamedReaction, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Reference Reference { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Reference.Add(Reference);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
