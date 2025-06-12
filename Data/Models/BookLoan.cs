using Microsoft.EntityFrameworkCore;

namespace BibliotekaSzkolnaBlazor.Data.Models
{
    public class BookLoan
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int BookCopyId { get; set; }
        public BookCopy BookCopy { get; set; }

        public DateTime BorrowDate { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        [Precision(4, 2)]
        public decimal PenaltyAmount { get; set; }

        public bool WasProlonged { get; set; } = false;

        public bool IsReturned => ReturnDate != null;
    }
}
