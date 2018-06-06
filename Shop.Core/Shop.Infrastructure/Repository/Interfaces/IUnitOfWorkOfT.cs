using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repository.Interfaces
{
    /// <summary>
    /// Обобщенный интерфейс для единиц работы с базой данных.
    /// </summary>
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        /// <summary>
        /// Возвразает <typeparamref name="TContext"/>.
        /// </summary>
        /// <returns>Экземпляр <typeparamref name="TContext"/>.</returns>
        TContext DbContext { get; }

        /// <summary>
        /// Сохраняет все изменения, сделанные в этом контексте, в базе данных.
        /// </summary>
        /// <param name="ensureAutoHistory"><c>True</c> если сохранять все изменения в истории изменений. По умолчанию <c>False</c></param>
        /// <param name="unitOfWorks">Одна или коллекция единиц работ с базой данных <see cref="IUnitOfWork"/>.</param>
        /// <returns>
        ///     <see cref="Task{TResult}"/> предоставляющую асинхронную запись изменений в базу данных.
        ///     Результатом задачи будет число изменённых записей в базе данных.
        /// </returns>
        Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IUnitOfWork[] unitOfWorks);
    }
}