namespace MySocialMediaDB.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Comment
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Likes = 0;
        }

        public string Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int Likes { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public string AuthorId { get; set; }
        public User Author { get; set; }

        //public virtual ICollection<Reply> Replies { get; set; }
    }
}
//3.COMMENT:             
//-Id
//- Text(Required)
//- Likes(Required)
//- CreatedOn DateTime(Required)
//- User(Required)
// --ICollection<Comment> Replies
