using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Library.DAL
{
    public class LibraryDbContext :DbContext
    {
        public  LibraryDbContext(DbContextOptions Options) : base(Options) 
        {
        }
        public DbSet<Author> Authors{ get; set; }
        public DbSet<Member> Members  { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Barrowed> Barroweds { get; set; }

        public DbSet<BookWriter> BookWriters { get; set; }

        public DbSet<Subject> Subjects { get; set; }

    }
}
