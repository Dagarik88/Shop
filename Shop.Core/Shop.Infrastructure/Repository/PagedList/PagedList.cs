using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Infrastructure.Repository.PagedList
{
    /// <summary>
    /// Модель для постраничных данных.
    /// </summary>
    /// <typeparam name="T">Тип объекта для постраничного разбиения.</typeparam>
    public class PagedList<T> : IPagedList<T>
    {
        /// <summary>
        /// Получает или задает номер выбранной страницы.
        /// </summary>
        /// <value>Номер выбранной страницы.</value>
        public int PageIndex { get; set; }

        /// <summary>
        /// Получает или задает количество объектов на странице.
        /// </summary>
        /// <value>Количество объектов на странице.</value>
        public int PageSize { get; set; }

        /// <summary>
        /// Получает или задает общее количество объектов типа <typeparamref name="T"/>.
        /// </summary>
        /// <value>Общее количество объектов типа <typeparamref name="T"/>.</value>
        public int TotalCount { get; set; }

        /// <summary>
        /// Получает или задает общее количество страниц.
        /// </summary>
        /// <value>Общее количество страниц.</value>
        public int TotalPages { get; set; }

        /// <summary>
        /// Получает или задает номер начальной страницы.
        /// </summary>
        /// <value>Номер начальной страницы.</value>
        public int IndexFrom { get; set; }

        /// <summary>
        /// Получает или задает список объектов типа <typeparamref name="T"/>.
        /// </summary>
        /// <value>Коллекция объектов типа <typeparamref name="T"/>.</value>
        public IList<T> Items { get; set; }

        /// <summary>
        /// Получает или задает возможность сущестования предыдущей страницы.
        /// </summary>
        /// <value>Флаг возможности сущестования предыдущей страницы.</value>
        public bool HasPreviousPage => PageIndex - IndexFrom > 0;

        /// <summary>
        /// Получает или задает возможность сущестования следующей страницы.
        /// </summary>
        /// <value>Флаг возможности сущестования следующей страницы.</value>
        public bool HasNextPage => PageIndex - IndexFrom + 1 < TotalPages;

        /// <summary>
        /// Конструктор создает готовый экземпляр с заданными параметрами:
        /// Номеру выбранной страницы <paramref name="pageIndex"/>.
        /// Количеству объектов на странице <paramref name="pageSize"/>.
        /// </summary>
        /// <param name="source">Тип объекта для постраничного разбиения.</param>
        /// <param name="pageIndex">Номер выбранной страницы.</param>
        /// <param name="pageSize">Количество объектов на странице.</param>
        /// <param name="indexFrom">Номер начальной страницы.</param>
        internal PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int indexFrom)
        {
            if (indexFrom > pageIndex)
            {
                throw new ArgumentException($"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");
            }

            if (source is IQueryable<T> querable)
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
                IndexFrom = indexFrom;
                TotalCount = querable.Count();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

                Items = querable.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToList();
            }
            else
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
                IndexFrom = indexFrom;
                TotalCount = source.Count();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

                Items = source.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToList();
            }
        }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        internal PagedList() => Items = new T[0];
    }

    /// <summary>
    /// Реализация постраничного представления тип <see cref="IPagedList{T}"/> и его конвертация.
    /// </summary>
    /// <typeparam name="TSource">Тип исходного объекта.</typeparam>
    /// <typeparam name="TResult">Тип ожидаемого объекта.</typeparam>
    internal class PagedList<TSource, TResult> : IPagedList<TResult>
    {
        /// <summary>
        /// Возвращаен номер выбранной страницы.
        /// </summary>
        /// <value>Номер выбранной страницы.</value>
        public int PageIndex { get; }

        /// <summary>
        /// Возвращает количиство объектов на странице типа <typeparamref name="TResult"/>.
        /// </summary>
        /// <value>Количиство объектов на странице.</value>
        public int PageSize { get; }

        /// <summary>
        /// Возвращает общее количество  объекта типа <typeparamref name="TResult"/>.
        /// </summary>
        /// <value>Общее количество объектов.</value>
        public int TotalCount { get; }

        /// <summary>
        /// Возвращает общее количество страниц.
        /// </summary>
        /// <value>Общее количество страниц.</value>
        public int TotalPages { get; }

        /// <summary>
        /// Возвращает номер начальной страницы.
        /// </summary>
        /// <value>Номер начальной страницы.</value>
        public int IndexFrom { get; }

        /// <summary>
        /// Возвращает ограниченную коллекцию объектов типа <typeparamref name="TResult"/>.
        /// </summary>
        /// <value>Объекты типа <typeparamref name="T"/>.</value>
        public IList<TResult> Items { get; }

        /// <summary>
        /// Возвращает фалаг существования предыдущей страницы.
        /// </summary>
        /// <value>Существование предыдущей страницы.</value>
        public bool HasPreviousPage => PageIndex - IndexFrom > 0;

        /// <summary>
        /// Возвращает фалаг существования следующей страницы.
        /// </summary>
        /// <value>Существование следующей страницы.</value>
        public bool HasNextPage => PageIndex - IndexFrom + 1 < TotalPages;

        /// <summary>
        /// Конструктор <see cref="PagedList{TSource, TResult}" />.
        /// </summary>
        /// <param name="source">Коллекция для постраничного разбиения.</param>
        /// <param name="converter">Функция конвертирующая из <typeparamref name="TSource"/> в <typeparamref name="TResult"/>.</param>
        /// <param name="pageIndex">Номер выбранной страницы.</param>
        /// <param name="pageSize">Количество объектов на странице.</param>
        /// <param name="indexFrom">Номер начальной страницы.</param>
        public PagedList(IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int pageIndex, int pageSize, int indexFrom)
        {
            if (indexFrom > pageIndex)
            {
                throw new ArgumentException($"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");
            }

            if (source is IQueryable<TSource> querable)
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
                IndexFrom = indexFrom;
                TotalCount = querable.Count();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

                var items = querable.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToArray();

                Items = new List<TResult>(converter(items));
            }
            else
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
                IndexFrom = indexFrom;
                TotalCount = source.Count();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

                var items = source.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToArray();

                Items = new List<TResult>(converter(items));
            }
        }

        /// <summary>
        /// Создает новый экземпляр <see cref="PagedList{TSource, TResult}" />.
        /// </summary>
        /// <param name="source">Коллекция для постраничного разбиения.</param>
        /// <param name="converter">Функция конвертирующая из <typeparamref name="TSource"/> в <typeparamref name="TResult"/>.</param>
        public PagedList(IPagedList<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
        {
            PageIndex = source.PageIndex;
            PageSize = source.PageSize;
            IndexFrom = source.IndexFrom;
            TotalCount = source.TotalCount;
            TotalPages = source.TotalPages;

            Items = new List<TResult>(converter(source.Items));
        }
    }

    /// <summary>
    /// Хэлперы для <see cref="IPagedList{T}"/>.
    /// </summary>
    public static class PagedList
    {
        /// <summary>
        /// Создание пустого <see cref="IPagedList{T}"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта для постраничного разбиения.</typeparam>
        /// <returns>Пустой экзепляр <see cref="IPagedList{T}"/>.</returns>
        public static IPagedList<T> Empty<T>() => new PagedList<T>();

        /// <summary>
        /// Создание нового экзепляра <see cref="IPagedList{TResult}"/> из <see cref="IPagedList{TSource}"/>.
        /// </summary>
        /// <typeparam name="TResult">Тип ожидаемого объекта.</typeparam>
        /// <typeparam name="TSource">Тип задаваемого объекта.</typeparam>
        /// <param name="source">Коллекция для постраничного разбиения.</param>
        /// <param name="converter">Функция конвертирующая из <typeparamref name="TSource"/> в <typeparamref name="TResult"/>.</param>
        /// <returns>Экземпляр <see cref="IPagedList{TResult}"/>.</returns>
        public static IPagedList<TResult> From<TResult, TSource>(IPagedList<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter) => new PagedList<TSource, TResult>(source, converter);
    }
}