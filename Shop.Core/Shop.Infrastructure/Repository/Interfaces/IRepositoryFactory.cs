namespace Shop.Infrastructure.Repository.Interfaces
{
    /// <summary>
    /// Интерфейс для фабрики реализующие <see cref="IRepository{TEntity}"/> интерфейс.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Возвращает репозиторий на основе типа запрашиваемых объектов<typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">Тип объекта.</typeparam>
        /// <returns>Экземпляр реализующий <see cref="IRepository{TEntity}"/> интерфейс.</returns>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}