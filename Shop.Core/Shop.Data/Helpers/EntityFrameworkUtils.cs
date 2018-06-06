using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Shop.Data.Helpers
{
    /// <summary>
    /// Вспомогательные методы для работы с EF.
    /// </summary>
    public static class EntityFrameworkUtils
    {
        /// <summary>
        /// Добавление Map названии таблиц и столбцов (Ковертация любого имени из PascalCase нотации в under_score нотацию).
        /// </summary>
        /// <param name="modelBuilder">Сопоставление классов со схемой базы данных.</param>
        public static void AddPostgresNameMap(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Таблицы
                entity.Relational().TableName = entity.Relational().TableName.ToUndescoreCase();

                // Поля (колонки)
                foreach (var property in entity.GetProperties())
                {
                    property.Relational().ColumnName = property.Name.ToUndescoreCase();
                }

                // перв. ключи
                foreach (var key in entity.GetKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToUndescoreCase();
                }

                // вн. ключи
                foreach (var key in entity.GetForeignKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToUndescoreCase();
                }

                // индексы
                foreach (var index in entity.GetIndexes())
                {
                    index.Relational().Name = index.Relational().Name.ToUndescoreCase();
                }
            }
        }
    }

    public static class StringExtensions
    {
        /// <summary>
        /// Ковертация любого имени из PascalCase нотации в under_score нотацию (удобную для работы с postgres).
        /// </summary>
        /// <param name="entityName">Любой текст использующий PascalCase.</param>
        public static string ToUndescoreCase(this string entityName)
        {
            var replaceRegex = new Regex(@"((?<UpperSymbol>)[A-Z])", RegexOptions.Singleline);
            var result = replaceRegex.Replace(entityName, @"_$1").TrimStart('_').ToLower();
            return result;
        }
    }
}