using System.Linq.Expressions;

namespace Kakhnouski_153503.Domain.Abstractions;

public interface IRepository<T> where T : Entity
{
    /// <summary>
    /// Поиск сущности по Id
    /// </summary>
    /// <param name="id">Id сущности</param>
    /// <param name="cancellationToken"></param>
    /// <param name="includesProperties">Делегаты для подключения навигационных свойств</param>
    /// <returns></returns>
     Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default
     /*params Expression<Func<T, object>>[]? includesProperties*/);

    /// <summary>
    /// Получение всего списка сущностей
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получение отфильтрованного списка
    /// </summary>
    /// <param name="filter">Делегат-условие отбора</param>
    /// <param name="cancellationToken"></param>
    /// <param name="includesProperties">Делегаты для подключения навигационных свойств</param>
     Task<IReadOnlyList<T>> ListAsync(Func<T, bool> filter,
     CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавление новой сущности
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Изменение сущности
    /// </summary>
    /// <param name="entity">Сущность с измененным содержимым</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
 /// Удаление сущности
 /// </summary>
 /// <param name="entity">Сущность, которую следует удалить</param>
 /// <param name="cancellationToken"></param>
 /// <returns></returns>
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Поиск первой сущности, удовлетворяющей условию отбора.
    /// Если сущность не найдена, будет возвращено значение по умолчанию
    /// </summary>
    /// <param name="filter">Делегат-условие отбора</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<T> FirstOrDefaultAsync(Func<T, bool>? filter = default, CancellationToken cancellationToken = default);
}
