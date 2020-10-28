namespace MySocialMediaDB.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Page
    {
        public Page()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Title { get; set; }

        public Photo Photo { get; set; }

        public string OwnerId { get; set; }
        public User Owner { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<User> Followers { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
//- Id(Required)
//- Title
//- Photo
//- UserId owner(Required)
//- CreatedOn DateTime(Required)
//-
//-- ICollection < User > Followers
//-- ICollection<Post> Posts
