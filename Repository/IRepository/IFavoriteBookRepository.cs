using BibliotekaSzkolnaBlazor.Data.Models;

namespace BibliotekaSzkolnaBlazor.Repository.IRepository
{
    public interface IFavoriteBookRepository
    {
        Task<List<FavoriteBook>> GetUserFavoritesAsync(string userId);
        Task AddFavoriteAsync(string userId, int bookId);
        Task RemoveFavoriteAsync(string userId, int bookId);
        Task<bool> IsFavoriteAsync(string userId, int bookId);
    }
}
