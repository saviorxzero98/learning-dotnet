## Background Work Queue

透過非同步背景工作讓 Web API 處理耗時工作，以避免發生效能問題。

* **.NET Framework 4.5.2+**
    * 使用內建  HostingEnvironment.QueueBackgroundWorkItem 

* **.NET Core 2.0+**
    1. 使用內建的 [IHostedService](https://devblogs.microsoft.com/cesardelatorre/implementing-background-tasks-in-microservices-with-ihostedservice-and-the-backgroundservice-class-net-core-2-x/)
    2. 使用 [DalSoft.Hosting.BackgroundQueue](https://github.com/DalSoft/DalSoft.Hosting.BackgroundQueue) **<font color=red>(已停止維護)</font>**
    3. 使用 [Hangfire](https://github.com/HangfireIO/Hangfire)
    4. 使用 [TwentyFourMinutes.BackgroundQueue](https://github.com/TwentyFourMinutes/BackgroundQueue)







