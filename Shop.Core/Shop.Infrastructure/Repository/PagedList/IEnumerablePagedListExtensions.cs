using System;
using System.Collections.Generic;

namespace Shop.Infrastructure.Repository.PagedList
{
    /// <summary>
    /// Расширение <see cref="IEnumerable{T}"/>, обеспечивающее возможность разбиения коллекций по страницам.
    /// </summary>
    public static class IEnumerablePagedListExtensions
    {
        /// <summary>
        ///     Преобразует в постраничный список <see cref="IPagedList{T}"/>
        ///     согласно номеру выбранной страницы <paramref name="pageIndex"/>
        ///     и количества объектов на странице <paramref name="pageSize"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <param name="source">Коллекция для постраничного разбиения.</param>
        /// <param name="pageIndex">Номер страницы.</param>
        /// <param name="pageSize">Количество объектов на стринице.</param>
        /// <param name="indexFrom">Начальное значение.</param>
        /// <returns>Экземпляр <see cref="IPagedList{T}"/>.</returns>
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int indexFrom = 0) => new PagedList<T>(source, pageIndex, pageSize, indexFrom);

        /// <summary>
        ///     Преобразует в постраничный список <see cref="IPagedList{T}"/>
        ///     по указанному <paramref name="converter"/>,
        ///     согласно номеру выбранной страницы <paramref name="pageIndex"/>
        ///     и количества объектов на странице <paramref name="pageSize"/>.
        /// </summary>
        /// <typeparam name="TSource">Тип объекта.</typeparam>
        /// <typeparam name="TResult">Тип результирующего объекта.</typeparam>
        /// <param name="source">Коллекция для постраничного разбиения.</param>
        /// <param name="converter">Функция конвертирующая из <typeparamref name="TSource"/> в <typeparamref name="TResult"/>.</param>
        /// <param name="pageIndex">Номер выбранной страницы.</param>
        /// <param name="pageSize">Количество объектов на стринице.</param>
        /// <param name="indexFrom">Начальное значение.</param>
        /// <returns>Экземпляр <see cref="IPagedList{T}"/>.</returns>
        public static IPagedList<TResult> ToPagedList<TSource, TResult>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int pageIndex, int pageSize, int indexFrom = 0) => new PagedList<TSource, TResult>(source, converter, pageIndex, pageSize, indexFrom);
    }
}