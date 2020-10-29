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

		public DbSet<Country> Countries { get; set; }

		public DbSet<Town> Towns { get; set; }

		public DbSet<Reply> Replies { get; set; }

		public DbSet<Photo> Photo { get; set; }

		public DbSet<ProfilePicture> ProfilePictures { get; set; }

		public DbSet<CoverPhoto> CoverPhoto { get; set; }

		public DbSet<Post> Posts { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<Page> Pages { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			if (!options.IsConfigured)
			{
				options.UseSqlServer("Server=.;Database=MySocialMediaa;Trusted_Connection=True");
			}
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Page>(entity =>
			{
				entity.HasOne(p => p.User)
					.WithMany(u => u.Pages)
					.HasForeignKey(p => p.UserId);
			});

			modelBuilder.Entity<Comment>(entity =>
			{
				entity.HasOne(c => c.Post)
					.WithMany(p => p.Comments)
					.HasForeignKey(c => c.PostId)
					.OnDelete(DeleteBehavior.Restrict);
			});

			//base.OnModelCreating(modelBuilder);
		}
    }
}
