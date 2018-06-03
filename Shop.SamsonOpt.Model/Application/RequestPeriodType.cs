using System.ComponentModel;

namespace Shop.SamsonOpt.Model.Application
{
    /// <summary>
    /// Тип периодичности.
    /// </summary>
    public enum RequestPeriodType
    {
        /// <summary>
        /// В секундах.
        /// </summary>
        [DisplayName("В секундах")]
        Second = 1,

        /// <summary>
        /// В митнутах.
        /// </summary>
        [DisplayName("В минутах")]
        Minute = 2,

        /// <summary>
        /// В часах.
        /// </summary>
        [DisplayName("В часах")]
        Hour = 3,

        /// <summary>
        /// В днях.
        /// </summary>
        [DisplayName("В днях")]
        Day = 4
    }
}
