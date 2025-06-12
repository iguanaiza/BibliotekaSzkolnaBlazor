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

        public bool Available => BookLoans == null || BookLoans.All(l => l.ReturnDate != null);//czy aktualnie dostepny tak/nie

        public int BookId { get; set; } //klucz ksiazki (oryginalu)
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; } = null!;//odwolanie, musi miec oryginal

        public ICollection<BookReservationCart> ReservationCart { get; set; }

        public ICollection<BookLoan> BookLoans { get; set; }
    }
}

/* Przyjęty schemat sygnatury:
 * Rodzaj (LP – literatura piękna, PR - podręczniki),
 * Kategoria główna (np. FAN fantastyka, GEO geografia)
 * Inicjały autora (nazwisko, imie),
 * Tytuł (pierwsze litery słów tytułowych lub pierwsze 2 litery tytułu)
 * Kolejność w serii lub numerze tomu (opcjonalnie), dla PR klasa
 * Numer kopii
 *
 * Przykład: LP.FAN.RJ.HP.1-001
 */