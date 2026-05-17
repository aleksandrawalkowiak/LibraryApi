using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Copy> Copies => Set<Copy>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Author -> Books
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId);

        // Book -> Copies
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Copies)
            .WithOne(c => c.Book)
            .HasForeignKey(c => c.BookId);
    }
}