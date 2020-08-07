using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SovcombankTest.Models;

namespace SovcombankTest.DB
{
    public class MessageDbContext: DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Invitation> Invitation { get; set; }

        public MessageDbContext(DbContextOptions<MessageDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres");
        }
    }
}
