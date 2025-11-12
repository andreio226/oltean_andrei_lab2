using oltean_andrei_lab2.Data;
using oltean_andrei_lab2.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
            BookD = new BookData();

            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";
            CurrentFilter = searchString;
            
            IQueryable<Book> booksIQ = _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
                .AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                string searchStringLower = searchString.ToLower();
                
                booksIQ = booksIQ.Where(s =>
                    s.Title.ToLower().Contains(searchStringLower) 
                    || (s.Author != null &&
                        (s.Author.FirstName.ToLower().Contains(searchStringLower)
                         || s.Author.LastName.ToLower().Contains(searchStringLower)))
                );
            }
            
            BookD.Books = await booksIQ.ToListAsync();

            switch (sortOrder)
            {
                case "title_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s => s.Title).ToList();
                    break;
                case "author_desc":
                    BookD.Books = BookD.Books.OrderByDescending(s => s.Author?.FullName).ToList();
                    break;
                case "author":
                    BookD.Books = BookD.Books.OrderBy(s => s.Author?.FullName).ToList();
                    break;
                default:
                    BookD.Books = BookD.Books.OrderBy(s => s.Title).ToList();
                    break;
            }

            if (id != null)
            {
                BookID = id.Value;
                Book book = BookD.Books
                    .FirstOrDefault(i => i.ID == id.Value); 
                if (book != null)
                {
                    BookD.Categories = book.BookCategories.Select(s => s.Category);
                }
            }
            
            Book = BookD.Books.ToList();
        }
    }
}