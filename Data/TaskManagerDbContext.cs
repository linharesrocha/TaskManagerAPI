using Microsoft.EntityFrameworkCore;
using CustomTask = TaskManagerAPI.Models.Domain.Task;
using CustomList = TaskManagerAPI.Models.Domain.List;

namespace TaskManagerAPI.Data
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<CustomTask> Tasks { get; set; }
        public DbSet<CustomList> Lists { get; set; }
    }
}
