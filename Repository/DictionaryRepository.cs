using BibliotekaSzkolnaBlazor.Data.Models;
using BibliotekaSzkolnaBlazor.Data;
using BibliotekaSzkolnaBlazor.DataTransferObjects;
using BibliotekaSzkolnaBlazor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BibliotekaSzkolnaBlazor.Repository
{
    public class DictionaryRepository : IDictionaryRepository
    {
        private readonly ApplicationDbContext _context;

        public DictionaryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<BookAuthor>> GetBookAuthorsAsync()
            => _context.BookAuthors.OrderBy(a => a.Surname).ThenBy(a => a.Name).ToListAsync();

        public Task<List<BookCategory>> GetBookCategoriesAsync()
            => _context.BookCategories.OrderBy(c => c.Name).ToListAsync();

        public Task<List<BookGenre>> GetBookGenresAsync()
            => _context.BookGenres.OrderBy(g => g.Title).ToListAsync();

        public Task<List<BookPublisher>> GetBookPublishersAsync()
            => _context.BookPublishers.OrderBy(p => p.Name).ToListAsync();

        public Task<List<BookSeries>> GetBookSeriesAsync()
            => _context.BookSeries.OrderBy(s => s.Title).ToListAsync();

        public Task<List<BookType>> GetBookTypesAsync()
            => _context.BookTypes.OrderBy(t => t.Title).ToListAsync();

        public Task<List<BookCopy>> GetBookCopiesAsync()
            => _context.BookCopies.OrderBy(c => c.Signature).ThenBy(c => c.InventoryNum).ThenBy(c => c.Available).ToListAsync();
    }
}
