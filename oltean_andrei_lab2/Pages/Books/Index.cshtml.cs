using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using oltean_andrei_lab2.Data;
using oltean_andrei_lab2.Models;

namespace oltean_andrei_lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly oltean_andrei_lab2Context _context;

        public IndexModel(oltean_andrei_lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;
        public async Task OnGetAsync()
        {
            Book = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author) 
                .ToListAsync();
        }
    }
}