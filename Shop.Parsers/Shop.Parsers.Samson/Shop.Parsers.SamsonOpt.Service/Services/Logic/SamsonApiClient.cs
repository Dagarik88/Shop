using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shop.Parsers.SamsonOpt.Model.ApiClientModels;
using Shop.Parsers.SamsonOpt.Model.ApiClientModels.Data.Assortment;
using Shop.Parsers.SamsonOpt.Model.ApiClientModels.Data.Category;
using Shop.Parsers.SamsonOpt.Model.Configuration;
using Shop.Parsers.SamsonOpt.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shop.Parsers.SamsonOpt.Service.Services.Logic
{
    /// <summary>
    /// HTTP клиент к API сервисам Самсона.
    /// </summary>
    /// <remarks>
    /// Документация к API сервису Самсона:
    /// https://api.samsonopt.ru/v1/doc/index.html
    /// </remarks>
    public class SamsonApiClient : ISamsonApiClient
    {
        #region Поля

        /// <summary>
        /// HTTP клиент.
        /// </summary>
        private readonly HttpClient _client = new HttpClient();

        /// <summary>
        /// Параметры доступа к API сервису Самсона.
        /// </summary>
        private readonly SamsonGate _configuration;

        #endregion Поля

        #region Конструктор

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        public SamsonApiClient(
            IOptions<AppConfiguration> configuration)
        {
            _configuration = configuration.Value.SamsonGate;

            _client.DefaultRequestHeaders.Add("User-Agent", "string");
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        #endregion Конструктор

        /// <summary>
        /// Возвращает список категорий.
        /// </summary>
        /// <returns>Список категорий.</returns>
        public async Task<IEnumerable<CategoryModel>> GetCategory()
        {
            var result = default(IEnumerable<CategoryModel>);

            try
            {
                var url = BuildBaseUrl("category");

                var response = await _client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<ResponseModel<CategoryModel>>(content).Data;
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        /// <summary>
        /// Возвращает список продуктов.
        /// </summary>
        /// <returns>Список продуктов.</returns>
        public async Task<IEnumerable<AssortmentModel>> GetAssortment()
        {
            var result = default(IEnumerable<AssortmentModel>);

            try
            {
                var url = BuildBaseUrl("assortment");

                var response = await _client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<ResponseModel<AssortmentModel>>(content).Data;
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        #region Приватные методы

        /// <summary>
        /// Возвращает путь с вбитыми ключами авторизации.
        /// </summary>
        /// <param name="route">Роут.</param>
        /// <returns>Url к методам.</returns>
        private string BuildBaseUrl(string route)
        {
            return $"{_configuration.ApiUrl}/{route}/?api_key={_configuration.ApiKey}";
        }

        #endregion Приватные методы
    }
}