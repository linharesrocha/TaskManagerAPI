using TaskManagerAPI.Models.Domain;
using CustomTask = TaskManagerAPI.Models.Domain.Task;

namespace TaskManagerAPI.Repositories.Interface
{
    public interface ITaskRepository
    {
        Task<List<CustomTask>> GetAllAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true);
        Task<CustomTask?> GetByIdAsync(Guid id);
        Task<CustomTask> CreateAsync(CustomTask task);
        Task<CustomTask?> UpdateAsync(Guid id, CustomTask task);
        Task<CustomTask?> DeleteAsync(Guid id);
    }
}
