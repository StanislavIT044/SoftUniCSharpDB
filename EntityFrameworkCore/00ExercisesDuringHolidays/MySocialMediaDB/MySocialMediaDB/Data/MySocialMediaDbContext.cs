namespace MySocialMediaDB.Data
{
    using Microsoft.EntityFrameworkCore;
    using MySocialMediaDB.Data.Models;

    public class MySocialMediaDbContext : DbContext
    {
		public MySocialMediaDbContext()
		{
		}

		public MySocialMediaDbContext(DbContextOptions options)
			: base(options)
		{
		}

        public DbSet<User> Users { get; set; }

		public DbSet<Photo> Photos { get; set; }

		public DbSet<ProfilePicture> ProfilePictures { get; set; }

		public DbSet<CoverPhoto> CoverPhoto { get; set; }

		//public DbSet<Post> Posts { get; set; }

		//public DbSet<Comment> Comments { get; set; }

		//public DbSet<Page> Pages { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			if (!options.IsConfigured)
			{
				options.UseSqlServer("Server=.;Database=MySocialMediaa;Trusted_Connection=True");
			}
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			//TODO: Write relations between photo and user
			//Също така не забравяй да изключиш каскадното триене


			base.OnModelCreating(modelBuilder);
		}
    }
}
