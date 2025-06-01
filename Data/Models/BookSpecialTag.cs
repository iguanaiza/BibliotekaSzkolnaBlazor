namespace BibliotekaSzkolnaBlazor.Data.Models
{
    public class BookSpecialTag
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<BookBookSpecialTag>? BookBookSpecialTags { get; set; }
    }
}
