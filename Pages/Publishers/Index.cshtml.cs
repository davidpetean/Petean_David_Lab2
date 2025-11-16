using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Petean_David_Lab2.Data;
using Petean_David_Lab2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petean_David_Lab2.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly Petean_David_Lab2Context _context;

        public IndexModel(Petean_David_Lab2Context context)
        {
            _context = context;
        }

        public IList<Publisher> Publisher { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Publisher = await _context.Publisher.ToListAsync();
        }
    }
}

