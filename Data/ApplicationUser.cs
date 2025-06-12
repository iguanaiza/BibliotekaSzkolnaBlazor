using BibliotekaSzkolnaBlazor.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BibliotekaSzkolnaBlazor.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int LibraryId { get; set; } //karta biblioteczna, 5 cyfr np 12345

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Class { get; set; }

        public ICollection<BookLoan> BookLoans { get; set; }

        public ICollection<FavoriteBook> FavoriteBooks { get; set; }

        public ICollection<BookReservationCart> ReservationCart { get; set; }
    }
}
