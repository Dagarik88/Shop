using Newtonsoft.Json;
using System.Collections.Generic;

namespace Shop.Parsers.SamsonOpt.Model.ApiClientModels.Data.Assortment
{
    /// <summary>
    /// Информация о товаре.
    /// </summary>
    public class AssortmentModel
    {
        /// <summary>
        /// Код.
        /// </summary>
        public int Sku { get; set; }

        /// <summary>
        /// Наименование продукта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Наименование производителя.
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Артикул.
        /// </summary>
        [JsonProperty(PropertyName = "vendor_code")]
        public string VendorCode { get; set; }

        /// <summary>
        /// Штрих-код.
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        /// Бренд.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Расширенное описание.
        /// </summary>
        [JsonProperty(PropertyName = "description_ext")]
        public string DescriptionExt { get; set; }

        /// <summary>
        /// Вес.
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Объём.
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// Дата распродажи.
        /// </summary>
        [JsonProperty(PropertyName = "sale_date")]
        public string SaleDate { get; set; }

        /// <summary>
        /// Список категорий.
        /// </summary>
        [JsonProperty(PropertyName = "category_list")]
        public int[] CategoryList { get; set; }

        /// <summary>
        /// Список характеристик.
        /// </summary>
        [JsonProperty(PropertyName = "characteristic_list")]
        public string[] CharacteristicList { get; set; }

        /// <summary>
        /// Список фасетов.
        /// </summary>
        [JsonProperty(PropertyName = "facet_list")]
        public IEnumerable<FacetModel> FacetList { get; set; }

        /// <summary>
        /// Список фотографий.
        /// </summary>
        [JsonProperty(PropertyName = "photo_list")]
        public string[] PhotoList { get; set; }

        /// <summary>
        /// Размерность упаковки.
        /// </summary>
        [JsonProperty(PropertyName = "package_list")]
        public IEnumerable<PackageModel> PackageList { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        [JsonProperty(PropertyName = "price_list")]
        public IEnumerable<PriceModel> PriceList { get; set; }

        /// <summary>
        /// Склад.
        /// </summary>
        [JsonProperty(PropertyName = "stock_list")]
        public IEnumerable<StockModel> StockList { get; set; }

        /// <summary>
        /// Атрибуты.
        /// </summary>
        [JsonProperty(PropertyName = "attribute_list")]
        public IEnumerable<AattributeModel> AttributeList { get; set; }
    }
}