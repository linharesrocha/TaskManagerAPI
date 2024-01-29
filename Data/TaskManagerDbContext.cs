using Microsoft.EntityFrameworkCore;
using Task = TaskManagerAPI.Models.Domain.Task;

namespace TaskManagerAPI.Data
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<Task> Tasks { get; set; }
    }
}
