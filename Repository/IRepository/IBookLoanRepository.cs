using BibliotekaSzkolnaBlazor.Data.Models;
using BibliotekaSzkolnaBlazor.DataTransferObjects;

namespace BibliotekaSzkolnaBlazor.Repository.IRepository
{
    public interface IBookLoanRepository
    {
        Task<List<BookLoan>> GetUserLoansAsync(string userId);
        Task<List<BookLoan>> GetActiveLoansAsync(string userId);
        Task<BookLoan?> GetLoanByIdAsync(int loanId);
        Task AddLoanAsync(BookLoan loan);
        Task ReturnBookAsync(int loanId, DateTime returnDate);
        Task<bool> IsBookCopyAvailableAsync(int bookCopyId);
        Task ProlongLoanAsync(int loanId);
    }
}
