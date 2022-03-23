using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class TaskAppContext : DbContext
    {
        public TaskAppContext(DbContextOptions<TaskAppContext> options) : base(options)
        {

        }

        public virtual DbSet<TaskList> TaskLists { get; set; } = null!;
        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
            });

            modelBuilder.Entity<TaskList>(entity =>
            {
                entity.ToTable("TaskList");
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("Activity");
                entity.HasOne(x => x.TaskList)
                .WithMany(x => x.Activities)
                .HasForeignKey(x => x.TaskListId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Todo_Activity");
            });
        }
    }
}
