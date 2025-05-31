using System.ComponentModel.DataAnnotations;

namespace BibliotekaSzkolnaBlazor.DataTransferObjects
{
    public class CopyGetDto
    {
        public int Id { get; set; }
        public string Signature { get; set; } = null!; 
        public int InventoryNum { get; set; }
        public bool Available { get; set; }
        public string Book { get; set; } = null!;
    }
}
