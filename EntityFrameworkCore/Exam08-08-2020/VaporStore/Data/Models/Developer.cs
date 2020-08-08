namespace VaporStore.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VaporStore.Data.Models;

    public class Developer
    {
        public Developer()
        {
            this.Games = new HashSet<Game>();
        }

        [Key]
        public int Id { get; set; }

        
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
