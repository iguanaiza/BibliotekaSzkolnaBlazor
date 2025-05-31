using BibliotekaSzkolnaBlazor.Data;
using BibliotekaSzkolnaBlazor.Data.Models;
using BibliotekaSzkolnaBlazor.DataTransferObjects;
using BibliotekaSzkolnaBlazor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BibliotekaSzkolnaBlazor.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookGetDto>> GetBooksAsync()
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
                    IsDeleted= b.IsDeleted,
                    IsVisible = b.IsVisible,
                    ImageUrl = b.ImageUrl,
                    SpecialTag = b.SpecialTag,
                    BookAuthor = b.BookAuthor.Surname + ", " + b.BookAuthor.Name,
                    BookPublisher = b.BookPublisher.Name,
                    BookSeries = b.BookSeries.Title,
                    BookType = b.BookType.Title,
                    BookCategory = b.BookCategory.Name,
                    BookGenres = b.BookBookGenres.Select(bb => bb.BookGenre.Title).ToList(),
                    CopyCount = b.BookCopies != null ? b.BookCopies.Count : 0
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
                .Include(b => b.BookCopies)
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
                ImageUrl = b.ImageUrl,
                SpecialTag = b.SpecialTag,
                BookAuthor = b.BookAuthor.Surname + ", " + b.BookAuthor.Name,
                BookPublisher = b.BookPublisher.Name,
                BookSeries = b.BookSeries.Title,
                BookType = b.BookType.Title,
                BookCategory = b.BookCategory.Name,
                BookGenres = b.BookBookGenres
                            .Select(g => g.BookGenre.Title).ToList(),
                BookCopies = b.BookCopies?
                            .Select(c => new CopyGetDto
                            {
                                Signature = c.Signature,
                                Available = c.Available,
                                InventoryNum = c.InventoryNum
                            }).ToList(),
                CopyCount = b.BookCopies != null ? b.BookCopies.Count : 0
            };
        }

        public async Task<BookGetDto> CreateBookAsync(BookUpsertDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Year = dto.Year,
                Description = dto.Description,
                Isbn = dto.Isbn,
                PageCount = dto.PageCount,
                IsVisible = dto.IsVisible,
                ImageUrl = dto.ImageUrl,
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

        public async Task<BookGetDto?> UpdateBookAsync(int id, BookUpsertDto dto)
        {
            var book = await _context.Books
                .Include(b => b.BookAuthor)
                .Include(b => b.BookPublisher)
                .Include(b => b.BookSeries)
                .Include(b => b.BookType)
                .Include(b => b.BookCategory)
                .Include(b => b.BookBookGenres)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return null;

            book.Title = dto.Title;
            book.Year = dto.Year;
            book.Description = dto.Description;
            book.Isbn = dto.Isbn;
            book.PageCount = dto.PageCount;
            book.IsVisible = dto.IsVisible;
            book.ImageUrl = dto.ImageUrl;
            book.SpecialTag = dto.SpecialTag;
            book.BookAuthorId = dto.BookAuthorId;
            book.BookPublisherId = dto.BookPublisherId;
            book.BookSeriesId = dto.BookSeriesId;
            book.BookTypeId = dto.BookTypeId;
            book.BookCategoryId = dto.BookCategoryId;

            if (dto.BookGenreIds != null)
            {
                //usunięcie starych relacji
                _context.BooksBookGenres.RemoveRange(book.BookBookGenres);

                //dodanie nowych
                book.BookBookGenres = dto.BookGenreIds
                    .Select(id => new BookBookGenre { BookGenreId = id, BookId = book.Id })
                    .ToList();
            }

            await _context.SaveChangesAsync();

            return await GetBookByIdAsync(id);
        }

        public async Task<BookGetDto?> BinBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return null;

            book.IsDeleted = true;
            book.IsVisible = false;

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
