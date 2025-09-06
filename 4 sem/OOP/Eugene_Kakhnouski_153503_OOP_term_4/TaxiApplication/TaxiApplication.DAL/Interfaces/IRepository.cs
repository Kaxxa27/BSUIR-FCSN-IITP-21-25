namespace TaxiApplication.DAL.Interfaces;

public interface IRepository<T> where T : Entity
{
	Task<T?> GetByIdAsync(int id);

	// IQueryble -> для того, чтобы можно 
	Task<IEnumerable<T>> ListAllAsync(CancellationToken cancellationToken = default);

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
	Task<T?> FirstOrDefaultAsync(Func<T, bool> filter, CancellationToken cancellationToken = default);
}
