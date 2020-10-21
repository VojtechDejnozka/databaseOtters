using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using databaseOtters.Model;

namespace databaseOtters.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly databaseOtters.Model.OtterDbContext _context;

        public DetailsModel(databaseOtters.Model.OtterDbContext context)
        {
            _context = context;
        }

        public Otter Otter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Otter = await _context.Otters
                .Include(o => o.Founder)
                .Include(o => o.Mother)
                .Include(o => o.Place).FirstOrDefaultAsync(m => m.TattooID == id);

            if (Otter == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
