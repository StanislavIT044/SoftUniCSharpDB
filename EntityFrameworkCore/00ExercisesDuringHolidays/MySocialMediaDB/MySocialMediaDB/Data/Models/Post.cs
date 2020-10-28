namespace MySocialMediaDB.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public Post()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Likes = 0;
        }

        public string Id { get; set; }

        public string Text { get; set; }

        public string PhotoId { get; set; }
        public Photo Photo { get; set; }

        [Required]
        public int Likes { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public string AuthorId { get; set; }
        public User Author { get; set; }

        //TODO: - Comments ???????????? 
    }
}

//-Id(Required)
//-User(Required)
//-Text
//-Photo
//-Likes int(Required)
//-CreatedOn DateTime(Required)
//-ICollection < Comment > Comments  