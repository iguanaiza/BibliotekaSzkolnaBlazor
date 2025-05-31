using System.ComponentModel.DataAnnotations;

namespace BibliotekaSzkolnaBlazor.DataTransferObjects
{
    public class CopyUpsertDto
    {
        public string Signature { get; set; } = null!;
        public int InventoryNum { get; set; }
        public bool Available { get; set; }
        public int BookId { get; set; }
    }
}
