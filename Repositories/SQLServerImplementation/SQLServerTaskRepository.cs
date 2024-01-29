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

        public async Task<List<Models.Domain.Task>> GetAllAsync()
        {
            return await dbContext.Tasks.ToListAsync();
        }

        public async Task<Models.Domain.Task?> GetByIdAsync(Guid id)
        {
            return await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Models.Domain.Task?> UpdateAsync(Guid id, Models.Domain.Task task)
        {
            throw new NotImplementedException();
        }
    }
}
