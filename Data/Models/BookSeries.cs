﻿using System.ComponentModel.DataAnnotations;

namespace BibliotekaSzkolnaBlazor.Data.Models
{
    public class BookSeries
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
