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

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
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
            throw new NotImplementedException();
        }

        public async Task<UserGetDto?> UpdateUserAsync(string userId, UserUpsertDto dto)
        {
            throw new NotImplementedException();
        }
  
        public async Task<bool> DisableUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
