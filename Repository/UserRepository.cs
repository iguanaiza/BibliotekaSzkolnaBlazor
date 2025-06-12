using BibliotekaSzkolnaBlazor.Data;
using BibliotekaSzkolnaBlazor.Data.Models;
using BibliotekaSzkolnaBlazor.DataTransferObjects;
using BibliotekaSzkolnaBlazor.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BibliotekaSzkolnaBlazor.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserGetDto>> GetUsersAsync()
        {
            var users = await _context.Users
               .Include(u => u.BookLoans)
                   .ThenInclude(l => l.BookCopy)
                       .ThenInclude(c => c.Book)
               .Include(u => u.FavoriteBooks)
                   .ThenInclude(fb => fb.Book)
               .ToListAsync();

            return users.Select(user => new UserGetDto
            {
                Id = user.Id,
                Email = user.Email,
                LibraryId = user.LibraryId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Class = user.Class,
                BookLoans = user.BookLoans.ToList(),
                FavoriteBooks = user.FavoriteBooks.ToList()
            });
        }

        public async Task<UserGetDto?> GetUserByIdAsync(string userId)
        {
            var user = await _context.Users
                    .Include(u => u.BookLoans)
                        .ThenInclude(l => l.BookCopy)
                            .ThenInclude(c => c.Book)
                    .Include(u => u.FavoriteBooks)
                        .ThenInclude(fb => fb.Book)
                    .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return null;

            return new UserGetDto
            {
                Id = user.Id,
                LibraryId = user.LibraryId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Class = user.Class,
                BookLoans = user.BookLoans.ToList(),
                FavoriteBooks = user.FavoriteBooks.ToList()
            };
        }

        public async Task<UserGetDto?> GetUserByLibraryIdAsync(int libraryId)
        {
            var user = await _context.Users
                .Include(u => u.BookLoans)
                    .ThenInclude(l => l.BookCopy)
                        .ThenInclude(c => c.Book)
                            .ThenInclude(b => b.BookAuthor)
                .Include(u => u.FavoriteBooks)
                    .ThenInclude(fb => fb.Book)
                        .ThenInclude(b => b.BookAuthor)
                .FirstOrDefaultAsync(u => u.LibraryId == libraryId);

            if (user == null)
                return null;

            return new UserGetDto
            {
                Id = user.Id,
                LibraryId = user.LibraryId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Class = user.Class,
                BookLoans = user.BookLoans.ToList(),
                FavoriteBooks = user.FavoriteBooks.ToList()
            };
        }

        public async Task<IdentityResult> CreateUserAsync(UserUpsertDto dto, string password)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                LibraryId = dto.LibraryId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Class = dto.Class
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return result;

            var roleResult = await _userManager.AddToRoleAsync(user, dto.Role);
            if (!roleResult.Succeeded)
                return IdentityResult.Failed(roleResult.Errors.ToArray());

            return IdentityResult.Success;
        }

        public async Task<UserGetDto?> UpdateUserAsync(string userId, UserUpsertDto dto)
        {
            var user = await _context.Users
                .Include(u => u.BookLoans)
                .Include(u => u.FavoriteBooks)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return null;
            
            user.LibraryId = dto.LibraryId;
            user.Email = dto.Email;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Class = dto.Class;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return null;

            return new UserGetDto
            {
                Id = user.Id,
                LibraryId = user.LibraryId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Class = user.Class
            };
        }

        public async Task<bool> DisableUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100);
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
