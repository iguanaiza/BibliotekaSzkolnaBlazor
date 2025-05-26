﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotekaSzkolnaBlazor.Models
{
    public class BookCopy
    {
        public int Id { get; set; }
        public bool Available { get; set; } //czy aktualnie dostepny tak/nie
        public int BookId { get; set; } //klucz ksiazki (oryginalu)

        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; } //odwolanie, musi miec oryginal
    }
}
