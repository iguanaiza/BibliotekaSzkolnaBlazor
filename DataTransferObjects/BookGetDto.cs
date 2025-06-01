
using System.ComponentModel.DataAnnotations;

namespace BibliotekaSzkolnaBlazor.DataTransferObjects
{
    public class BookGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public string Description { get; set; } = null!;
        public string Isbn { get; set; } = null!;
        public int PageCount { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsVisible { get; set; }
        public string? ImageUrl { get; set; }
        public string BookAuthor { get; set; } = null!;
        public string BookPublisher { get; set; } = null!;
        public string BookSeries { get; set; } = null!;
        public string BookCategory { get; set; } = null!;
        public string BookType { get; set; } = null!;
        public List<string> BookGenres { get; set; } = null!;
        public List<string>? BookSpecialTags { get; set; }
        public List<CopyGetDto>? BookCopies { get; set; }
        public int CopyCount;
    }
}
