namespace MySocialMediaDB.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Likes = 0;
            this.Replies = new HashSet<Reply>();
        }

        public string Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int Likes { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string AuthorId { get; set; }
        public User Author { get; set; }

        [Required]
        public string PostId { get; set; }
        public Post Post { get; set; }  

        public virtual ICollection<Reply> Replies { get; set; }
    }
}
