using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadAcross.Data;
using ReadAcross.Models;

namespace ReadAcross.Pages_FunctionalGroup
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
            return Page();
        }

        [BindProperty]
        public FunctionalGroup FunctionalGroup { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.FunctionalGroup.Add(FunctionalGroup);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
