using Microsoft.EntityFrameworkCore;
using Shop.Data.Common;
using Shop.Data.Helpers;
using Shop.Data.ModelConfigurations;

namespace Shop.Data
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    public class ShopContext : DbContext
    {
        #region Свойства (Таблицы)

        /// <summary>
        /// Категория каталога.
        /// </summary>
        public DbSet<Category> Category { get; set; }

        #endregion Свойства (Таблицы)

        #region Конструктор

        /// <summary>
        /// Контекст для работы с базой данных сервиса "Dispatcher".
        /// </summary>
        public ShopContext(DbContextOptions options) : base(options) { }

        #endregion Конструктор

        #region Публичные методы

        /// <summary>
        /// Обработка создания модели.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Мапинг на постгрес имена
            EntityFrameworkUtils.AddPostgresNameMap(modelBuilder);

            //Применение конфигураций
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }

        #endregion Публичные методы
    }
}