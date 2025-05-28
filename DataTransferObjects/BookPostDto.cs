using System.ComponentModel.DataAnnotations;

namespace BibliotekaSzkolnaBlazor.DataTransferObjects
{
    public class BookPostDto
    {
        [Required(ErrorMessage = "Wprowadź tytuł książki (maksymalnie 60 znaków).")]
        [StringLength(60, ErrorMessage = "Niepoprawny tytuł: wpisz maksymalnie 60 znaków")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Wprowadź rok wydania książki (4 cyfry).")]
        [StringLength(4, ErrorMessage = "Niepoprawny rok: wprowadź 4 cyfry.")]
        public string Year { get; set; }

        [Required(ErrorMessage = "Wprowadź krótki opis książki (maksymalnie 255 znaków).")]
        [StringLength(255, ErrorMessage= "Niepoprawny opis: wpisz maksymalnie 255 znaków")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Wprowadź numer ISBN książki (13 cyfr).")]
        [StringLength(13, ErrorMessage = "Niepoprawny ISBN: wprowadź 13 cyfr.")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "Wprowadź ilość stron w książce (maksymalnie 4 cyfry).")]
        [StringLength(4, ErrorMessage = "Wprowadź maksymalnie 4 cyfry")]
        public string PageCount { get; set; }

        public bool IsVisible { get; set; }

        [Required(ErrorMessage = "Wybierz autora książki.")]
        public int BookAuthorId { get; set; }

        [Required(ErrorMessage = "Wybierz wydawcę książki.")]
        public int BookPublisherId { get; set; }

        [Required(ErrorMessage = "Wybierz do jakiej serii należy książka.")]
        public int BookSeriesId { get; set; }

        [Required(ErrorMessage = "Wybierz rodzaj książki.")]
        public int BookTypeId { get; set; }

        [Required(ErrorMessage = "Wybierz kategorię książki.")]
        public int BookCategoryId { get; set; }

        public List<int> BookGenreIds { get; set; }
    }
}
