using System.ComponentModel.DataAnnotations;

namespace BibliotekaSzkolnaBlazor.DataTransferObjects
{
    public class CopyUpsertDto
    {
        [Required(ErrorMessage = "Sygnatura jest wymagana.")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Sygnatura musi mieć od 3 do 60 znaków.")]
        public string Signature { get; set; } = null!;

        [Required(ErrorMessage = "Numer inwentarzowy jest wymagany.")]
        [Range(1, int.MaxValue, ErrorMessage = "Numer inwentarzowy musi być dodatni.")]
        public int InventoryNum { get; set; }

        [Required(ErrorMessage = "BookId jest wymagane.")]
        public int BookId { get; set; }
    }
}
