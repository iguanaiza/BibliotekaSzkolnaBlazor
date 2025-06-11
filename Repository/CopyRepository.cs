using BibliotekaSzkolnaBlazor.Data;
using BibliotekaSzkolnaBlazor.Data.Models;
using BibliotekaSzkolnaBlazor.DataTransferObjects;
using BibliotekaSzkolnaBlazor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BibliotekaSzkolnaBlazor.Repository
{
    public class CopyRepository : ICopyRepository
    {
        private readonly ApplicationDbContext _context;

        public CopyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CopyGetDto>> GetCopiesAsync()
        {
            return await _context.BookCopies
                .Include(c => c.Book).ThenInclude(b => b.BookAuthor)
                .Include(c => c.BookLoans)
                .Select(c => new CopyGetDto
                {
                    Id = c.Id,
                    Signature = c.Signature,
                    InventoryNum = c.InventoryNum,
                    Available = c.Available,
                    BookTitle = c.Book.Title,
                    BookImageUrl = c.Book.ImageUrl,
                    AuthorName = c.Book.BookAuthor.Surname + ", " + c.Book.BookAuthor.Name,
                    BookId = c.BookId
                })
                .ToListAsync();
        }

        public async Task<CopyGetDto?> GetCopyByIdAsync(int id)
        {
            return await _context.BookCopies
            .Include(c => c.Book).ThenInclude(b => b.BookAuthor)
            .Where(c => c.Id == id)
            .Select(c => new CopyGetDto
            {
                Id = c.Id,
                Signature = c.Signature,
                InventoryNum = c.InventoryNum,
                Available = c.Available,
                BookTitle = c.Book.Title,
                BookImageUrl = c.Book.ImageUrl,
                AuthorName = c.Book.BookAuthor.Surname + ", " + c.Book.BookAuthor.Name,
                BookId = c.BookId
            })
            .FirstOrDefaultAsync();
        }

        public async Task<CopyGetDto> CreateCopyAsync(CopyUpsertDto dto)
        {
            var copy = new BookCopy
            {
                Signature = dto.Signature,
                InventoryNum = dto.InventoryNum,
                BookId = dto.BookId
            };

            _context.BookCopies.Add(copy);
            await _context.SaveChangesAsync();

            copy = await _context.BookCopies
                .Include(c => c.Book)
                .FirstAsync(c => c.Id == copy.Id);

            return new CopyGetDto
            {
                Id = copy.Id,
                Signature = copy.Signature,
                InventoryNum = copy.InventoryNum,
                BookTitle = copy.Book.Title,
                BookId = copy.BookId,
                Available = copy.Available
            };
        }

        public async Task<CopyGetDto?> UpdateCopyAsync(int id, CopyUpsertDto dto)
        {
            var copy = await _context.BookCopies
            .Include(c => c.Book)
            .Include(c => c.BookLoans)
            .FirstOrDefaultAsync(c => c.Id == id);

            if (copy == null)
                return null;

            copy.Signature = dto.Signature;
            copy.InventoryNum = dto.InventoryNum;
            copy.BookId = dto.BookId;

            await _context.SaveChangesAsync();

            return new CopyGetDto
            {
                Id = copy.Id,
                Signature = copy.Signature,
                InventoryNum = copy.InventoryNum,
                BookTitle = copy.Book.Title
            };
        }


        public async Task<CopyGetDto?> BinCopyAsync(int id)
        {
            throw new NotImplementedException();
        }



        public async Task<bool> DeleteCopyAsync(int id)
        {
            throw new NotImplementedException();
        }

    }      
}
