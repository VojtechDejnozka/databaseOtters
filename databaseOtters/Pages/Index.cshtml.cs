using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using databaseOtters.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace databaseOtters.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly OtterDbContext _db;

        public List<Otter> Otters { get; set; }
        public IndexModel(ILogger<IndexModel> logger, OtterDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet()
        {
            Otters = _db.Otters.ToList();
        }
    }
}
