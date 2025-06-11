using BibliotekaSzkolnaBlazor.Data.Models;

namespace BibliotekaSzkolnaBlazor.Repository.IRepository
{
    public interface IBookReservationCartRepository
    {
        Task AddToCartAsync(string userId, int bookCopyId);
        Task<bool> IsInCartAsync(string userId, int bookCopyId);
        Task<List<BookReservationCart>> GetUserCartAsync(string userId);
        Task RemoveFromCartAsync(int bookCopyId, string userId);
        Task ClearCartAsync(string userId);
        Task FinalizeCartAsync(string userId);
    }
}
