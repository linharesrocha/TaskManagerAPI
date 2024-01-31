using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.Repositories.Interface;

namespace TaskManagerAPI.Repositories.SQLServerImplementation
{
    public class SQLServerTaskRepository : ITaskRepository
    {
        private readonly TaskManagerDbContext dbContext;

        public SQLServerTaskRepository(TaskManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Models.Domain.Task> CreateAsync(Models.Domain.Task task)
        {
            await dbContext.AddAsync(task);
            await dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<Models.Domain.Task?> DeleteAsync(Guid id)
        {
            var existingTask = await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingTask == null)
            {
                return null;
            }

            dbContext.Tasks.Remove(existingTask);
            _ = dbContext.SaveChangesAsync();
            return existingTask;


        }

        public async Task<List<Models.Domain.Task>> GetAllAsync(string? filterOn = null, string? filterQuery = null)
        {
            var tasksDomain = dbContext.Tasks.AsQueryable();

            // Filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    tasksDomain = tasksDomain.Where(x => x.Name.Contains(filterQuery)); 
                }
                else if(filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    tasksDomain = tasksDomain.Where(x => x.Description.Contains(filterQuery));
                }
            }

            return await tasksDomain.ToListAsync();
        }

        public async Task<Models.Domain.Task?> GetByIdAsync(Guid id)
        {
            return await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Models.Domain.Task?> UpdateAsync(Guid id, Models.Domain.Task task)
        {
            var existingTask = await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingTask == null)
            {
                return null;
            }

            existingTask.Name = task.Name;
            existingTask.Description = task.Description;
            
            _ = dbContext.SaveChangesAsync();
            return existingTask;
        }
    }
}
