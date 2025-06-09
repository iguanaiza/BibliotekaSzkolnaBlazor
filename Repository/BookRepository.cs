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

        #region GETs
        public async Task<IEnumerable<BookGetDto>> GetBooksAsync()
        {
            return await _context.Books
                .Where(b => !b.IsDeleted)
                .Include(b => b.BookAuthor)
                .Include(b => b.BookPublisher)
                .Include(b => b.BookSeries)
                .Include(b => b.BookType)
                .Include(b => b.BookCategory)
                .Include(b => b.BookBookGenres).ThenInclude(bb => bb.BookGenre)
                .Include(b => b.BookCopies).ThenInclude(c => c.BookLoans)
                .Include(b => b.BookBookSpecialTags).ThenInclude(bb => bb.BookSpecialTag)
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
                    Subject = b.Subject,
                    Class = b.Class,
                    BookAuthorId = b.BookAuthor.Id,
                    BookAuthor = b.BookAuthor.Surname + ", " + b.BookAuthor.Name,
                    BookPublisherId = b.BookPublisher.Id,
                    BookPublisher = b.BookPublisher.Name,
                    BookSeriesId = b.BookSeries != null ? b.BookSeries.Id : (int?)null,
                    BookSeries = b.BookSeries != null ? b.BookSeries.Title : null,
                    BookCategoryId = b.BookCategory.Id,
                    BookCategory = b.BookCategory.Name,
                    BookTypeId = b.BookType.Id,
                    BookType = b.BookType.Title,
                    BookGenreIds = b.BookBookGenres.Select(bg => bg.BookGenre.Id).ToList(),
                    BookGenres = b.BookBookGenres.Select(bg => bg.BookGenre.Title).ToList(),
                    BookSpecialTags = b.BookBookSpecialTags.Select(bb => bb.BookSpecialTag.Title).ToList(),
                    CopyCount = b.BookCopies != null ? b.BookCopies.Count : 0,
                    AvailableCopyCount = b.BookCopies.Count(c =>
                        c.BookLoans == null || c.BookLoans.All(l => l.ReturnDate != null))
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
                .Include(b => b.BookBookSpecialTags).ThenInclude(bb => bb.BookSpecialTag)
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
                Subject = b.Subject,
                Class = b.Class,
                BookAuthor = b.BookAuthor.Surname + ", " + b.BookAuthor.Name,
                BookPublisher = b.BookPublisher.Name,
                BookSeries = b.BookSeries.Title,
                BookType = b.BookType.Title,
                BookCategory = b.BookCategory.Name,
                BookGenres = b.BookBookGenres.Select(g => g.BookGenre.Title).ToList(),
                BookSpecialTags = b.BookBookSpecialTags.Select(bb => bb.BookSpecialTag.Title).ToList(),
                BookCopies = b.BookCopies?
                            .Select(c => new CopyGetDto
                            {
                                Id = c.Id,
                                Signature = c.Signature,
                                Available = c.Available,
                                InventoryNum = c.InventoryNum
                            }).ToList(),
                CopyCount = b.BookCopies != null ? b.BookCopies.Count : 0,
                //AvailableCopyCount = b.BookCopies.Count(c => c.Available)
            };
        }

        public async Task<IEnumerable<BookGetDto>> GetDeletedBooksAsync()
        {
            return await _context.Books
                .Where(b => b.IsDeleted)
                .Include(b => b.BookAuthor)
                .Include(b => b.BookPublisher)
                .Include(b => b.BookSeries)
                .Include(b => b.BookType)
                .Include(b => b.BookCategory)
                .Include(b => b.BookBookGenres).ThenInclude(bb => bb.BookGenre)
                .Include(b => b.BookCopies).ThenInclude(c => c.BookLoans)
                .Include(b => b.BookBookSpecialTags).ThenInclude(bb => bb.BookSpecialTag)
                .Select(b => new BookGetDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Year = b.Year,
                    Description = b.Description,
                    Isbn = b.Isbn,
                    PageCount = b.PageCount,
                    IsDeleted = b.IsDeleted,
                    IsVisible = b.IsVisible,
                    ImageUrl = b.ImageUrl,
                    BookAuthorId = b.BookAuthor.Id,
                    BookAuthor = b.BookAuthor.Surname + ", " + b.BookAuthor.Name,
                    BookPublisherId = b.BookPublisher.Id,
                    BookPublisher = b.BookPublisher.Name,
                    BookSeriesId = b.BookSeries != null ? b.BookSeries.Id : (int?)null,
                    BookSeries = b.BookSeries != null ? b.BookSeries.Title : null,
                    BookCategoryId = b.BookCategory.Id,
                    BookCategory = b.BookCategory.Name,
                    BookTypeId = b.BookType.Id,
                    BookType = b.BookType.Title,
                    BookGenreIds = b.BookBookGenres.Select(bg => bg.BookGenre.Id).ToList(),
                    BookGenres = b.BookBookGenres.Select(bg => bg.BookGenre.Title).ToList(),
                    BookSpecialTags = b.BookBookSpecialTags.Select(bb => bb.BookSpecialTag.Title).ToList(),
                    CopyCount = b.BookCopies != null ? b.BookCopies.Count : 0,
                    AvailableCopyCount = b.BookCopies.Count(c =>
                        c.BookLoans == null || c.BookLoans.All(l => l.ReturnDate != null))
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BookGetDto>> GetLekturyAsync()
        {
            const int CategoryId = 1;

            return await _context.Books
                .Include(b => b.BookAuthor)
                .Include(b => b.BookPublisher)
                .Include(b => b.BookCategory).Where(b => b.BookCategoryId == CategoryId)
                .Select(b => new BookGetDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Year = b.Year,
                    Subject = b.Subject,
                    Class = b.Class,
                    BookAuthor = b.BookAuthor.Surname + ", " + b.BookAuthor.Name,
                    BookPublisher = b.BookPublisher.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BookGetDto>> GetPodrecznikiAsync()
        {
            const int CategoryId = 2;

            return await _context.Books
                .Include(b => b.BookAuthor)
                .Include(b => b.BookPublisher)
                .Include(b => b.BookCategory).Where(b => b.BookCategoryId == CategoryId)
                .Select(b => new BookGetDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Year = b.Year,
                    Subject = b.Subject,
                    Class = b.Class,
                    BookAuthor = b.BookAuthor.Surname + ", " + b.BookAuthor.Name,
                    BookPublisher = b.BookPublisher.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BookGetDto>> GetBooksByTagAsync(string tagName)
        {
            return await _context.Books
                .Include(b => b.BookAuthor)
                .Include(b => b.BookBookSpecialTags).Where(b => b.BookBookSpecialTags.Any(bb => bb.BookSpecialTag.Title == tagName))
                .Select(b => new BookGetDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Year = b.Year,
                    Description = b.Description,
                    Isbn = b.Isbn,
                    PageCount = b.PageCount,
                    IsDeleted = b.IsDeleted,
                    IsVisible = b.IsVisible,
                    ImageUrl = b.ImageUrl,
                    BookAuthor = b.BookAuthor.Surname + ", " + b.BookAuthor.Name,
                    BookSpecialTags = b.BookBookSpecialTags.Select(bb => bb.BookSpecialTag.Title).ToList(),
                })
                .ToListAsync();
        }
        #endregion
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
                BookBookGenres = dto.BookGenreIds.Select(id => new BookBookGenre { BookGenreId = id }).ToList(),
                BookBookSpecialTags = dto.BookSpecialTagIds.Select(id => new BookBookSpecialTag { BookSpecialTagId = id }).ToList()
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
                .Include(b => b.BookBookSpecialTags).ThenInclude(bb => bb.BookSpecialTag)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return null;

            book.Title = dto.Title;
            book.Year = dto.Year;
            book.Description = dto.Description;
            book.Isbn = dto.Isbn;
            book.PageCount = dto.PageCount;
            book.IsVisible = dto.IsVisible;
            book.ImageUrl = dto.ImageUrl;
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

            if (dto.BookSpecialTagIds != null)
            {
                //usunięcie starych relacji
                _context.BooksBookSpecialTags.RemoveRange(book.BookBookSpecialTags);

                //dodanie nowych
                book.BookBookSpecialTags = dto.BookSpecialTagIds
                    .Select(id => new BookBookSpecialTag { BookSpecialTagId = id, BookId = book.Id })
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
            var book = await _context.Books
                .Include(b => b.BookCopies)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                return false;

            //Mozliwośc usunięcia tylko jeśli nie ma żadnych kopii
            if (book.BookCopies.Any())
                return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BookGetDto?> RestoreBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null) return null;

            book.IsDeleted = false;
            book.IsVisible = true;

            await _context.SaveChangesAsync();

            return await GetBookByIdAsync(id);
        }
    }
}
