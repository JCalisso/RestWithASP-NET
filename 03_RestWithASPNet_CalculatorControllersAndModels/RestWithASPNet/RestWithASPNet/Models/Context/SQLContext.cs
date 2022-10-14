using Microsoft.EntityFrameworkCore;
using RestWithASPNet.Model;

namespace RestWithASPNet.Models.Context
{
    public class SQLContext : DbContext
    {
        public SQLContext()
        {

        }

        public SQLContext(DbContextOptions<SQLContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
    }
}
