using BibliotekaSzkolnaBlazor.Data.Models;
using BibliotekaSzkolnaBlazor.DataTransferObjects;
using BibliotekaSzkolnaBlazor.Repository.IRepository;

namespace BibliotekaSzkolnaBlazor.Services
{
    public class LoanService
    {
        private readonly IBookReservationCartRepository _cartRepo;
        private readonly IBookLoanRepository _loanRepo;

        public LoanService(IBookReservationCartRepository cartRepo, IBookLoanRepository loanRepo)
        {
            _cartRepo = cartRepo;
            _loanRepo = loanRepo;
        }

        public async Task FinalizeCartAsync(string userId)
        {
            var cartItems = await _cartRepo.GetUserCartAsync(userId);

            if (cartItems.Count == 0)
                throw new InvalidOperationException("Koszyk jest pusty.");

            foreach (var item in cartItems)
            {
                bool isAvailable = await _loanRepo.IsBookCopyAvailableAsync(item.BookCopyId);
                if (!isAvailable)
                    throw new InvalidOperationException($"Egzemplarz '{item.BookCopy.Signature}' jest niedostępny.");
            }

            foreach (var item in cartItems)
            {
                var loan = new LoanDto
                {
                    UserId = userId,
                    BookCopyId = item.BookCopyId,
                    BorrowDate = DateTime.UtcNow,
                    DueDate = DateTime.UtcNow.AddDays(30),
                    WasProlonged = false,
                    ReturnDate = null
                };

                await _loanRepo.AddLoanAsync(loan);
            }

            await _cartRepo.FinalizeCartAsync(userId);
        }
    }
}
