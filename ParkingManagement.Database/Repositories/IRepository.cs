namespace ParkingManagement.Database.Repositories
{
    public interface IRepository<T> : IDisposable
    {
        Task<T> FindAsync(string id);

        Task<IEnumerable<T>> GetAllAsync(); // TODO: Pagination to improve performance

        Task<int> CountAsync();

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task RemoveRangeAsync(IEnumerable<string> ids);

        Task ClearAsync();

        Task UpdateRangeAsync(IEnumerable<T> entities);
    }
}
