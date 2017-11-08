namespace AuctionSystem.Data
{
    using System.Data.Entity;
    using Models;

    public partial class AuctionContext : DbContext
    {
        public AuctionContext()
            : base("name=AuctionContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<User>()
                                .HasRequired(c => c.Zip);

            builder.Entity<User>()
                                .HasMany(c => c.Payments)
                                .WithRequired(p => p.User)
                                .HasForeignKey(p => p.UserId);

            builder.Entity<Bid>()
                            .HasKey(b => b.Id);

            builder.Entity<Bid>()
                            .HasRequired(b => b.User)
                            .WithMany(a => a.Products)
                            .HasForeignKey(b => b.UserId);

            builder.Entity<Bid>()
                            .HasRequired(b => b.Product)
                            .WithMany(p => p.Users)
                            .HasForeignKey(b => b.ProductId);

            builder.Entity<Invoice>()
                                .HasKey(i => new { i.UserId, i.ProductId });

            builder.Entity<Invoice>()
                            .HasRequired(i => i.User)
                            .WithMany(c => c.Invoices)
                            .HasForeignKey(i => i.UserId);

            builder.Entity<Invoice>()
                            .HasRequired(i => i.Product);
        }

        public DbSet<Bid> Bids { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Zip> Zips { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Payment> Payments { get; set; }
    }
}
