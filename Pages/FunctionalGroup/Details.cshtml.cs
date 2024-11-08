using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReadAcross.Data;
using ReadAcross.Models;

namespace ReadAcross.Pages_FunctionalGroup
{
    public class DetailsModel : PageModel
    {
        private readonly ReadAcross.Data.ReadAcrossContext _context;

        public DetailsModel(ReadAcross.Data.ReadAcrossContext context)
        {
            _context = context;
        }

        public FunctionalGroup FunctionalGroup { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var functionalgroup = await _context.FunctionalGroup.FirstOrDefaultAsync(m => m.Id == id);
            if (functionalgroup == null)
            {
                return NotFound();
            }
            else
            {
                FunctionalGroup = functionalgroup;
            }
            return Page();
        }
    }
}
