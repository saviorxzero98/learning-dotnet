using Topshelf;

namespace CS_Topshelf
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(serviceConfig =>
            {
                serviceConfig.UseNLog();

                serviceConfig.Service<BackendService>(serviceInstance =>
                {
                    // 註冊 WinService初始化方法至 Topshelf 
                    serviceInstance.ConstructUsing(() => new BackendService());

                    // 註冊如何啟動
                    serviceInstance.WhenStarted(execute => execute.OnStart());

                    // 註冊如何停止
                    serviceInstance.WhenStopped(execute => execute.OnStop());
                });

                // 設定Service名稱
                serviceConfig.SetServiceName("TestService");

                // 設定 service 顯示名稱
                serviceConfig.SetDisplayName("Test Service");

                // 設定 service 顯示描述
                serviceConfig.SetDescription("A Topshelf Test Service");

                // 以「local system」執行，開機自動啟動
                serviceConfig.RunAsLocalSystem().StartAutomatically();
            });
        }
    }
}
