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
                .Where(bl => bl.UserId == userId)
                .ToListAsync();
        }

        public async Task<BookLoan?> GetLoanByIdAsync(int loanId)
        {
            return await _context.BookLoans
                .Include(bl => bl.BookCopy)
                    .ThenInclude(bc => bc.Book)
                .FirstOrDefaultAsync(bl => bl.Id == loanId);
        }

        public async Task AddLoanAsync(BookLoan loan)
        {
            _context.BookLoans.Add(loan);
            await _context.SaveChangesAsync();
        }

        public async Task ReturnBookAsync(int loanId, DateTime returnDate)
        {
            var loan = await _context.BookLoans.FindAsync(loanId);
            if (loan != null && loan.ReturnDate == null)
            {
                loan.ReturnDate = returnDate;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<BookLoan>> GetActiveLoansAsync(string userId)
        {
            return await _context.BookLoans
                .Include(bl => bl.BookCopy)
                    .ThenInclude(bc => bc.Book)
                .Where(bl => bl.UserId == userId && bl.ReturnDate == null)
                .ToListAsync();
        }
    }

}
