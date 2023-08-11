using CommonBack.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Backend.db
{
    public class UserChatContextcs: DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
       

        public UserChatContextcs()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // Database.EnsureDeleted();

            try
            {
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(name: "user");
            modelBuilder.Entity<Chat>().ToTable(name: "chat");
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=userchat;Username=postgres;Password=postgres");
        }
    }
}
