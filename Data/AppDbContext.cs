using DotnetMinApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetMinApi.Data

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // ctor - generates a contructor
        {
              
        }

        public DbSet<Command> Commands=> Set<Command>();
    }
}