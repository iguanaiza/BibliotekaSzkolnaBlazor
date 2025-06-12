namespace BibliotekaSzkolnaBlazor.DataTransferObjects
{
    public class LoanGetDto
    {
        public int LoanId { get; set; }

        public string Signature { get; set; }
        public string Title { get; set; }
        public string AuthorFullName { get; set; }

        public int LibraryCardNumber { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }

        public decimal PenaltyAmount { get; set; }
        public bool WasProlonged { get; set; }
    }
}
