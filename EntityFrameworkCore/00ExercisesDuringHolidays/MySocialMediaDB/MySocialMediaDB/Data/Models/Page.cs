namespace MySocialMediaDB.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Page
    {
        public Page()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Followers = new HashSet<User>();
            this.Posts = new HashSet<Post>();
            this.Photos = new HashSet<Photo>();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Title { get; set; }

        [ForeignKey("CoverPhoto")]
        public string CoverPhotoId { get; set; }
        public CoverPhoto CoverPhoto { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<User> Followers { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
