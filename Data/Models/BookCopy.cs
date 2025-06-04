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
 * Rodzaj (LP – literatura piękna, P - podręczniki),
 * Kategoria główna (np. FAN fantastyka, GEO geografia)
 * Inicjały autora (nazwisko, imie),
 * Tytuł (pierwsze litery słów tytułowych lub pierwsze 3 litery tytułu)
 * Kolejność w serii lub numerze tomu (opcjonalnie)
 * Numer kopii
 *
 * Przykład: LP.RJ.HP.1-001

LP.DZI.GA.KIC.1-001
LP.DZI.GA.KIC.1-002
LP.DZI.GA.KIC.1-003

LP.DZI.GA.KIC.2-001
LP.DZI.GA.KIC.2-002

LP.DZI.GA.KIC.3-001
LP.DZI.GA.KIC.3-002
LP.DZI.GA.KIC.3-003
LP.DZI.GA.KIC.3-004

LP.FAN.GK.NIE.1-001
LP.FAN.GK.NIE.1-002
LP.FAN.GK.NIE.1-003

LP.FAN.GK.NIE.2-001
LP.FAN.GK.NIE.2-002


LP.FAN.GK.NIE.3-001
LP.FAN.GK.NIE.3-002

LP.FAN.RR.PER.1-001
LP.FAN.RR.PER.1-002
LP.FAN.RR.PER.1-003

LP.FAN.RR.PER.2-001
LP.FAN.RR.PER.2-002
LP.FAN.RR.PER.2-003

LP.FAN.RR.PER.3-001

LP.FAN.RR.PER.4-001
LP.FAN.RR.PER.4-002
LP.FAN.RR.PER.4-003

LP.FAN.RR.PER.5-001
LP.FAN.RR.PER.5-002

LP.FAN.RR.PER.6-001

LP.FAN.LGU.ZIE.1-001
LP.FAN.LGU.ZIE.1-002
LP.FAN.LGU.ZIE.1-003
LP.FAN.LGU.ZIE.1-004

LP.FAN.LGU.ZIE.2-001
LP.FAN.LGU.ZIE.2-002

LP.FAN.LGU.ZIE.3-001
LP.FAN.LGU.ZIE.3-002

LP.FAN.LGU.ZIE.4-001

LP.FAN.LGU.ZIE.5-001

LP.FAN.LGU.ZIE.6-001
 */