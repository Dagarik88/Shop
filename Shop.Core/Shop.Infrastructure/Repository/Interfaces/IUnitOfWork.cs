using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repository.Interfaces
{
    /// <summary>
    /// Интерфейс для единицы работы с базой данных.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Возвращает репозиторий для типа объекта <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">Тип объекта.</typeparam>
        /// <returns>Экземпляр реализующий <see cref="IRepository{TEntity}"/> интерфейс.</returns>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// Сохраняет в базе данных все изменения, внесенные в этом контексте.
        /// </summary>
        /// <param name="ensureAutoHistory"><c>True</c> если сохранять все изменения в истории изменений. По умолчанию <c>False</c></param>
        /// <returns>Количество изменённых записей в базе данных.</returns>
        int SaveChanges(bool ensureAutoHistory = false);

        /// <summary>
        /// Асинхронно сохраняет все изменения в базу данных, внесенные в эту единицу работы.
        /// </summary>
        /// <param name="ensureAutoHistory"><c>True</c> если сохранять все изменения в истории изменений. По умолчанию <c>False</c></param>
        /// <returns>
        ///     <see cref="Task{TResult}"/> предоставляющую асинхронную запись изменений в базу данных.
        ///     Результатом задачи будет число изменённых записей в базе данных.
        /// </returns>
        Task<int> SaveChangesAsync(bool ensureAutoHistory = false);

        /// <summary>
        /// Выполняет необработанный SQL-запрос.
        /// </summary>
        /// <param name="sql">Необработанный SQL-запрос.</param>
        /// <param name="parameters">Параметры.</param>
        /// <returns>Количества записей в базу данных.</returns>
        int ExecuteSqlCommand(string sql, params object[] parameters);

        /// <summary>
        /// Получает объект на основе типа <typeparamref name="TEntity"/> при помощи необработанного SQL-запроса.
        /// </summary>
        /// <typeparam name="TEntity">Тип объекта.</typeparam>
        /// <param name="sql">Необработанный SQL-запрос.</param>
        /// <param name="parameters">Параметры.</param>
        /// <returns>Коллекцию <see cref="IQueryable{T}"/> удовлетворяющую услвоию необработанного SQL-запроса.</returns>
        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class;

        /// <summary>
        /// Использует TraсkGrapр Api для подключения/отключения объектов
        /// </summary>
        /// <param name="rootEntity">Головной объект</param>
        /// <param name="callback">Делегат для преобразования свойств объекта в свойства сущности.</param>
        void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback);
    }
}