namespace Shop.Parsers.SamsonOpt.Model.ApiClientModels.Data.Assortment
{
    /// <summary>
    /// Цена.
    /// </summary>
    public class PriceModel
    {
        /// <summary>
        /// Тип цены.
        /// </summary>
        /// <value>
        /// contract - договорная цена
        /// infiltration - Рекомендованная розничная цена
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Размер цены.
        /// </summary>
        public decimal Value { get; set; }
    }
}