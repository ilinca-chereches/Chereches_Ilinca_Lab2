using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chereches_Ilinca_Lab2.Data;
using Chereches_Ilinca_Lab2.Models;

namespace Chereches_Ilinca_Lab2.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly Chereches_Ilinca_Lab2.Data.Chereches_Ilinca_Lab2Context _context;

        public IndexModel(Chereches_Ilinca_Lab2.Data.Chereches_Ilinca_Lab2Context context)
        {
            _context = context;
        }

        public IList<Author> Author { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Author = await _context.Author.ToListAsync();
        }
    }
}
