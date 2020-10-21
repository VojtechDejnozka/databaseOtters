using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using databaseOtters.Model;

namespace databaseOtters.Pages.Places
{
    public class EditModel : PageModel
    {
        private readonly databaseOtters.Model.OtterDbContext _context;

        public EditModel(databaseOtters.Model.OtterDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["LocationId"] = new SelectList(_context.Locations, "LocationID", "LocationID");
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

            _context.Attach(Place).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceExists(Place.Name))
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

        private bool PlaceExists(string id)
        {
            return _context.Places.Any(e => e.Name == id);
        }
    }
}
