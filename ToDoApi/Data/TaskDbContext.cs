using Microsoft.EntityFrameworkCore;
using ToDoApi.Entities;

namespace ToDoApi.Data;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

    public DbSet<ToDoTaskEF> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ToDoTaskEfConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}