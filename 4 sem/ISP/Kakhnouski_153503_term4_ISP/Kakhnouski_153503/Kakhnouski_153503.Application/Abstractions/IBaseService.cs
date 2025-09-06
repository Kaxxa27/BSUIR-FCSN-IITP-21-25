using Kakhnouski_153503.Domain.Entities;

namespace Kakhnouski_153503.Application.Abstractions;

public interface IBaseService<T> where T : Entity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T item);
    Task<T> UpdateAsync(T item);
    Task<T> DeleteAsync(int id);
}

