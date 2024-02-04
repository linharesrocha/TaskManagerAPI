using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models.Domain;
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
            task.CreatedAt = DateTime.Now;
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

        public async Task<List<Models.Domain.Task>> GetAllAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 50)
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
                else if(filterOn.Equals("Priority", StringComparison.OrdinalIgnoreCase))
                {
                    if(Enum.TryParse(filterQuery, true, out PriorityLevel priorityFilter)) {
                        tasksDomain = tasksDomain.Where(x => x.Priority == priorityFilter);
                    }
                }
            }

            // Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    tasksDomain = isAscending ? tasksDomain.OrderBy(x => x.Name) : tasksDomain.OrderByDescending(x => x.Name);
                }
                else if(sortBy.Equals("Description", StringComparison.OrdinalIgnoreCase)) {
                    tasksDomain = isAscending ? tasksDomain.OrderBy(x => x.Description) : tasksDomain.OrderByDescending(x => x.Description);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await tasksDomain.Include("List").Skip(skipResults).Take(pageSize).ToListAsync();
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

            existingTask.UpdatedAt = DateTime.Now;
            existingTask.Name = task.Name;
            existingTask.Description = task.Description;
            
            _ = dbContext.SaveChangesAsync();
            return existingTask;
        }
    }
}
