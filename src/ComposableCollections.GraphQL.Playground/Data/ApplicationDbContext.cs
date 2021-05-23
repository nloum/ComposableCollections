using Microsoft.EntityFrameworkCore;

namespace ComposableCollections.GraphQL.Playground.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public DbSet<Person> People { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
    }
}