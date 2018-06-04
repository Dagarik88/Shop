namespace Shop.Parsers.SamsonOpt.Model.Configuration
{
    /// <summary>
    /// Параметры доступа к API клинету Самсона.
    /// </summary>
    public class SamsonGate
    {
        /// <summary>
        /// Адрес к клиенту.
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// Ключь к API клиенту.
        /// </summary>
        public string ApiKey { get; set; }
    }
}