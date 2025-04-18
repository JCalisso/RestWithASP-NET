using Microsoft.EntityFrameworkCore;
using RestWithASPNet.Models;

namespace RestWithASPNet.Models.Context
{
    public class SQLContext : DbContext
    {
        public SQLContext(){}

        public SQLContext(DbContextOptions<SQLContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
