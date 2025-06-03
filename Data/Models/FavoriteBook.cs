namespace BibliotekaSzkolnaBlazor.Data.Models
{
    public class FavoriteBook
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
