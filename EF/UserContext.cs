using Microsoft.EntityFrameworkCore;

namespace repos.EF
{
    public class UserContext:DbContext  
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Users");
        }
    }
}
