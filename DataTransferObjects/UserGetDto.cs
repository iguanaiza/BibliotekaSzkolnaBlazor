using BibliotekaSzkolnaBlazor.Data.Models;

namespace BibliotekaSzkolnaBlazor.DataTransferObjects
{
    public class UserGetDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public int LibraryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }

        public List<BookLoan> BookLoans { get; set; } = new();

        public List<FavoriteBook> FavoriteBooks { get; set; } = new();

        public decimal TotalPenaltyAmount =>
            BookLoans?.Where(l => l.PenaltyAmount > 0)
                      .Sum(l => l.PenaltyAmount) ?? 0;
        public IList<string> Roles { get; set; }
    }
}
