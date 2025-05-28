using Microsoft.EntityFrameworkCore;
using Midgar.Domain.Entities;

namespace Midgar.Persistence.Context
{
    public class MidgarContext : DbContext
    {
        public MidgarContext(DbContextOptions<MidgarContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
    }
}