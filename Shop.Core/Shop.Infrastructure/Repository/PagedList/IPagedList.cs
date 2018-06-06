using System.Collections.Generic;

namespace Shop.Infrastructure.Repository.PagedList
{
    /// <summary>
    /// Интерфейс для постраничного представления коллекции.
    /// </summary>
    /// <typeparam name="T">Тип объекта для постраничного разбиения.</typeparam>
    public interface IPagedList<T>
    {
        /// <summary>
        /// Возвращает номер начальной страницы.
        /// </summary>
        /// <value>Номер начальной страницы.</value>
        int IndexFrom { get; }

        /// <summary>
        /// Возвращаен номер выбранной страницы.
        /// </summary>
        /// <value>Номер выбранной страницы.</value>
        int PageIndex { get; }

        /// <summary>
        /// Возвращает количиство объектов на странице.
        /// </summary>
        /// <value>Количиство объектов на странице.</value>
        int PageSize { get; }

        /// <summary>
        /// Возвращает общее количество объектов типа <typeparamref name="T"/>.
        /// </summary>
        /// <value>Общее количество объектов типа <typeparamref name="T"/>.</value>
        int TotalCount { get; }

        /// <summary>
        /// Возвращает общее количество страниц.
        /// </summary>
        /// <value>Общее количество страниц.</value>
        int TotalPages { get; }

        /// <summary>
        /// Возвращает ограниченную коллекцию объектов <typeparamref name="T"/>.
        /// </summary>
        /// <value>Коллекция объектов <typeparamref name="T"/>.</value>
        IList<T> Items { get; }

        /// <summary>
        /// Возвращает фалаг существования предыдущей страницы.
        /// </summary>
        /// <value>Существование предыдущей страницы.</value>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Возвращает фалаг существования следующей страницы.
        /// </summary>
        /// <value>Существование следующей страницы.</value>
        bool HasNextPage { get; }
    }
}