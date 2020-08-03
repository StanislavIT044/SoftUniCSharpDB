namespace BookShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BookShop.Data.Models.Enums;
    using Newtonsoft.Json;

    public class Book
    {
        public Book()
        {
            this.AuthorsBooks = new HashSet<AuthorBook>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public decimal Price { get; set; }

        [MaxLength(5000)]
        public int Pages { get; set; }

        public DateTime PublishedOn  { get; set; }

        [JsonIgnore]
        public ICollection<AuthorBook> AuthorsBooks { get; set; }
    }
}
