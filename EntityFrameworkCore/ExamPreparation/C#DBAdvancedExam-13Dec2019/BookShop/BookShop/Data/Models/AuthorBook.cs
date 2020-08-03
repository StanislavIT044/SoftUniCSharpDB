namespace BookShop.Data.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AuthorBook
    {

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        [JsonIgnore]
        public Author Author { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        [JsonIgnore]
        public Book Book { get; set; }
    }
}
