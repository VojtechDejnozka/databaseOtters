using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using databaseOtters.Model;

namespace databaseOtters.Pages.Places
{
    public class DetailsModel : PageModel
    {
        private readonly databaseOtters.Model.OtterDbContext _context;

        public DetailsModel(databaseOtters.Model.OtterDbContext context)
        {
            _context = context;
        }

        public Place Place { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Place = await _context.Places
                .Include(p => p.Location).FirstOrDefaultAsync(m => m.Name == id);

            if (Place == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
