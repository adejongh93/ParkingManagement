using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManagement.Repositories
{
    internal interface IRepository<T> : IDisposable
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync(); // TODO: Pagination to improve performance

        Task<int> GetCountAsync(); // TODO: Pagination to improve performance

        Task<bool> TryAddAsync(T entity);
    }
}
