namespace Shop.Parsers.SamsonOpt.Model.ApiClientModels.Data.Assortment
{
    /// <summary>
    /// Размерность упаковки.
    /// </summary>
    public class PackageModel
    {
        /// <summary>
        /// Тип упаковки.
        /// </summary>
        /// <value>
        /// min_opt - минимальнач партия ОПТ
        /// min_kor - минимальная партия КОР
        /// pzk - партия под заказ
        /// intermediate - промежуточная упаковка
        /// transport - транспортная упковка
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Количество.
        /// </summary>
        public int Value { get; set; }
    }
}