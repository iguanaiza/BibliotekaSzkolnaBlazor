using BibliotekaSzkolnaBlazor.Data.Models;
using BibliotekaSzkolnaBlazor.Data;
using BibliotekaSzkolnaBlazor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using BibliotekaSzkolnaBlazor.DataTransferObjects;

namespace BibliotekaSzkolnaBlazor.Repository
{
    public class BookLoanRepository : IBookLoanRepository
    {
        private readonly ApplicationDbContext _context;

        public BookLoanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LoanDto>> GetUserLoansAsync(string userId)
        {
            return await _context.BookLoans
                .Include(bl => bl.BookCopy)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.BookAuthor)
                .Include(bl => bl.User)
                .Where(bl => bl.UserId == userId)
                .OrderByDescending(bl => bl.BorrowDate)
                .Select(bl => new LoanDto
                {
                    LoanId = bl.Id,
                    UserId = bl.UserId,
                    BookCopyId = bl.BookCopyId,
                    Title = bl.BookCopy.Book.Title,
                    AuthorFullName = bl.BookCopy.Book.BookAuthor.Name + " " + bl.BookCopy.Book.BookAuthor.Surname,
                    Signature = bl.BookCopy.Signature,
                    BorrowDate = bl.BorrowDate,
                    DueDate = bl.DueDate,
                    ReturnDate = bl.ReturnDate,
                    WasProlonged = bl.WasProlonged,
                    PenaltyAmount = bl.PenaltyAmount,
                    LibraryCardNumber = bl.User.LibraryId,
                    UserFirstName = bl.User.FirstName,
                    UserLastName = bl.User.LastName
                })
                .ToListAsync();
        }

        public async Task<List<LoanDto>> GetActiveLoansAsync(string userId)
        {
            return await _context.BookLoans
                .Include(bl => bl.BookCopy)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.BookAuthor)
                .Include(bl => bl.User)
                .Where(bl => bl.UserId == userId && bl.ReturnDate == null)
                .Select(bl => new LoanDto
                {
                    LoanId = bl.Id,
                    UserId = bl.UserId,
                    BookCopyId = bl.BookCopyId,
                    Title = bl.BookCopy.Book.Title,
                    AuthorFullName = bl.BookCopy.Book.BookAuthor.Name + " " + bl.BookCopy.Book.BookAuthor.Surname,
                    Signature = bl.BookCopy.Signature,
                    BorrowDate = bl.BorrowDate,
                    DueDate = bl.DueDate,
                    ReturnDate = bl.ReturnDate,
                    WasProlonged = bl.WasProlonged,
                    PenaltyAmount = bl.PenaltyAmount,
                    LibraryCardNumber = bl.User.LibraryId,
                    UserFirstName = bl.User.FirstName,
                    UserLastName = bl.User.LastName
                })
                .ToListAsync();
        }

        public async Task<LoanDto?> GetLoanByIdAsync(int loanId)
        {
            return await _context.BookLoans
                .Include(bl => bl.BookCopy)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.BookAuthor)
                .Include(bl => bl.User)
                .Where(bl => bl.Id == loanId)
                .Select(bl => new LoanDto
                {
                    LoanId = bl.Id,
                    UserId = bl.UserId,
                    BookCopyId = bl.BookCopyId,
                    Title = bl.BookCopy.Book.Title,
                    AuthorFullName = bl.BookCopy.Book.BookAuthor.Name + " " + bl.BookCopy.Book.BookAuthor.Surname,
                    Signature = bl.BookCopy.Signature,
                    BorrowDate = bl.BorrowDate,
                    DueDate = bl.DueDate,
                    ReturnDate = bl.ReturnDate,
                    WasProlonged = bl.WasProlonged,
                    PenaltyAmount = bl.PenaltyAmount,
                    LibraryCardNumber = bl.User.LibraryId,
                    UserFirstName = bl.User.FirstName,
                    UserLastName = bl.User.LastName
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddLoanAsync(LoanDto loanDto)
        {
            bool isAvailable = await IsBookCopyAvailableAsync(loanDto.BookCopyId);
            if (!isAvailable)
                throw new InvalidOperationException("Egzemplarz książki nie jest dostępny do wypożyczenia.");

            var bookLoan = new BookLoan
            {
                UserId = loanDto.UserId,
                BookCopyId = loanDto.BookCopyId,
                BorrowDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
                WasProlonged = false,
                ReturnDate = null,
                PenaltyAmount = 0m
            };

            _context.BookLoans.Add(bookLoan);
            await _context.SaveChangesAsync();
        }

        public async Task ReturnLoanAsync(int loanId, DateTime returnDate)
        {
            var loan = await _context.BookLoans.FindAsync(loanId);
            if (loan == null || loan.ReturnDate != null)
                return;

            loan.ReturnDate = returnDate;

            if (loan.DueDate.HasValue && returnDate > loan.DueDate.Value)
            {
                var daysLate = (returnDate.Date - loan.DueDate.Value.Date).Days;
                loan.PenaltyAmount = daysLate * 1.00m; // 1 zł/dzień
            }
            else
            {
                loan.PenaltyAmount = 0m;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsBookCopyAvailableAsync(int bookCopyId)
        {
            bool hasActiveLoan = await _context.BookLoans
                .AnyAsync(bl => bl.BookCopyId == bookCopyId && bl.ReturnDate == null);

            return !hasActiveLoan;
        }

        public async Task ProlongLoanAsync(int loanId)
        {
            var loan = await _context.BookLoans.FindAsync(loanId);
            if (loan == null || loan.ReturnDate != null || loan.WasProlonged) return;

            loan.DueDate = loan.DueDate?.AddDays(30);
            loan.WasProlonged = true;

            await _context.SaveChangesAsync();
        }

        public async Task<(List<LoanDto> loans, int totalCount)> GetActiveLoansPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.BookLoans
                .Where(l => l.ReturnDate == null)
                .Include(l => l.User)
                .Include(l => l.BookCopy)
                    .ThenInclude(c => c.Book)
                        .ThenInclude(b => b.BookAuthor);

            var totalCount = await query.CountAsync();

            var loans = await query
                .OrderByDescending(l => l.BorrowDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(l => new LoanDto
                {
                    LoanId = l.Id,
                    Signature = l.BookCopy.Signature,
                    Title = l.BookCopy.Book.Title,
                    AuthorFullName = l.BookCopy.Book.BookAuthor.Name + " " + l.BookCopy.Book.BookAuthor.Surname,

                    LibraryCardNumber = l.User.LibraryId,
                    UserFirstName = l.User.FirstName,
                    UserLastName = l.User.LastName,

                    BorrowDate = l.BorrowDate,
                    DueDate = l.DueDate ?? DateTime.MinValue,
                    PenaltyAmount = l.PenaltyAmount,
                    WasProlonged = l.WasProlonged
                })
                .ToListAsync();

            return (loans, totalCount);
        }
    }
}
