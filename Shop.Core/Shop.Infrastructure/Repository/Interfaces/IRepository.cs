using Microsoft.EntityFrameworkCore.Query;
using Shop.Infrastructure.Repository.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repository.Interfaces
{
    /// <summary>
    /// Интерфес для универсального репозитория.
    /// </summary>
    /// <typeparam name="TEntity">Тип объекта.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
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
        IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                bool disableTracking = true);

        /// <summary>
        /// Возвращает <see cref="IEnumerable{TEntity}"/> на основе условия выборки, сортировки. Этот метод по умолчанию не отслеживает запрос.
        /// </summary>
        /// <param name="predicate">Функция условний для выборки.</param>
        /// <param name="orderBy">Функция сортировки.</param>
        /// <param name="include">Функция подключения навигаций дочерних/родительских элементов.</param>
        /// <param name="disableTracking"><c>True</c> для отключения отслеживания запроса; в противном случае, <c>false</c>. По умолчанию: <c>true</c>.</param>
        /// <returns>Коллекция <see cref="IQueryable{TEntity}"/> которая содержит элементы удовлетворающие условию выборки <paramref name="predicate"/>, сортировки <paramref name="orderBy"/> и навигаций <paramref name="include"/>.</returns>
        /// <remarks>Этот метод по умолчанию не отслеживает запрос.</remarks>
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                bool disableTracking = true);

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
        IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                         int pageIndex = 0,
                                         int pageSize = 20,
                                         bool disableTracking = true);

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
        Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                    int pageIndex = 0,
                                                    int pageSize = 20,
                                                    bool disableTracking = true,
                                                    CancellationToken cancellationToken = default(CancellationToken));

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
        IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                  Expression<Func<TEntity, bool>> predicate = null,
                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                  int pageIndex = 0,
                                                  int pageSize = 20,
                                                  bool disableTracking = true) where TResult : class;

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
        Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                             Expression<Func<TEntity, bool>> predicate = null,
                                                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                             Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                             int pageIndex = 0,
                                                             int pageSize = 20,
                                                             bool disableTracking = true,
                                                             CancellationToken cancellationToken = default(CancellationToken)) where TResult : class;

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
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                  bool disableTracking = true);

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
        TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                                           Expression<Func<TEntity, bool>> predicate = null,
                                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                           bool disableTracking = true);

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
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);

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
        Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);

        /// <summary>
        /// Использует необработанные SQL-запросы для выборки <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="sql">Необработанный SQL-запрос.</param>
        /// <param name="parameters">Параметры.</param>
        /// <returns><see cref="IQueryable{TEntity}" /> удовлетворяющие необработанному SQL-запросу <paramref name="sql"/>.</returns>
        IQueryable<TEntity> FromSql(string sql, params object[] parameters);

        /// <summary>
        /// Ищет объект по значению первичного ключа. Если объект не найден вернёт null.
        /// </summary>
        /// <param name="keyValues">Значения первичного ключа.</param>
        /// <returns>Объект или null.</returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// Асинхронно ищет объект по значению первичного ключа. Если объект не найден вернёт null.
        /// </summary>
        /// <param name="keyValues">Значения первичного ключа.</param>
        /// <returns><see cref="Task{TEntity}"/> по асинхронному поиску объекта. Результатом будет либо объект, либо null.</returns>
        Task<TEntity> FindAsync(params object[] keyValues);

        /// <summary>
        /// Асинхронно ищет объект по значению первичного ключа. Если объект не найден вернёт null.
        /// </summary>
        /// <param name="keyValues">Значения первичного ключа.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> маркер отмены.</param>
        /// <returns><see cref="Task{TEntity}"/> по асинхронному поиску объекта. Результатом будет либо объект, либо null.</returns>
        Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);

        /// <summary>
        /// Возвразает число объектов удовлетворяющие заднанному условию.
        /// </summary>
        /// <param name="predicate">Функция условия выборки.</param>
        /// <returns>Число.</returns>
        int Count(Expression<Func<TEntity, bool>> predicate = null);

        #endregion Методы поиска объектов

        #region Методы изменений объектов

        /// <summary>
        /// Добавляет объект.
        /// </summary>
        /// <param name="entity">Объект для добавления.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Добавляет объекты.
        /// </summary>
        /// <param name="entities">Объекты для добавления.</param>
        void Insert(params TEntity[] entities);

        /// <summary>
        /// Добавляет объекты.
        /// </summary>
        /// <param name="entities">Объекты для добавления.</param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Добовляет асинхронно объект.
        /// </summary>
        /// <param name="entity">Объект для добавления.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> маркер отмены.</param>
        /// <returns><see cref="Task"/> предоставляющую асинхронную операцию добавления объекта.</returns>
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Добовляет асинхронно объекты.
        /// </summary>
        /// <param name="entities">Объекты для добавления.</param>
        /// <returns><see cref="Task"/> предоставляющую асинхронную операцию добавления объектов.</returns>
        Task InsertAsync(params TEntity[] entities);

        /// <summary>
        /// Добовляет асинхронно объекты.
        /// </summary>
        /// <param name="entities">Объекты для добавления.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> маркер отмены.</param>
        /// <returns><see cref="Task"/> предоставляющую асинхронную операцию добавления объектов.</returns>
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Обновляет указанный объект.
        /// </summary>
        /// <param name="entity">Объект для обновления.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Обновляет указанные объекты.
        /// </summary>
        /// <param name="entities">Объекты для обновления.</param>
        void Update(params TEntity[] entities);

        /// <summary>
        /// Обновляет указанные объекты.
        /// </summary>
        /// <param name="entities">Объекты для обновления.</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Удаляет объект по значению первичного ключа.
        /// </summary>
        /// <param name="id">Значение первичного ключа.</param>
        void Delete(object id);

        /// <summary>
        /// Удаляет указанный объект.
        /// </summary>
        /// <param name="entity">Объект для удаления.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Удаляет указанные объекты.
        /// </summary>
        /// <param name="entities">Объекты для удаления.</param>
        void Delete(params TEntity[] entities);

        /// <summary>
        /// Удаляет указанные объекты.
        /// </summary>
        /// <param name="entities">Объекты для удаления.</param>
        void Delete(IEnumerable<TEntity> entities);

        #endregion Методы изменений объектов
    }
}