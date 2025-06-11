using BibliotekaSzkolnaBlazor.Data.Models;

namespace BibliotekaSzkolnaBlazor.Repository.IRepository
{
    public interface IBookLoanRepository
    {
        Task<List<BookLoan>> GetUserLoansAsync(string userId);
        Task<BookLoan?> GetLoanByIdAsync(int loanId);
        Task AddLoanAsync(BookLoan loan);
        Task ReturnBookAsync(int loanId, DateTime returnDate);
        Task<List<BookLoan>> GetActiveLoansAsync(string userId);
    }
}
