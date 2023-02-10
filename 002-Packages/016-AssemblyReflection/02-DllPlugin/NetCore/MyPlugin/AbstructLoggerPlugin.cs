namespace MyPlugin
{
    public abstract class AbstructLoggerPlugin : ILoggerPlugin
    {
        public string Level { get; set; }

        public AbstructLoggerPlugin(string level)
        {
            Level = level;
        }

        public abstract object Execute<T>(T param);
    }
}
