namespace BibliotekaSzkolnaBlazor.DataTransferObjects
{
    public class LoanHistoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool WasProlonged { get; set; }
        public bool IsReturned => ReturnDate.HasValue;
    }
}
