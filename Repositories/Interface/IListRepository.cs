using CustomList = TaskManagerAPI.Models.Domain.List;

namespace TaskManagerAPI.Repositories.Interface
{
    public interface IListRepository
    {
        Task<List<CustomList>> GetAllAsync();
        Task<CustomList> CreateAsync(CustomList customList);
        Task<CustomList?> DeleteAsync(Guid id);
        Task<CustomList?> UpdateAsync(Guid id, CustomList custom);
    }
}
