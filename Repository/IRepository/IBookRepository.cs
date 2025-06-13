using BibliotekaSzkolnaBlazor.DataTransferObjects;
using System.Threading.Tasks;

namespace BibliotekaSzkolnaBlazor.Repository.IRepository
{
    public interface IBookRepository
    {
        public Task<IEnumerable<BookGetDto>> GetBooksAsync();
        public Task<BookGetDto?> GetBookByIdAsync(int id);
        public Task<IEnumerable<BookGetDto>> GetDeletedBooksAsync();
        public Task<IEnumerable<BookGetDto>> GetLekturyAsync();
        public Task<IEnumerable<BookGetDto>> GetPodrecznikiAsync();
        public Task<IEnumerable<BookGetDto>> GetPozostaleAsync();
        public Task<IEnumerable<BookGetDto>> GetBooksByTagAsync(string tagName);
        public Task<BookGetDto> CreateBookAsync(BookUpsertDto dto);
        public Task<BookGetDto?> UpdateBookAsync(int id, BookUpsertDto dto);
        public Task<BookGetDto?> BinBookAsync(int id);
        public Task<bool> DeleteBookAsync(int id);
        public Task<BookGetDto?> RestoreBookAsync(int id);
    }
}
