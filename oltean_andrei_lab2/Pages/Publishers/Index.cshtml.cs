// Fisier: oltean_andrei_lab2/Pages/Publishers/Index.cshtml.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using oltean_andrei_lab2.Data;
using oltean_andrei_lab2.Models;
using oltean_andrei_lab2.ViewModels; // Am adaugat acest using

namespace oltean_andrei_lab2.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly oltean_andrei_lab2.Data.oltean_andrei_lab2Context _context;

        public IndexModel(oltean_andrei_lab2.Data.oltean_andrei_lab2Context context)
        {
            _context = context;
        }

        // Aceasta linie este din PDF-ul dumneavoastra
        public IList<Publisher> Publisher { get;set; } = default!; 
        
        // Acestea sunt noile proprietati din Pasul 4
        public PublisherIndexData PublisherData { get; set; }
        public int PublisherID { get; set; }
        public int BookID { get; set; }

        // Acesta este noul OnGetAsync din Pasul 4
        public async Task OnGetAsync(int? id, int? bookID)
        {
            PublisherData = new PublisherIndexData();
            PublisherData.Publishers = await _context.Publisher
                .Include(i => i.Books)
                .ThenInclude(c => c.Author)
                .OrderBy(i => i.PublisherName)
                .ToListAsync();
            
            if (id != null)
            {
                PublisherID = id.Value;
                Publisher publisher = PublisherData.Publishers
                    .Where(i => i.ID == id.Value).Single();
                PublisherData.Books = publisher.Books;
            }
        }
    }
}