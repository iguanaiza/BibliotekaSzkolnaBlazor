using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotekaSzkolnaBlazor.Data.Models
{
    public class BookCopy
    {
        public int Id { get; set; }

        [Required]
        public string Signature { get; set; } = null!; //syngatura, format:

        public int InventoryNum { get; set; } //numer inwentarzowy, format: 00000

        [Required]
        public bool Available { get; set; } //czy aktualnie dostepny tak/nie

        public int BookId { get; set; } //klucz ksiazki (oryginalu)
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; } = null!;//odwolanie, musi miec oryginal
    }
}

/* Przyjęty schemat sygnatury:
 * Rodzaj literatury (np. L – literatura piękna),
 * Nazwisko autora (pierwsza litera),
 * Tytuł (pierwsze 2-3 litery)
 * Kolejność w serii lub numerze tomu (opcjonalnie)
 * Numer kopii
 *
 * Przykład: LP.R.HP.1-001
 * Literatura piękna, Rowling, Harry Potter, Tom I, egzemplarz 1
 */