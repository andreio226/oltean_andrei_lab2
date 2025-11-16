using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using oltean_andrei_lab2.Data;
using oltean_andrei_lab2.Models;

namespace oltean_andrei_lab2.Pages.Borrowings
{
    public class CreateModel : PageModel
    {
        private readonly oltean_andrei_lab2.Data.oltean_andrei_lab2Context _context;

        public CreateModel(oltean_andrei_lab2.Data.oltean_andrei_lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["BookID"] = new SelectList(_context.Book, "ID", "Title");
            ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
            return Page();
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Borrowing.Add(Borrowing);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}