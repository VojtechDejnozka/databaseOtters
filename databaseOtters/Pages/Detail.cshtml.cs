using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using databaseOtters.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace databaseOtters.Pages
{
    public class DetailModel : PageModel
    {
        private readonly OtterDbContext _db;
        public Otter Otter { get; set; }
        public DetailModel(OtterDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            foreach (Otter p in _db.Otters) if (p.TattooID == id) Otter = p;
        }
    }
}
