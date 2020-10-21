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
    public class IndexModel : PageModel
    {
        private readonly databaseOtters.Model.OtterDbContext _context;

        public IndexModel(databaseOtters.Model.OtterDbContext context)
        {
            _context = context;
        }

        public IList<Place> Place { get;set; }

        public async Task OnGetAsync()
        {
            Place = await _context.Places
                .Include(p => p.Location).ToListAsync();
        }
    }
}
