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

		public DbSet<Post> Posts { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<Page> Pages { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			if (!options.IsConfigured)
			{
				options.UseSqlServer("Server=.;Database=MySocialMedia;Trusted_Connection=True");
			}
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
