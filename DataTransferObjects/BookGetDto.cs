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
        public string? Subject { get; set; }
        public string? Class { get; set; }
        public int BookAuthorId { get; set; }
        public string BookAuthor { get; set; } = null!;

        public int BookPublisherId { get; set; }
        public string BookPublisher { get; set; } = null!;

        public int? BookSeriesId { get; set; }
        public string? BookSeries { get; set; }

        public int BookCategoryId { get; set; }
        public string BookCategory { get; set; } = null!;

        public int BookTypeId { get; set; }
        public string BookType { get; set; } = null!;
        public List<int> BookGenreIds { get; set; } = new();
        public List<string> BookGenres { get; set; } = new();
        public List<string>? BookSpecialTags { get; set; }
        public List<CopyGetDto>? BookCopies { get; set; }
        public int CopyCount;
        public int AvailableCopyCount;
    }
}
