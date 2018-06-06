using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Data.Common;

namespace Shop.Data.ModelConfigurations
{
    /// <summary>
    /// Конфигуратор для модели <see cref="Category"/>.
    /// </summary>
    public class CategoryConfiguration : BaseEntityTypeConfiguration<Category>
    {
        /// <summary>
        /// Настройка индексов БД для ускорения поиска.
        /// </summary>
        /// <param name="builder">Билдер используемый для настройки модели.</param>
        protected override void ConfigureIndexes(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.HasIndex(x => x.ParentId);
            builder.HasIndex(x => x.Name);
        }

        /// <summary>
        /// Настройка связей БД.
        /// </summary>
        /// <param name="builder">Билдер используемый для настройки модели.</param>
        protected override void ConfigureRelations(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(x => x.ChildCategories).WithOne(x => x.ParentCategory).HasForeignKey(x => x.ParentId);
        }

        /// <summary>
        /// Настройка ограничений и стандартных значений свойств БД.
        /// </summary>
        /// <param name="builder">Билдер используемый для настройки модели.</param>
        protected override void ConfigureProperties(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.IsActive).ValueGeneratedOnAdd();
        }
    }
}