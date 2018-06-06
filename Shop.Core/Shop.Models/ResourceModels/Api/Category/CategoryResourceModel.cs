namespace Shop.Models.ResourceModels.Api.Category
{
    /// <summary>
    /// Ресурс модель категории каталога.
    /// </summary>
    public class CategoryResourceModel
    {
        /// <summary>
        /// ID значение родительской категории.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Наименование категории.
        /// </summary>
        public string Name { get; set; }
    }
}