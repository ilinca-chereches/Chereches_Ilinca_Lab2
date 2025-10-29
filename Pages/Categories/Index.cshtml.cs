using Chereches_Ilinca_Lab2.Data;
using Chereches_Ilinca_Lab2.Models;
using Chereches_Ilinca_Lab2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chereches_Ilinca_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Chereches_Ilinca_Lab2.Data.Chereches_Ilinca_Lab2Context _context;

        public IndexModel(Chereches_Ilinca_Lab2.Data.Chereches_Ilinca_Lab2Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;
        public CategoryIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }
        public int BookID { get; set; }

        public async Task OnGetAsync(int? id, int? bookID)   //modifica tot cu category de la pct 4
        {
            CategoryData = new CategoryIndexData();

            CategoryData.Categories = await _context.Category
                .Include(c => c.BookCategories)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.Author)
                .OrderBy(c => c.CategoryName)
                .ToListAsync();

            if (id != null)
            {
                CategoryID = id.Value;

                Category category = CategoryData.Categories
                    .Where(c => c.ID == id.Value)
                    .Single();

                CategoryData.Books = category.BookCategories
                    .Select(bc => bc.Book)
                    .ToList();
            }
        }
    }
}
