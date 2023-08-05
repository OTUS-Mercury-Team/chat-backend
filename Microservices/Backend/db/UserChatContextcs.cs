using CommonBack.Models;
using Microsoft.EntityFrameworkCore;

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
            Database.EnsureCreated();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(name: "user");
            modelBuilder.Entity<Chat>().ToTable(name: "chat");
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=userchat;Username=postgres;Password=admin");
        }
    }
}
