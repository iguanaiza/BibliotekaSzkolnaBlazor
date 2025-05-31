using BibliotekaSzkolnaBlazor.DataTransferObjects;

namespace BibliotekaSzkolnaBlazor.Repository.IRepository
{
    public interface IBookRepository
    {
        public Task<IEnumerable<BookGetDto>> GetBooksAsync();
        public Task<BookGetDto?> GetBookByIdAsync(int id);
        public Task<BookGetDto> CreateBookAsync(BookUpsertDto dto);
        public Task<BookGetDto?> UpdateBookAsync(int id, BookUpsertDto dto);
        public Task<BookGetDto?> BinBookAsync(int id);
        public Task<bool> DeleteBookAsync(int id);
    }
}
