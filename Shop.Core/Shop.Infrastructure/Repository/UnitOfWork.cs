using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Shop.Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repository
{
    /// <summary>
    /// Обобщенная единица работы с базой данных.
    /// </summary>
    /// <typeparam name="TContext">Тип контекста работы с БД.</typeparam>
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>, IUnitOfWork where TContext : DbContext
    {
        #region Поля

        /// <summary>
        /// Контекст работы с БД.
        /// </summary>
        private readonly TContext _context;

        /// <summary>
        /// Флаг высвобождения неуправлаемых ресурсов.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Коллекция репозиториев.
        /// </summary>
        private Dictionary<Type, object> repositories;

        #endregion Поля

        #region Конструкторы

        /// <summary>
        /// Контсруктор по умолчанию.
        /// </summary>
        /// <param name="context">Контекст работы с БД.</param>
        public UnitOfWork(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion Конструкторы

        #region Свойства

        /// <summary>
        /// Контекст работы с БД.
        /// </summary>
        /// <returns>The instance of type <typeparamref name="TContext"/>.</returns>
        public TContext DbContext => _context;

        #endregion Свойства

        #region Методы

        /// <summary>
        /// Изменяет имя базы данных.Для этого требуются базы данных на одном компьютере.Примечание: это только работает для MySQL прямо сейчас.
        /// </summary>
        /// <param name="database">Имя базы данных.</param>
        /// <remarks>
        /// Это используется только для поддержки нескольких баз данных в одной модели. Для этого требуются базы данных на одном компьютере.
        /// </remarks>
        public void ChangeDatabase(string database)
        {
            var connection = _context.Database.GetDbConnection();
            if (connection.State.HasFlag(ConnectionState.Open))
            {
                connection.ChangeDatabase(database);
            }
            else
            {
                var connectionString = Regex.Replace(connection.ConnectionString.Replace(" ", ""), @"(?<=[Dd]atabase=)\w+(?=;)", database, RegexOptions.Singleline);
                connection.ConnectionString = connectionString;
            }

            // Следующий код работает только для mysql
            var items = _context.Model.GetEntityTypes();
            foreach (var item in items)
            {
                if (item.Relational() is RelationalEntityTypeAnnotations extensions)
                {
                    extensions.Schema = database;
                }
            }
        }

        /// <summary>
        /// Возвращает репозиторий для типа объекта <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">Тип объекта.</typeparam>
        /// <returns>Экземпляр реализующий <see cref="IRepository{TEntity}"/> интерфейс.</returns>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!repositories.ContainsKey(type))
            {
                repositories[type] = new Repository<TEntity>(_context);
            }

            return (IRepository<TEntity>)repositories[type];
        }

        /// <summary>
        /// Сохраняет в базе данных все изменения, внесенные в этом контексте.
        /// </summary>
        /// <param name="ensureAutoHistory"><c>True</c> если сохранять все изменения в истории изменений. По умолчанию <c>False</c></param>
        /// <returns>Количество изменённых записей в базе данных.</returns>
        public int SaveChanges(bool ensureAutoHistory = false)
        {
            if (ensureAutoHistory)
            {
                _context.EnsureAutoHistory();
            }

            return _context.SaveChanges();
        }

        /// <summary>
        /// Асинхронно сохраняет все изменения в базу данных, внесенные в эту единицу работы.
        /// </summary>
        /// <param name="ensureAutoHistory"><c>True</c> если сохранять все изменения в истории изменений. По умолчанию <c>False</c></param>
        /// <returns>
        ///     <see cref="Task{TResult}"/> предоставляющую асинхронную запись изменений в базу данных.
        ///     Результатом задачи будет число изменённых записей в базе данных.
        /// </returns>
        public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false)
        {
            if (ensureAutoHistory)
            {
                _context.EnsureAutoHistory();
            }

            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Saves all changes made in this context to the database with distributed transaction.
        /// </summary>
        /// <param name="ensureAutoHistory"><c>True</c> if save changes ensure auto record the change history.</param>
        /// <param name="unitOfWorks">An optional <see cref="IUnitOfWork"/> array.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous save operation. The task result contains the number of state entities written to database.</returns>
        public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IUnitOfWork[] unitOfWorks)
        {
            // TransactionScope will be included in .NET Core v2.0
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var count = 0;
                    foreach (var unitOfWork in unitOfWorks)
                    {
                        var uow = unitOfWork as UnitOfWork<DbContext>;
                        uow.DbContext.Database.UseTransaction(transaction.GetDbTransaction());
                        count += await uow.SaveChangesAsync(ensureAutoHistory);
                    }

                    count += await SaveChangesAsync(ensureAutoHistory);

                    transaction.Commit();

                    return count;
                }
                catch (Exception)
                {
                    transaction.Rollback();

                    throw;
                }
            }
        }

        /// <summary>
        /// Выполняет необработанный SQL-запрос.
        /// </summary>
        /// <param name="sql">Необработанный SQL-запрос.</param>
        /// <param name="parameters">Параметры.</param>
        /// <returns>Количества записей в базу данных.</returns>
        public int ExecuteSqlCommand(string sql, params object[] parameters) => _context.Database.ExecuteSqlCommand(sql, parameters);

        /// <summary>
        /// Получает объект на основе типа <typeparamref name="TEntity"/> при помощи необработанного SQL-запроса.
        /// </summary>
        /// <typeparam name="TEntity">Тип объекта.</typeparam>
        /// <param name="sql">Необработанный SQL-запрос.</param>
        /// <param name="parameters">Параметры.</param>
        /// <returns>Коллекцию <see cref="IQueryable{T}"/> удовлетворяющую услвоию необработанного SQL-запроса.</returns>
        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class => _context.Set<TEntity>().FromSql(sql, parameters);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Выполняет определенные приложением задачи, связанные с освобождением, освобождением или сбросом неуправляемых ресурсов
        /// </summary>
        /// <param name="disposing">Флаг высвобождения.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // ичистить репозитории
                    if (repositories != null)
                    {
                        repositories.Clear();
                    }

                    // освободить контекст работы с БД.
                    _context.Dispose();
                }
            }

            disposed = true;
        }

        /// <summary>
        /// Использует TrakGrap Api для подключения/отключения объектов
        /// </summary>
        /// <param name="rootEntity">Головной объект</param>
        /// <param name="callback">Делегат для преобразования свойств объекта в свойства сущности.</param>
        public void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback)
        {
            _context.ChangeTracker.TrackGraph(rootEntity, callback);
        }

        #endregion Методы
    }
}