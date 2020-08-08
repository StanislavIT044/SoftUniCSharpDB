namespace VaporStore.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enums;

    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public PurchaseType Type { get; set; }

        //TODO : VALIDATION
        public string ProductKey { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [ForeignKey("Card")]
        public int CardId { get; set; }
        public Card Card { get; set; }

        [Required]
        [ForeignKey("Game")]
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
