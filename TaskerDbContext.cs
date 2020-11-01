using Microsoft.EntityFrameworkCore;
using Tasker.Models;
using TaskManager.Configuration;

namespace Tasker
{
    public class TaskerDbContext : DbContext
    {
        public TaskerDbContext(DbContextOptions<TaskerDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserOrder>().HasKey(t => new { t.UserId, t.OrderId });
            modelBuilder.Entity<UserExec>().HasKey(t => new { t.UserId, t.ExecuteId });
            





            modelBuilder.Entity<UserOrder>()
                .HasOne(u => u.User)
                .WithMany(uw => uw.UserOrders)
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserExec>()
               .HasOne(u => u.User)
               .WithMany(uw => uw.UserExecutes)
               .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.Restrict);




            modelBuilder.ApplyConfiguration(new WorkConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Work> Works { get; set; }
    }
}
