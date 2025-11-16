using oltean_andrei_lab2.Data;
using oltean_andrei_lab2.Models;
using oltean_andrei_lab2.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace oltean_andrei_lab2.Pages.Publishers
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly oltean_andrei_lab2.Data.oltean_andrei_lab2Context _context;

        public IndexModel(oltean_andrei_lab2.Data.oltean_andrei_lab2Context context)
        {
            _context = context;
        }

        public PublisherIndexData PublisherData { get; set; }
        public int PublisherID { get; set; }
        public int BookID { get; set; }

        public async Task OnGetAsync(int? id, int? bookID)
        {
            PublisherData = new PublisherIndexData();
            PublisherData.Publishers = await _context.Publisher
                .Include(p => p.Books)
                .ThenInclude(b => b.Author)
                .OrderBy(p => p.PublisherName)
                .ToListAsync();

            if (id != null)
            {
                PublisherID = id.Value;
                Publisher publisher = PublisherData.Publishers
                    .Single(p => p.ID == id.Value);
                PublisherData.Books = publisher.Books;
            }
        }
    }
}