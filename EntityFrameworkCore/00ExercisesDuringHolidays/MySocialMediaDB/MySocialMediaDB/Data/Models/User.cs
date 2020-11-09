namespace MySocialMediaDB.Data.Models
{
    using System;
    using MySocialMediaDB.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Photos = new HashSet<Photo>();
            this.Posts = new HashSet<Post>();
            this.Pages = new HashSet<Page>();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public int Test { get; set; }

        public string ProfilPictureId { get; set; }
        public ProfilePicture ProfilePicture { get; set; }

        public string CoverPhotoId { get; set; }
        public CoverPhoto CoverPhoto { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public DateTime CretedOn { get; set; }

        public string CountryId { get; set; }
        public Country Country { get; set; }

        public string TownId { get; set; }
        public Town Town { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Page> Pages { get; set; }
    }
}
