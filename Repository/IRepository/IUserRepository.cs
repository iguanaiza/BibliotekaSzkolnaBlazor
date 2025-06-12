using BibliotekaSzkolnaBlazor.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace BibliotekaSzkolnaBlazor.Repository.IRepository
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserGetDto>> GetUsersAsync();

        public Task<UserGetDto?> GetUserByIdAsync(string userId);
        public Task<UserGetDto?> GetUserByLibraryIdAsync(int libraryId);

        // Tworzenie użytkownika przez admina (z hasłem)
        Task<IdentityResult> CreateUserAsync(UserUpsertDto dto, string password);

        public Task<UserGetDto?> UpdateUserAsync(string userId, UserUpsertDto dto);

        Task<bool> DisableUserAsync(string userId); //wyłączenie konta
        Task<bool> DeleteUserAsync(string userId);  //trwałe usunięcie
    }
}
