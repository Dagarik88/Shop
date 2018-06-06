using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Shop.Infrastructure.Repository.Interfaces;
using Shop.Infrastructure.Repository.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repository
{
    /// <summary>
    /// Реализация обобщенного репозитория.
    /// </summary>
    /// <typeparam name="TEntity">Тип объекта.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Поля

        /// <summary>
        /// Контекст работы с БД.
        /// </summary>
        protected readonly DbContext _dbContext;

        protected readonly DbSet<TEntity> _dbSet;

        #endregion Поля

        #region Конструкторы

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="dbContext">Контекст работы с БД.</param>
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<TEntity>();
        }

        #endregion Конструкторы

        #region Методы поиска объектов

        /// <summary>
        /// Возвращает <see cref="IQueryable{TEntity}"/> на основе условия выборки, сортировки. Этот метод по умолчанию не отслеживает запрос.
        /// </summary>
        /// <param name="predicate">Функция условний для выборки.</param>
        /// <param name="orderBy">Функция сортировки.</param>
        /// <param name="include">Функция подключения навигаций дочерних/родительских элементов.</param>
        /// <param name="disableTracking"><c>True</c> для отключения отслеживания запроса; в противном случае, <c>false</c>. По умолчанию: <c>true</c>.</param>
        /// <returns>Коллекция <see cref="IQueryable{TEntity}"/> которая содержит элементы удовлетворающие условию выборки <paramref name="predicate"/>, сортировки <paramref name="orderBy"/> и навигаций <paramref name="include"/>.</returns>
        /// <remarks>Этот метод по умолчанию не отслеживает запрос.</remarks>
        public IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        /// <summary>
        /// Возвращает <see cref="IQueryable{TEntity}"/> на основе условия выборки, сортировки. Этот метод по умолчанию не отслеживает запрос.
        /// </summary>
        /// <param name="predicate">Функция условний для выборки.</param>
        /// <param name="orderBy">Функция сортировки.</param>
        /// <param name="include">Функция подключения навигаций дочерних/родительских элементов.</param>
        /// <param name="disableTracking"><c>True</c> для отключения отслеживания запроса; в противном случае, <c>false</c>. По умолчанию: <c>true</c>.</param>
        /// <returns>Коллекция <see cref="IQueryable{TEntity}"/> которая содержит элементы удовлетворающие условию выборки <paramref name="predicate"/>, сортировки <paramref name="orderBy"/> и навигаций <paramref name="include"/>.</returns>
        /// <remarks>Этот метод по умолчанию не отслеживает запрос.</remarks>
        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        /// <summary>
        /// Возвращает <see cref="IPagedList{TEntity}"/> на основе условия выборки, сортировки. Этот метод по умолчанию не отслеживает запрос.
        /// </summary>
        /// <param name="predicate">Функция условний для выборки.</param>
        /// <param name="orderBy">Функция сортировки.</param>
        /// <param name="include">Функция подключения навигаций дочерних/родительских элементов.</param>
        /// <param name="pageIndex">Номер выбронной страницы.</param>
        /// <param name="pageSize">Количество объектов на странице.</param>
        /// <param name="disableTracking"><c>True</c> для отключения отслеживания запроса; в противном случае, <c>false</c>. По умолчанию: <c>true</c>.</param>
        /// <returns>
        ///     Коллекция <see cref="IPagedList{TEntity}"/> которая содержит элементы удовлетворающие
        ///     условию выборки <paramref name="predicate"/>, сортировки <paramref name="orderBy"/> и навигаций <paramref name="include"/>.
        /// </returns>
        /// <remarks>Этот метод по умолчанию не отслеживает запрос.</remarks>
        public IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                int pageIndex = 0,
                                                int pageSize = 20,
                                                bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToPagedList(pageIndex, pageSize);
            }
            else
            {
                return query.ToPagedList(pageIndex, pageSize);
            }
        }

        /// <summary>
        /// Асинхронный метод возвращающий <see cref="IPagedList{TEntity}"/> на основе условия выборки, сортировки. Этот метод по умолчанию не отслеживает запрос.
        /// </summary>
        /// <param name="predicate">Функция условний для выборки.</param>
        /// <param name="orderBy">Функция сортировки.</param>
        /// <param name="include">Функция подключения навигаций дочерних/родительских элементов.</param>
        /// <param name="pageIndex">Номер выбронной страницы.</param>
        /// <param name="pageSize">Количество объектов на странице.</param>
        /// <param name="disableTracking"><c>True</c> для отключения отслеживания запроса; в противном случае, <c>false</c>. По умолчанию: <c>true</c>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken" /> маркер отмены.</param>
        /// <returns>
        ///     Коллекция <see cref="IPagedList{TEntity}"/> которая содержит элементы удовлетворающие
        ///     условию выборки <paramref name="predicate"/>, сортировки <paramref name="orderBy"/> и навигаций <paramref name="include"/>.
        /// </returns>
        /// <remarks>Этот метод по умолчанию не отслеживает запрос.</remarks>
        public Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                           int pageIndex = 0,
                                                           int pageSize = 20,
                                                           bool disableTracking = true,
                                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
            }
            else
            {
                return query.ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
            }
        }

        /// <summary>
        /// Возвращает <see cref="IPagedList{TEntity}"/> на основе условия выборки, сортировки. Этот метод по умолчанию не отслеживает запрос.
        /// </summary>
        /// <param name="selector">Проекция зависимости.</param>
        /// <param name="predicate">Функция условний для выборки.</param>
        /// <param name="orderBy">Функция сортировки.</param>
        /// <param name="include">Функция подключения навигаций дочерних/родительских элементов.</param>
        /// <param name="pageIndex">Номер выбронной страницы.</param>
        /// <param name="pageSize">Количество объектов на странице.</param>
        /// <param name="disableTracking"><c>True</c> для отключения отслеживания запроса; в противном случае, <c>false</c>. По умолчанию: <c>true</c>.</param>
        /// <returns>
        ///     Коллекция <see cref="IPagedList{TEntity}"/> которая содержит элементы удовлетворающие
        ///     условию выборки <paramref name="predicate"/>, сортировки <paramref name="orderBy"/> и навигаций <paramref name="include"/>.
        /// </returns>
        /// <remarks>Этот метод по умолчанию не отслеживает запрос.</remarks>
        public IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                         Expression<Func<TEntity, bool>> predicate = null,
                                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                         int pageIndex = 0,
                                                         int pageSize = 20,
                                                         bool disableTracking = true)
            where TResult : class
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).Select(selector).ToPagedList(pageIndex, pageSize);
            }
            else
            {
                return query.Select(selector).ToPagedList(pageIndex, pageSize);
            }
        }

        /// <summary>
        /// Асинхронный метод возвращающий <see cref="IPagedList{TEntity}"/> на основе условия выборки, сортировки. Этот метод по умолчанию не отслеживает запрос.
        /// </summary>
        /// <param name="selector">Проекция зависимости.</param>
        /// <param name="predicate">Функция условний для выборки.</param>
        /// <param name="orderBy">Функция сортировки.</param>
        /// <param name="include">Функция подключения навигаций дочерних/родительских элементов.</param>
        /// <param name="pageIndex">Номер выбронной страницы.</param>
        /// <param name="pageSize">Количество объектов на странице.</param>
        /// <param name="disableTracking"><c>True</c> для отключения отслеживания запроса; в противном случае, <c>false</c>. По умолчанию: <c>true</c>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken" /> маркер отмены.</param>
        /// <returns>
        ///     Коллекция <see cref="IPagedList{TEntity}"/> которая содержит элементы удовлетворающие
        ///     условию выборки <paramref name="predicate"/>, сортировки <paramref name="orderBy"/> и навигаций <paramref name="include"/>.
        /// </returns>
        /// <remarks>Этот метод по умолчанию не отслеживает запрос.</remarks>
        public Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                                    Expression<Func<TEntity, bool>> predicate = null,
                                                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                                    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                                    int pageIndex = 0,
                                                                    int pageSize = 20,
                                                                    bool disableTracking = true,
                                                                    CancellationToken cancellationToken = default(CancellationToken))
            where TResult : class
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).Select(selector).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
            }
            else
            {
                return query.Select(selector).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
            }
        }

        /// <summary>
        /// Возвращает первый или по умолчанию объект на основе условия выборки, сортировки и навигаций. Этот метод по умолчанию не отслеживает запрос.
        /// </summary>
        /// <param name="predicate">Функция условний для выборки.</param>
        /// <param name="orderBy">Функция сортировки.</param>
        /// <param name="include">Функция подключения навигаций дочерних/родительских элементов.</param>
        /// <param name="disableTracking"><c>True</c> для отключения отслеживания запроса; в противном случае, <c>false</c>. По умолчанию: <c>true</c>.</param>
        /// <returns>
        ///     Возвращает первый или по умолчанию объект на основе условия выборки <paramref name="predicate"/>,
        ///     сортировки <paramref name="orderBy"/> и навигаций <paramref name="include"/>.
        /// </returns>
        /// <remarks>Этот метод по умолчанию не отслеживает запрос.</remarks>
        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                         bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).FirstOrDefault();
            }
            else
            {
                return query.FirstOrDefault();
            }
        }

        /// <summary>
        /// Возвращает первый или по умолчанию объект на основе условия выборки, сортировки и навигаций. Этот метод по умолчанию не отслеживает запрос.
        /// </summary>
        /// <param name="selector">Проекция зависимости.</param>
        /// <param name="predicate">Функция условний для выборки.</param>
        /// <param name="orderBy">Функция сортировки.</param>
        /// <param name="include">Функция подключения навигаций дочерних/родительских элементов.</param>
        /// <param name="disableTracking"><c>True</c> для отключения отслеживания запроса; в противном случае, <c>false</c>. По умолчанию: <c>true</c>.</param>
        /// <returns>
        ///     Возвращает первый или по умолчанию объект на основе условия выборки <paramref name="predicate"/>,
        ///     сортировки <paramref name="orderBy"/> и навигаций <paramref name="include"/>.
        /// </returns>
        /// <remarks>Этот метод по умолчанию не отслеживает запрос.</remarks>
        public TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                  Expression<Func<TEntity, bool>> predicate = null,
                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                  bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).Select(selector).FirstOrDefault();
            }
            else
            {
                return query.Select(selector).FirstOrDefault();
            }
        }

        /// <summary>
        /// Асинхронно возвращает первый или по умолчанию объект на основе условия выборки, сортировки и навигаций. Этот метод по умолчанию не отслеживает запрос.
        /// </summary>
        /// <param name="predicate">Функция условний для выборки.</param>
        /// <param name="orderBy">Функция сортировки.</param>
        /// <param name="include">Функция подключения навигаций дочерних/родительских элементов.</param>
        /// <param name="disableTracking"><c>True</c> для отключения отслеживания запроса; в противном случае, <c>false</c>. По умолчанию: <c>true</c>.</param>
        /// <returns>
        ///     Возвращает первый или по умолчанию объект на основе условия выборки <paramref name="predicate"/>,
        ///     сортировки <paramref name="orderBy"/> и навигаций <paramref name="include"/>.
        /// </returns>
        /// <remarks>Этот метод по умолчанию не отслеживает запрос.</remarks>
        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            else
            {
                return await query.FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// Асинхронно возвращает первый или по умолчанию объект на основе условия выборки, сортировки и навигаций. Этот метод по умолчанию не отслеживает запрос.
        /// </summary>
        /// <param name="selector">Проекция зависимости.</param>
        /// <param name="predicate">Функция условний для выборки.</param>
        /// <param name="orderBy">Функция сортировки.</param>
        /// <param name="include">Функция подключения навигаций дочерних/родительских элементов.</param>
        /// <param name="disableTracking"><c>True</c> для отключения отслеживания запроса; в противном случае, <c>false</c>. По умолчанию: <c>true</c>.</param>
        /// <returns>
        ///     Возвращает первый или по умолчанию объект на основе условия выборки <paramref name="predicate"/>,
        ///     сортировки <paramref name="orderBy"/> и навигаций <paramref name="include"/>.
        /// </returns>
        /// <remarks>Этот метод по умолчанию не отслеживает запрос.</remarks>
        public async Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                  Expression<Func<TEntity, bool>> predicate = null,
                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                  bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).FirstOrDefaultAsync();
            }
            else
            {
                return await query.Select(selector).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// Использует необработанные SQL-запросы для выборки <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="sql">Необработанный SQL-запрос.</param>
        /// <param name="parameters">Параметры.</param>
        /// <returns><see cref="IQueryable{TEntity}" /> удовлетворяющие необработанному SQL-запросу <paramref name="sql"/>.</returns>
        public IQueryable<TEntity> FromSql(string sql, params object[] parameters) => _dbSet.FromSql(sql, parameters);

        /// <summary>
        /// Ищет объект по значению первичного ключа. Если объект не найден вернёт null.
        /// </summary>
        /// <param name="keyValues">Значения первичного ключа.</param>
        /// <returns>Объект или null.</returns>
        public TEntity Find(params object[] keyValues) => _dbSet.Find(keyValues);

        /// <summary>
        /// Асинхронно ищет объект по значению первичного ключа. Если объект не найден вернёт null.
        /// </summary>
        /// <param name="keyValues">Значения первичного ключа.</param>
        /// <returns><see cref="Task{TEntity}"/> по асинхронному поиску объекта. Результатом будет либо объект, либо null.</returns>
        public Task<TEntity> FindAsync(params object[] keyValues) => _dbSet.FindAsync(keyValues);

        /// <summary>
        /// Асинхронно ищет объект по значению первичного ключа. Если объект не найден вернёт null.
        /// </summary>
        /// <param name="keyValues">Значения первичного ключа.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> маркер отмены.</param>
        /// <returns><see cref="Task{TEntity}"/> по асинхронному поиску объекта. Результатом будет либо объект, либо null.</returns>
        public Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken) => _dbSet.FindAsync(keyValues, cancellationToken);

        /// <summary>
        /// Возвразает число объектов удовлетворяющие заднанному условию.
        /// </summary>
        /// <param name="predicate">Функция условия выборки.</param>
        /// <returns>Число.</returns>
        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return _dbSet.Count();
            }
            else
            {
                return _dbSet.Count(predicate);
            }
        }

        #endregion Методы поиска объектов

        #region Методы изменений объектов

        /// <summary>
        /// Добавляет объект.
        /// </summary>
        /// <param name="entity">Объект для добавления.</param>
        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Добавляет объекты.
        /// </summary>
        /// <param name="entities">Объекты для добавления.</param>
        public void Insert(params TEntity[] entities) => _dbSet.AddRange(entities);

        /// <summary>
        /// Добавляет объекты.
        /// </summary>
        /// <param name="entities">Объекты для добавления.</param>
        public void Insert(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);

        /// <summary>
        /// Добовляет асинхронно объект.
        /// </summary>
        /// <param name="entity">Объект для добавления.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> маркер отмены.</param>
        /// <returns><see cref="Task"/> предоставляющую асинхронную операцию добавления объекта.</returns>
        public Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.AddAsync(entity, cancellationToken);

        /// <summary>
        /// Добовляет асинхронно объекты.
        /// </summary>
        /// <param name="entities">Объекты для добавления.</param>
        /// <returns><see cref="Task"/> предоставляющую асинхронную операцию добавления объектов.</returns>
        public Task InsertAsync(params TEntity[] entities) => _dbSet.AddRangeAsync(entities);

        /// <summary>
        /// Добовляет асинхронно объекты.
        /// </summary>
        /// <param name="entities">Объекты для добавления.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> маркер отмены.</param>
        /// <returns><see cref="Task"/> предоставляющую асинхронную операцию добавления объектов.</returns>
        public Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.AddRangeAsync(entities, cancellationToken);

        /// <summary>
        /// Обновляет указанный объект.
        /// </summary>
        /// <param name="entity">Объект для обновления.</param>
        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Асинхронно обновляет указанный объект.
        /// </summary>
        /// <param name="entity">Объект для обновления.</param>
        public void UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Обновляет указанные объекты.
        /// </summary>
        /// <param name="entities">Объекты для обновления.</param>
        public void Update(params TEntity[] entities) => _dbSet.UpdateRange(entities);

        /// <summary>
        /// Обновляет указанные объекты.
        /// </summary>
        /// <param name="entities">Объекты для обновления.</param>
        public void Update(IEnumerable<TEntity> entities) => _dbSet.UpdateRange(entities);

        /// <summary>
        /// Удаляет объект по значению первичного ключа.
        /// </summary>
        /// <param name="id">Значение первичного ключа.</param>
        public void Delete(object id)
        {
            // using a stub entity to mark for deletion
            var typeInfo = typeof(TEntity).GetTypeInfo();
            var key = _dbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property = typeInfo.GetProperty(key?.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<TEntity>();
                property.SetValue(entity, id);
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                var entity = _dbSet.Find(id);
                if (entity != null)
                {
                    Delete(entity);
                }
            }
        }

        /// <summary>
        /// Удаляет указанный объект.
        /// </summary>
        /// <param name="entity">Объект для удаления.</param>
        public void Delete(TEntity entity) => _dbSet.Remove(entity);

        /// <summary>
        /// Удаляет указанные объекты.
        /// </summary>
        /// <param name="entities">Объекты для удаления.</param>
        public void Delete(params TEntity[] entities) => _dbSet.RemoveRange(entities);

        /// <summary>
        /// Удаляет указанные объекты.
        /// </summary>
        /// <param name="entities">Объекты для удаления.</param>
        public void Delete(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);

        #endregion Методы изменений объектов
    }
}