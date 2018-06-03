namespace Shop.SamsonOpt.Model.Application
{
    /// <summary>
    /// Настройка для сервиса обновления.
    /// </summary>
    public class SamsonGate
    {
        /// <summary>
        /// URL к API клиенты.
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// Ключ к API клиенту.
        /// </summary>
        public string ApiKey { get; set; }
    }
}
