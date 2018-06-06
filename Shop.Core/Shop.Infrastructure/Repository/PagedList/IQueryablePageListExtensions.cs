using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repository.PagedList
{
    /// <summary>
    /// Расширение для преобразование коллекции IQueryable в IPagedList
    /// </summary>
    public static class IQueryablePageListExtensions
    {
        /// <summary>
        ///     Преобразует коллекцию IQueryable в <see cref="IPagedList{T}"/>
        ///     согласно значению выбранного страницы <paramref name="pageIndex"/>
        ///     и количеству объектов <paramref name="pageSize"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="source">Источник.</param>
        /// <param name="pageIndex">Номер выбранной страницы.</param>
        /// <param name="pageSize">Количество на странице.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken" /> маркер отмены.</param>
        /// <param name="indexFrom">The start index value.</param>
        /// <returns>Экземпляр <see cref="IPagedList{T}"/>.</returns>
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, int indexFrom = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (indexFrom > pageIndex)
            {
                throw new ArgumentException($"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");
            }

            var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            var items = await source.Skip((pageIndex - indexFrom) * pageSize)
                                    .Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

            var pagedList = new PagedList<T>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                IndexFrom = indexFrom,
                TotalCount = count,
                Items = items,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            return pagedList;
        }
    }
}
