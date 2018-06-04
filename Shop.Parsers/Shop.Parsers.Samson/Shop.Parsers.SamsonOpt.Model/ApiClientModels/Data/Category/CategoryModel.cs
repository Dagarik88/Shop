using Newtonsoft.Json;

namespace Shop.Parsers.SamsonOpt.Model.ApiClientModels.Data.Category
{
    /// <summary>
    /// Инфорация о категории каталога.
    /// </summary>
    public class CategoryModel
    {
        /// <summary>
        /// ID значение категории.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID значение родительской категории.
        /// </summary>
        [JsonProperty(PropertyName = "parent_id")]
        public int ParentId { get; set; }

        /// <summary>
        /// Название категории.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Уровень вложения категории.
        /// </summary>
        [JsonProperty(PropertyName = "depth_level")]
        public int DepthLevel { get; set; }
    }
}