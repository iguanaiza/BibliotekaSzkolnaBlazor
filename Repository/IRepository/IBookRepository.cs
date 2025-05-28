using BibliotekaSzkolnaBlazor.DataTransferObjects;

namespace BibliotekaSzkolnaBlazor.Repository.IRepository
{
    public interface IBookRepository
    {
        public Task<IEnumerable<BookGetDto>> GetBooksAsync();
        public Task<BookGetDto?> GetBookByIdAsync(int id);
        public Task<BookGetDto> CreateBookAsync(BookPostDto dto);
        public Task<BookGetDto?> UpdateBookAsync(int id, BookPutDto dto);
        public Task<bool> DeleteBookAsync(int id);
    }
}
