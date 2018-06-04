using System.Collections.Generic;

namespace Shop.Parsers.SamsonOpt.Model.ApiClientModels
{
    /// <summary>
    /// Дополнительная модель информации.
    /// </summary>
    public class MetaModel
    {
        /// <summary>
        /// Информация о постраничном выводе.
        /// </summary>
        public IEnumerable<PaginationModel> Pagination { get; set; }
    }
}