namespace MySocialMediaDB.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Surname { get; set; }

        public string ProfilPictureId { get; set; }
        public Photo? ProfilePicture { get; set; }

        //public Photo? CoverPhoto { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public DateTime CretedOn { get; set; }

        public string Country { get; set; }

        public string Town { get; set; }

        public virtual ICollection<User> Friends { get; set; }

        //TODO: --ICollection<Post> Posts
        //TODO: --ICollection < Page > Pages
    }
}

//USER:
//-Id(Required)-
//- Name(Required)-
//- Surname(Required)-
//- Gender
//- CreatedOn DateTime(Required)-
//- Birth date DateTime(Required)-
//-
//--ICollection < User > Friends-
//--ICollection<Post> Posts
//--ICollection < Page > Pages
