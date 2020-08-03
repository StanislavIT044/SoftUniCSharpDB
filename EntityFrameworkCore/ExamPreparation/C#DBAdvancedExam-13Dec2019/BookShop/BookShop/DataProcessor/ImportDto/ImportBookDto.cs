namespace BookShop.DataProcessor.ImportDto
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using BookShop.Data.Models.Enums;

    [XmlType("Book")]
    public class ImportBookDto
    {
        [Required]
        [XmlElement("Name")]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [XmlElement("Genre")]
        public int Genre { get; set; }

        [XmlElement("Price")]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [XmlElement("Pages")]
        [Range(50, 5000)]
        public int Pages { get; set; }

        [Required]
        [XmlElement("PublishedOn")]
        public string PublishedOn { get; set; }
    }
}
