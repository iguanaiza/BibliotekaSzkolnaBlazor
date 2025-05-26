using BibliotekaSzkolnaBlazor.DataTransferObjects;

namespace BibliotekaSzkolnaBlazor.Services
{
    public interface IBookService<T> where T : class
    {
        Task<List<BookGetDto>> GetBooksAsync();
        Task<BookGetDto?> GetBookByIdAsync(int id);
        Task<BookGetDto> CreateBookAsync(BookPostDto dto);
        Task<BookGetDto?> UpdateBookAsync(int id, BookPutDto dto);
        Task<bool> DeleteBookAsync(int id);
    }
}
