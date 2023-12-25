using ParkingManagement.Database.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManagement.Repositories
{
    public interface IRepository<T> : IDisposable
    {
        Task<T> GetAsync(string id);

        Task<IEnumerable<T>> GetAllAsync(); // TODO: Pagination to improve performance

        Task<int> GetCountAsync(); // TODO: Pagination to improve performance

        Task AddAsync(T entity);

        Task<bool> ExistsAsync(string id);

        Task RemoveAsync(T entity);

        Task Clear();

        Task UpdateRangeAsync(IEnumerable<T> entities);
    }
}
