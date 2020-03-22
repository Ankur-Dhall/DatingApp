using Datingapp.API.Models;
using Datingapp.API.Properties.Models;
using Microsoft.EntityFrameworkCore;

namespace Datingapp.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DataContext(DbContextOptions<DataContext> options): base(options){}
    }
}