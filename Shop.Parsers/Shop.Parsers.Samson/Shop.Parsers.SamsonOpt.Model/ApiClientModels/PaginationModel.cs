namespace Shop.Parsers.SamsonOpt.Model.ApiClientModels
{
    /// <summary>
    /// Информация о постраничном выводе.
    /// </summary>
    public class PaginationModel
    {
        /// <summary>
        /// Ссылка на предыдущую страницу.
        /// </summary>
        public string Previous { get; set; }

        /// <summary>
        /// Ссылка на следующую страницу.
        /// </summary>
        public string Next { get; set; }
    }
}