using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using oltean_andrei_lab2.Data;
using oltean_andrei_lab2.Models;
using System.Linq;

namespace oltean_andrei_lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly oltean_andrei_lab2Context _context;

        public IndexModel(oltean_andrei_lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; }
        public BookData BookD { get; set; }
        public int BookID { get; set; }
        public int CategoryID { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID)
        {
            BookD = new BookData();

            BookD.Books = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category) // Această linie este crucială
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            if (id != null)
            {
                BookID = id.Value;
                Book book = BookD.Books.Single(i => i.ID == id.Value);
                BookD.Categories = book.BookCategories.Select(s => s.Category);
            }
        }
    }
}