using System.Collections.Generic;
using System.Reflection.Emit;
using DataAccess;
using Microsoft.EntityFrameworkCore;


namespace UserDbSevice
{
    public class UserDbSeviceContext : DbContext
    {

        public DbSet<UserData> Users { get; set; }

        public UserDbSeviceContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            //Database.EnsureDeleted();
            Database.EnsureCreated();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>().ToTable(name: "users");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=chat_users;Username=postgres;Password=admin");
        }

    }
}