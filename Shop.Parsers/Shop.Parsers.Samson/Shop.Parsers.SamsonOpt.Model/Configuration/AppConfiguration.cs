using Shop.Parsers.SamsonOpt.Model.Configuration.Enums;

namespace Shop.Parsers.SamsonOpt.Model.Configuration
{
    /// <summary>
    /// Конфигурация приложения.
    /// </summary>
    public class AppConfiguration
    {
        /// <summary>
        /// Параметры доступа к API клинету Самсона.
        /// </summary>
        public SamsonGate SamsonGate { get; set; }

        /// <summary>
        /// Периодичность проверки обновления.
        /// </summary>
        public int CheckUpdateTime { get; set; }

        /// <summary>
        /// Тип периодичности проверки обновления.
        /// </summary>
        public UpdateTimeType UpdateTimeType { get; set; }
    }
}