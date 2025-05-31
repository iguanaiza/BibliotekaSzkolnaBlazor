using BibliotekaSzkolnaBlazor.DataTransferObjects;

namespace BibliotekaSzkolnaBlazor.Repository.IRepository
{
    public interface ICopyRepository
    {
        public Task<IEnumerable<CopyGetDto>> GetCopiesAsync();
        public Task<CopyGetDto?> GetCopyByIdAsync(int id);
        public Task<CopyGetDto> CreateCopyAsync(CopyUpsertDto dto);
        public Task<CopyGetDto?> UpdateCopyAsync(int id, CopyUpsertDto dto);
        public Task<CopyGetDto?> BinCopyAsync(int id);
        public Task<bool> DeleteCopyAsync(int id);
    }
}
