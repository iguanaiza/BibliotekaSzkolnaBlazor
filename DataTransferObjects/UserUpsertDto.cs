﻿namespace BibliotekaSzkolnaBlazor.DataTransferObjects
{
    public class UserUpsertDto
    {
        public int LibraryId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }
        public string Role { get; set; }
    }
}
