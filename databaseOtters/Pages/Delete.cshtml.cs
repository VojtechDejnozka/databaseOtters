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
    public class DeleteModel : PageModel
    {
        private readonly databaseOtters.Model.OtterDbContext _context;

        public DeleteModel(databaseOtters.Model.OtterDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Otter = await _context.Otters.FindAsync(id);

            if (Otter != null)
            {
                _context.Otters.Remove(Otter);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
