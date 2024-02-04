using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models.Domain;
using TaskManagerAPI.Repositories.Interface;

namespace TaskManagerAPI.Repositories.SQLServerImplementation
{
    public class SQLServerListRepository : IListRepository
    {
        private readonly TaskManagerDbContext dbContext;

        public SQLServerListRepository(TaskManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List> CreateAsync(List list)
        {
            await dbContext.Lists.AddAsync(list);
            await dbContext.SaveChangesAsync();
            return list;
        }

        public async Task<List?> DeleteAsync(Guid id)
        {
            var existingList = await dbContext.Lists.FirstOrDefaultAsync(x => x.Id == id);

            if(existingList == null)
            {
                return null;
            }

            dbContext.Lists.Remove(existingList);
            _ = dbContext.SaveChangesAsync();
            return existingList;
        }

        public async Task<List<List>> GetAllAsync()
        {
            var listsDomain = await dbContext.Lists.ToListAsync();
            return listsDomain;
        }

        public async Task<List?> UpdateAsync(Guid id, List list)
        {
            var existingList = await dbContext.Lists.FirstOrDefaultAsync(x => x.Id == id);

            if(existingList == null)
            {
                return null;
            }

            existingList.Name = list.Name;
            _ = dbContext.SaveChangesAsync();
            return existingList;
        }
    }
}
