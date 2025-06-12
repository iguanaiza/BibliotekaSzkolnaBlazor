using BibliotekaSzkolnaBlazor.Data.Models;
using BibliotekaSzkolnaBlazor.Data;
using BibliotekaSzkolnaBlazor.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BibliotekaSzkolnaBlazor.Repository
{
    public class BookLoanRepository : IBookLoanRepository
    {
        private readonly ApplicationDbContext _context;

        public BookLoanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookLoan>> GetUserLoansAsync(string userId)
        {
            return await _context.BookLoans
                .Include(bl => bl.BookCopy)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.BookAuthor)
                .Where(bl => bl.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<BookLoan>> GetActiveLoansAsync(string userId)
        {
            return await _context.BookLoans
                .Include(bl => bl.BookCopy)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.BookAuthor)
                .Where(bl => bl.UserId == userId && bl.ReturnDate == null)
                .ToListAsync();
        }

        public async Task<BookLoan?> GetLoanByIdAsync(int loanId)
        {
            return await _context.BookLoans
                .Include(bl => bl.BookCopy)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.BookAuthor)
                .FirstOrDefaultAsync(bl => bl.Id == loanId);
        }

        public async Task AddLoanAsync(BookLoan loan)
        {
            // Sprawdź dostępność egzemplarza (czy nie jest aktualnie wypożyczony bez zwrotu)
            bool isAvailable = await IsBookCopyAvailableAsync(loan.BookCopyId);
            if (!isAvailable)
                throw new InvalidOperationException("Egzemplarz książki nie jest dostępny do wypożyczenia.");

            loan.BorrowDate = DateTime.UtcNow;
            loan.DueDate = loan.BorrowDate.AddDays(30);
            loan.WasProlonged = false;
            loan.ReturnDate = null;

            _context.BookLoans.Add(loan);
            await _context.SaveChangesAsync();
        }

        public async Task ReturnBookAsync(int loanId, DateTime returnDate)
        {
            var loan = await _context.BookLoans.FirstOrDefaultAsync(l => l.Id == loanId);

            if (loan == null)
                throw new InvalidOperationException("Nie znaleziono wypożyczenia.");

            if (loan.ReturnDate != null)
                throw new InvalidOperationException("Książka została już zwrócona.");

            loan.ReturnDate = returnDate;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsBookCopyAvailableAsync(int bookCopyId)
        {
            // Egzemplarz jest dostępny jeśli nie ma aktywnego wypożyczenia (bez daty zwrotu)
            bool hasActiveLoan = await _context.BookLoans
                .AnyAsync(bl => bl.BookCopyId == bookCopyId && bl.ReturnDate == null);

            return !hasActiveLoan;
        }

        public async Task ProlongLoanAsync(int loanId)
        {
            var loan = await _context.BookLoans.FirstOrDefaultAsync(l => l.Id == loanId);

            if (loan == null)
                throw new InvalidOperationException("Nie znaleziono wypożyczenia.");

            if (loan.ReturnDate != null)
                throw new InvalidOperationException("Nie można prolongować zwróconej książki.");

            if (loan.WasProlonged)
                throw new InvalidOperationException("Wypożyczenie zostało już przedłużone.");

            if (loan.DueDate == null)
                throw new InvalidOperationException("Brak daty zwrotu do przedłużenia.");

            loan.DueDate = loan.DueDate.Value.AddDays(30);
            loan.WasProlonged = true;

            await _context.SaveChangesAsync();
        }
    }
}
