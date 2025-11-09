using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Chereches_Ilinca_Lab2.Data;
using Chereches_Ilinca_Lab2.Models;

namespace Chereches_Ilinca_Lab2.Pages.Borrowings
{
    public class EditModel : PageModel
    {
        private readonly Chereches_Ilinca_Lab2.Data.Chereches_Ilinca_Lab2Context _context;

        public EditModel(Chereches_Ilinca_Lab2.Data.Chereches_Ilinca_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //am scris eu si inainte era var borrowing =  await _context.Borrowing.FirstOrDefaultAsync(m => m.ID == id); 
                Borrowing = await _context.Borrowing
                .Include(b => b.Book)
                 .ThenInclude(b => b.Author)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Borrowing == null)
            {
                return NotFound();
            }
            //Borrowing = borrowing;

            //am scris eu
            var bookList = _context.Book
                .Include(b => b.Author)
                .Select(x => new
        {
            x.ID,
            BookFullName = x.Title + " - " + x.Author.LastName + " " + x.Author.FirstName
        });
            //pana aici
            ViewData["BookID"] = new SelectList(bookList, "ID", "BookFullName", Borrowing.BookID);
            ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName", Borrowing.MemberID);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var bookList = _context.Book
                  .Include(b => b.Author)
                  .Select(x => new
                  {
                      x.ID,
                      BookFullName = x.Title + " - " + x.Author.LastName + " " + x.Author.FirstName
                  });

                ViewData["BookID"] = new SelectList(bookList, "ID", "BookFullName", Borrowing.BookID);
                ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName", Borrowing.MemberID);
                return Page();
            }

            _context.Attach(Borrowing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowingExists(Borrowing.ID))
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

        private bool BorrowingExists(int id)
        {
            return _context.Borrowing.Any(e => e.ID == id);
        }
    }
}
