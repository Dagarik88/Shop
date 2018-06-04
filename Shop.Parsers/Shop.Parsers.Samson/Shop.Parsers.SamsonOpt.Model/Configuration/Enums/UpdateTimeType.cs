using System.ComponentModel;

namespace Shop.Parsers.SamsonOpt.Model.Configuration.Enums
{
    /// <summary>
    /// Тип периодичности проверки обновления.
    /// </summary>
    public enum UpdateTimeType
    {
        /// <summary>
        /// Позволяет отключать сервис обновления.
        /// </summary>
        [DisplayName("Не указано")]
        None = 0,

        /// <summary>
        /// Проверять обновление каждые N секунд.
        /// </summary>
        [DisplayName("В секундах")]
        Second = 1,

        /// <summary>
        /// Проверять обновление каждые N минут.
        /// </summary>
        [DisplayName("В минутаха")]
        Minute = 2,

        /// <summary>
        /// Проверять обновление каждые N часов.
        /// </summary>
        [DisplayName("В часах")]
        Hour = 3,

        /// <summary>
        /// Проверять обновление каждые N дней.
        /// </summary>
        [DisplayName("В днях")]
        Day = 4,
    }
}