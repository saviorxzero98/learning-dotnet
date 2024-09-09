namespace GarnetSampleServer
{
    public class GarnetSetting
    {
        public const string SettingName = "GarnetSettings";

        /// <summary>
        /// Port to run server on.
        /// </summary>
        public int Port { get; set; } = 6379;

        /// <summary>
        /// IP address to bind server to.
        /// </summary>
        public string Address { get; set; } = "127.0.0.1";

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
