using BibliotekaSzkolnaBlazor.Data.Models;
using BibliotekaSzkolnaBlazor.DataTransferObjects;

namespace BibliotekaSzkolnaBlazor.Repository.IRepository
{
    public interface IBookLoanRepository
    {
        Task<List<LoanDto>> GetUserLoansAsync(string userId);
        Task<List<LoanDto>> GetActiveLoansAsync(string userId);
        Task<LoanDto?> GetLoanByIdAsync(int loanId);
        Task AddLoanAsync(LoanDto loan);
        Task ReturnLoanAsync(int loanId, DateTime returnDate);
        Task<bool> IsBookCopyAvailableAsync(int bookCopyId);
        Task ProlongLoanAsync(int loanId);

        Task<(List<LoanDto> loans, int totalCount)> GetActiveLoansPagedAsync(int pageNumber, int pageSize);

    }
}
