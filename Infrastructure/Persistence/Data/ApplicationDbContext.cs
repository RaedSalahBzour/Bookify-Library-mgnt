using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
        public DbSet<CategoryBook> CategoryBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoryBook>()
           .HasKey(cb => new { cb.BookId, cb.CategoryId });

            modelBuilder.Entity<UserBook>()
           .HasKey(cb => new { cb.UserId, cb.BookId });

            modelBuilder.Entity<Book>()
                .HasMany(b => b.UserBooks)
                .WithOne(ub => ub.Book)
                .HasForeignKey(ub => ub.BookId);

            modelBuilder.Entity<Book>()
           .HasKey(b => b.Id);

            modelBuilder.Entity<Book>()
           .Property(b => b.Id)
           .ValueGeneratedOnAdd();

            modelBuilder.Entity<Book>()
           .HasMany(b => b.CategoryBooks)
           .WithOne(cb => cb.Book)
           .HasForeignKey(cb => cb.BookId);

            modelBuilder.Entity<Category>()
           .HasMany(c => c.CategoryBooks)
           .WithOne(cb => cb.Category)
           .HasForeignKey(cb => cb.CategoryId);

            modelBuilder.Entity<Category>()
           .HasKey(c => c.Id);

            modelBuilder.Entity<Category>()
           .Property(c => c.Id)
           .ValueGeneratedOnAdd();

            modelBuilder.Entity<Review>()
           .HasOne(r => r.Book)
           .WithMany(b => b.Reviews)
           .HasForeignKey(r => r.BookId);

            modelBuilder.Entity<Review>()
           .HasKey(r => r.Id);

            modelBuilder.Entity<Review>()
           .Property(r => r.Id)
           .ValueGeneratedOnAdd();

            modelBuilder.Entity<Borrowing>()
           .HasOne(b => b.Book)
           .WithMany(b => b.Borrowings)
           .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<Borrowing>()
           .HasKey(b => b.Id);

            modelBuilder.Entity<Borrowing>()
           .Property(b => b.Id)
           .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
           .HasMany(u => u.UserBooks)
           .WithOne(ub => ub.User)
           .HasForeignKey(ub => ub.UserId);

            modelBuilder.Entity<User>()
           .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
           .Property(u => u.Id)
           .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
           .HasMany(u => u.Reviews)
           .WithOne(r => r.User)
           .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<User>()
           .HasMany(u => u.Borrowings)
           .WithOne(r => r.User)
           .HasForeignKey(r => r.UserId);




        }


    }
}
