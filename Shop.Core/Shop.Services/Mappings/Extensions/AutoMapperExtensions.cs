using AutoMapper;
using System.Linq;

namespace Shop.Services.Mappings.Extensions
{
    /// <summary>
    /// Расширение для AutoMapper.
    /// </summary>
    public static class AutoMapperExtensions
    {
        /// <summary>
        /// Мержит <paramref name="item2"/> в <paramref name="item1"/>.
        /// </summary>
        /// <typeparam name="TResult">Тип конечной модели данных.</typeparam>
        /// <param name="item1">Объект требующий обновлений.</param>
        /// <param name="item2">Объект из которого возмуться данные.</param>
        public static TResult MergeInto<TResult>(this IMapper mapper, object item1, object item2)
        {
            return mapper.Map(item2, mapper.Map<TResult>(item1));
        }

        /// <summary>
        /// Собирает модель данных из переданных объектов.
        /// Перед сборкой убедись что есть все карты для существующих типов переменных учасвующих в сборке результирующей модели.
        /// </summary>
        /// <typeparam name="TResult">Тип конечной модели данных.</typeparam>
        /// <param name="objects">Объекты из которых будут браться данные.</param>
        public static TResult MergeInto<TResult>(this IMapper mapper, params object[] objects)
        {
            var res = mapper.Map<TResult>(objects.First());
            return objects.Skip(1).Aggregate(res, (r, obj) => mapper.Map(obj, r));
        }
    }
}