namespace MySocialMediaDB.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Reply
    {
        public Reply()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string AuthorId { get; set; }
        public User Author { get; set; }
    }
}
