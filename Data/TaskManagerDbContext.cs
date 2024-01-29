using Microsoft.EntityFrameworkCore;
using CustomTask = TaskManagerAPI.Models.Domain.Task;

namespace TaskManagerAPI.Data
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<CustomTask> Tasks { get; set; }
    }
}
