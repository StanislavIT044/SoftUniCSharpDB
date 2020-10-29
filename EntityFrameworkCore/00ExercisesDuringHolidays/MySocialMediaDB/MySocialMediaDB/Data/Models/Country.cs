namespace MySocialMediaDB.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        public Country()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<User>();
        }

        public string Id { get; set; }

        [Required]
        public string CountyName { get; set; }

        [Required]
        public string CountryCode { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
