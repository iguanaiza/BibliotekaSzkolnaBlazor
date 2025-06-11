using BibliotekaSzkolnaBlazor.Data;
using BibliotekaSzkolnaBlazor.Data.Models;
using BibliotekaSzkolnaBlazor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BibliotekaSzkolnaBlazor.Repository
{
    public class FavoriteBookRepository : IFavoriteBookRepository
    {
        private readonly ApplicationDbContext _context;

        public FavoriteBookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FavoriteBook>> GetUserFavoritesAsync(string userId)
        {
            return await _context.FavoriteBooks
                .Include(fb => fb.Book)
                .Where(fb => fb.UserId == userId)
                .ToListAsync();
        }

        public async Task AddFavoriteAsync(string userId, int bookId)
        {
            bool exists = await IsFavoriteAsync(userId, bookId);
            if (exists) return;

            var favorite = new FavoriteBook
            {
                UserId = userId,
                BookId = bookId
            };
            _context.FavoriteBooks.Add(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFavoriteAsync(string userId, int bookId)
        {
            var favorite = await _context.FavoriteBooks
                .FirstOrDefaultAsync(fb => fb.UserId == userId && fb.BookId == bookId);
            if (favorite != null)
            {
                _context.FavoriteBooks.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsFavoriteAsync(string userId, int bookId)
        {
            return await _context.FavoriteBooks
                .AnyAsync(fb => fb.UserId == userId && fb.BookId == bookId);
        }
    }

}
