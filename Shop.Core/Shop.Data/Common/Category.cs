using System.Collections.Generic;

namespace Shop.Data.Common
{
    /// <summary>
    /// Категория каталога.
    /// </summary>
    public class Category
    {
        #region Поля

        /// <summary>
        /// Id значение категории.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id значение родительской категории.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Наименование категории.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Активность категории.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Порядковый номер.
        /// </summary>
        public int Order { get; set; }

        #endregion Поля

        #region Навигационные свойства

        /// <summary>
        /// Родительская категория.
        /// </summary>
        public virtual Category ParentCategory { get; set; }

        /// <summary>
        /// Подкатегории.
        /// </summary>
        public virtual IEnumerable<Category> ChildCategories { get; set; }

        #endregion  Навигационные свойства
    }
}