namespace BibliotekaSzkolnaBlazor.DataTransferObjects
{
    public class LoanDto
    {

        public int LoanId { get; set; }


        public string UserId { get; set; } = string.Empty;
        public int BookCopyId { get; set; }


        public DateTime BorrowDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool WasProlonged { get; set; }
        public bool IsReturned => ReturnDate.HasValue;
        public decimal? PenaltyAmount { get; set; }


        public string? Title { get; set; }
        public string? AuthorFullName { get; set; }
        public string? Signature { get; set; }
        public int? LibraryCardNumber { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
    }
}
