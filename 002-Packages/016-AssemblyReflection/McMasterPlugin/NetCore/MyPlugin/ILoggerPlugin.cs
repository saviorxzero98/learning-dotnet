namespace MyPlugin
{
    public interface ILoggerPlugin
    {
        object Execute<T>(T param);
    }
}
