namespace RefitterApiClientSample.HeaderHandlers
{
    public class GenericApiHeaderContext
    {
        /// <summary>
        /// Authorization
        /// </summary>
        public string Authorization { get; set; } = string.Empty;

        /// <summary>
        /// Authorization Scheme
        /// </summary>
        public string AuthorizationScheme { get; set; } = "Bearer";

        /// <summary>
        /// Other Properties
        /// </summary>
        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
    }
}
