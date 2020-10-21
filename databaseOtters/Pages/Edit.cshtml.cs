using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using databaseOtters.Model;

namespace databaseOtters.Pages
{
    public class EditModel : PageModel
    {
        private readonly databaseOtters.Model.OtterDbContext _context;

        public EditModel(databaseOtters.Model.OtterDbContext context)
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
           ViewData["FounderId"] = new SelectList(_context.Users, "Id", "Id");
           ViewData["MotherId"] = new SelectList(_context.Otters, "TattooID", "TattooID");
           ViewData["PlaceName"] = new SelectList(_context.Places, "Name", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Otter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OtterExists(Otter.TattooID))
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

        private bool OtterExists(int? id)
        {
            return _context.Otters.Any(e => e.TattooID == id);
        }
    }
}
