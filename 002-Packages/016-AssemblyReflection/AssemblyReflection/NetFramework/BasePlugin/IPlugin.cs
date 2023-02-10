namespace BasePlugin
{
    // Plugin Interface 接口
    public interface IPlugin
    {
        void Execute<T>(T param);
    }
}
