using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using oltean_andrei_lab2.Data;
using oltean_andrei_lab2.Models;
using oltean_andrei_lab2.ViewModels;

namespace oltean_andrei_lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly oltean_andrei_lab2.Data.oltean_andrei_lab2Context _context;

        public IndexModel(oltean_andrei_lab2.Data.oltean_andrei_lab2Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;
        
        public CategoryIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }

        public async Task OnGetAsync(int? id)
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
                    .Single(c => c.ID == id.Value);
                CategoryData.Books = category.BookCategories.Select(bc => bc.Book);
            }
        }
    }
}