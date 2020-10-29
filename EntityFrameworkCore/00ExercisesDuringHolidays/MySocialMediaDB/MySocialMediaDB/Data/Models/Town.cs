namespace MySocialMediaDB.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Town
    {
        public Town()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<User>();
        }

        public string Id { get; set; }

        public string TownName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
