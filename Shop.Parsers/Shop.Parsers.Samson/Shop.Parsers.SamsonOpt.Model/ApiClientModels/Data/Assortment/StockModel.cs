namespace Shop.Parsers.SamsonOpt.Model.ApiClientModels.Data.Assortment
{
    /// <summary>
    /// Склад.
    /// </summary>
    public class StockModel
    {
        /// <summary>
        /// Тип склада.
        /// </summary>
        /// <value>
        /// idp - остаток в филиале
        /// transit - количество в пути
        /// distribution_warehouse - остатки на РЦ
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Количество.
        /// </summary>
        public int Value { get; set; }
    }
}