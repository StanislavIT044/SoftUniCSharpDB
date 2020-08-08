namespace VaporStore.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VaporStore.Data.Models.Enums;

    public class Card
    {
        public Card()
        {
            this.Purchases = new HashSet<Purchase>();
        }

        [Key]
        public int Id { get; set; }

        //TODO : VALIDATION
        public string Number { get; set; }

        //TODO : VALIDATION
        public string Cvc { get; set; }

        [Required]
        public CardType Type { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
    }
}
