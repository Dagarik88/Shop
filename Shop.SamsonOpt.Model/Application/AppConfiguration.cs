namespace Shop.SamsonOpt.Model.Application
{
    /// <summary>
    /// Конфигурация приложения.
    /// </summary>
    public class AppConfiguration
    {
        /// <summary>
        /// Данные к доступу к API СамосонОпта.
        /// </summary>
        public SamsonGate SamsonGate { get; set; }

        /// <summary>
        /// Периодичность запроса данных.
        /// </summary>
        public int RequestPeriod { get; set; }

        /// <summary>
        /// Тип периодичности.
        /// </summary>
        public RequestPeriodType RequestPeriodType { get; set; }
    }
}
