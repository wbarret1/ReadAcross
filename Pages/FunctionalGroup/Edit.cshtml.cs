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

namespace ReadAcross.Pages_FunctionalGroup
{
    public class EditModel : PageModel
    {
        private readonly ReadAcross.Data.ReadAcrossContext _context;

        public EditModel(ReadAcross.Data.ReadAcrossContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FunctionalGroup FunctionalGroup { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionalgroup =  await _context.FunctionalGroup.FirstOrDefaultAsync(m => m.Id == id);
            if (functionalgroup == null)
            {
                return NotFound();
            }
            FunctionalGroup = functionalgroup;
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

            _context.Attach(FunctionalGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FunctionalGroupExists(FunctionalGroup.Id))
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

        private bool FunctionalGroupExists(long id)
        {
            return _context.FunctionalGroup.Any(e => e.Id == id);
        }
    }
}
