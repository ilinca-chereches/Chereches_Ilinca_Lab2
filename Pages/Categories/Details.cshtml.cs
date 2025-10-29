using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Chereches_Ilinca_Lab2.Data;
using Chereches_Ilinca_Lab2.Models;

namespace Chereches_Ilinca_Lab2.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly Chereches_Ilinca_Lab2.Data.Chereches_Ilinca_Lab2Context _context;

        public DetailsModel(Chereches_Ilinca_Lab2.Data.Chereches_Ilinca_Lab2Context context)
        {
            _context = context;
        }

        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            return Page();
        }
    }
}
