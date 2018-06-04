using System.Collections.Generic;

namespace Shop.Parsers.SamsonOpt.Model.ApiClientModels
{
    /// <summary>
    /// Ответная модель от API сервиса Самосон
    /// </summary>
    /// <typeparam name="T">Модель сущности.</typeparam>
    /// <remarks>
    /// Документация к API сервису Самсона:
    /// https://api.samsonopt.ru/v1/doc/index.html
    /// </remarks>
    public class ResponseModel<T> where T : class
    {
        /// <summary>
        /// Основная информация.
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Доп. информация.
        /// </summary>
        public MetaModel Meta { get; set; }
    }
}