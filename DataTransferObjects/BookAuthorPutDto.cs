using System.ComponentModel.DataAnnotations;

namespace BibliotekaSzkolnaBlazor.DataTransferObjects
{
    public class BookAuthorPutDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }
        public List<int> BooksIds { get; set; }
    }
}
