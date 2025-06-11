using BibliotekaSzkolnaBlazor.Data.Models;
using BibliotekaSzkolnaBlazor.Data;
using BibliotekaSzkolnaBlazor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BibliotekaSzkolnaBlazor.Repository
{
    public class BookReservationCartRepository : IBookReservationCartRepository
    {
        private readonly ApplicationDbContext _context;

        public BookReservationCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddToCartAsync(string userId, int bookCopyId)
        {
            // Sprawdź, czy egzemplarz jest już w koszyku
            bool exists = await IsInCartAsync(userId, bookCopyId);
            if (exists)
                return; // albo rzuć wyjątek lub zignoruj, zależy od logiki

            var cartItem = new BookReservationCart
            {
                UserId = userId,
                BookCopyId = bookCopyId,
                AddedAt = DateTime.UtcNow,
                // IsFinalized domyślnie false
            };

            _context.BookReservationCarts.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsInCartAsync(string userId, int bookCopyId)
        {
            return await _context.BookReservationCarts
                .AnyAsync(rc => rc.UserId == userId && rc.BookCopyId == bookCopyId && !rc.IsFinalized);
        }

        public async Task<List<BookReservationCart>> GetUserCartAsync(string userId)
        {
            return await _context.BookReservationCarts
                .Include(rc => rc.BookCopy)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.BookAuthor)
                .Where(rc => rc.UserId == userId && !rc.IsFinalized)
                .ToListAsync();
        }

        public async Task RemoveFromCartAsync(int bookCopyId, string userId)
        {
            var item = await _context.BookReservationCarts
                .FirstOrDefaultAsync(rc => rc.BookCopyId == bookCopyId && rc.UserId == userId && !rc.IsFinalized);

            if (item != null)
            {
                _context.BookReservationCarts.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(string userId)
        {
            var items = _context.BookReservationCarts.Where(rc => rc.UserId == userId && !rc.IsFinalized);
            _context.BookReservationCarts.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task FinalizeCartAsync(string userId)
        {
            var items = await _context.BookReservationCarts
                .Where(rc => rc.UserId == userId && !rc.IsFinalized)
                .ToListAsync();

            foreach (var item in items)
            {
                item.IsFinalized = true;
            }
            await _context.SaveChangesAsync();
        }
    }
}
