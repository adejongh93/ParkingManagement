using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManagement.Repositories
{
    internal interface IRepository<T> : IDisposable
    {
        Task<T> GetAsync(string id);

        Task<IEnumerable<T>> GetAllAsync(); // TODO: Pagination to improve performance

        Task<int> GetCountAsync(); // TODO: Pagination to improve performance

        Task AddAsync(T entity);

        Task<bool> ExistsAsync(string id);

        Task RemoveAsync(T entity);
    }
}
