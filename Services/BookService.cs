using BibliotekaSzkolnaBlazor.Data;
using BibliotekaSzkolnaBlazor.DataTransferObjects;
using BibliotekaSzkolnaBlazor.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotekaSzkolnaBlazor.Services
{
    public class BookService : IBookService<Book>
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookGetDto>> GetBooksAsync()
        {
            return await _context.Books
                .Include(b => b.BookAuthor)
                .Include(b => b.BookPublisher)
                .Include(b => b.BookSeries)
                .Include(b => b.BookType)
                .Include(b => b.BookCategory)
                .Include(b => b.BookBookGenres).ThenInclude(bb => bb.BookGenre)
                .Select(b => new BookGetDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Year = b.Year,
                    Description = b.Description,
                    Isbn = b.Isbn,
                    PageCount = b.PageCount,
                    IsVisible = b.IsVisible,
                    BookAuthor = b.BookAuthor.Surname + ", " + b.BookAuthor.Name,
                    BookPublisher = b.BookPublisher.Name,
                    BookSeries = b.BookSeries.Title,
                    BookType = b.BookType.Title,
                    BookCategory = b.BookCategory.Name,
                    BookGenres = b.BookBookGenres.Select(bb => bb.BookGenre.Title).ToList()
                })
                .ToListAsync();
        }

        public async Task<BookGetDto?> GetBookByIdAsync(int id)
        {
            var b = await _context.Books
                .Include(b => b.BookAuthor)
                .Include(b => b.BookPublisher)
                .Include(b => b.BookSeries)
                .Include(b => b.BookType)
                .Include(b => b.BookCategory)
                .Include(b => b.BookBookGenres).ThenInclude(bb => bb.BookGenre)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (b == null) return null;

            return new BookGetDto
            {
                Id = b.Id,
                Title = b.Title,
                Year = b.Year,
                Description = b.Description,
                Isbn = b.Isbn,
                PageCount = b.PageCount,
                IsVisible = b.IsVisible,
                BookAuthor = b.BookAuthor.Surname + ", " + b.BookAuthor.Name,
                BookPublisher = b.BookPublisher.Name,
                BookSeries = b.BookSeries.Title,
                BookType = b.BookType.Title,
                BookCategory = b.BookCategory.Name,
                BookGenres = b.BookBookGenres.Select(g => g.BookGenre.Title).ToList()
            };
        }

        public async Task<BookGetDto> CreateBookAsync(BookPostDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Year = dto.Year,
                Description = dto.Description,
                Isbn = dto.Isbn,
                PageCount = dto.PageCount,
                IsVisible = dto.IsVisible,
                BookAuthorId = dto.BookAuthorId,
                BookPublisherId = dto.BookPublisherId,
                BookSeriesId = dto.BookSeriesId,
                BookTypeId = dto.BookTypeId,
                BookCategoryId = dto.BookCategoryId,
                BookBookGenres = dto.BookGenreIds.Select(id => new BookBookGenre { BookGenreId = id }).ToList()
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return await GetBookByIdAsync(book.Id) ?? throw new Exception("Book not found after creation.");
        }

        public async Task<BookGetDto?> UpdateBookAsync(int id, BookPutDto dto)
        {
            var book = await _context.Books
                .Include(b => b.BookBookGenres)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return null;

            foreach (var prop in typeof(BookPutDto).GetProperties())
            {
                var value = prop.GetValue(dto);
                if (value != null)
                {
                    var bookProp = typeof(Book).GetProperty(prop.Name);
                    if (bookProp != null && bookProp.CanWrite)
                    {
                        bookProp.SetValue(book, value);
                    }
                }
            }

            if (dto.BookGenreIds != null)
            {
                book.BookBookGenres = dto.BookGenreIds
                    .Select(id => new BookBookGenre { BookGenreId = id })
                    .ToList();
            }

            await _context.SaveChangesAsync();

            return await GetBookByIdAsync(id);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
