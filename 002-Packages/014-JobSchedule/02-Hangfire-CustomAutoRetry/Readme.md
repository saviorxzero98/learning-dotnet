# 如何建立一個自訂 Hangfire Job Filter



## Step.1 建立 Custom Job Filter Options

> **建立自訂 Job Filter 參數**

* **CustomAutoRetryOptions.cs**

```c#
public class CustomAutoRetryOptions 
{
    // 發生錯誤時，最大重試次數
    public int ErrorRetryAttempts { get; set; }
    
    // 錯誤的重試間隔秒數
    public int ErrorRetryDelayInSeconds { get; set;}
    
    // 服務忙碌時，最大重試次數
    public int BusyRetryAttempts { get; set; }
    
    // 服務忙碌的重試間隔秒數
    Public int BusyRetryDelayInSeconds { get; set; }
    
    public CustomAutoRetryOptions() 
    {
    }
    public CustomAutoRetryOptions(CustomAutoRetryOptions options) 
    {
        ErrorRetryAttempts = options.ErrorRetryAttempts;
        ErrorRetryDelayInSeconds = options.ErrorRetryDelayInSeconds;
        BusyRetryAttempts = options.BusyRetryAttempts;
        BusyRetryDelayInSeconds = options.BusyRetryDelayInSeconds;
    }
}
```



## Step.2 建立一個 Custom Job Filter

1. 建立一個 Job Filter Attribute
2. 加入 Custom Job 參數
    * `ErrorRetryAttempts` ─ 錯誤重試
    * `ErrorRetryDelayInSeconds` ─ 錯誤重試間隔
    * `BusyRetryAttempts` ─ 服務忙碌重試
    * `BusyRetryDelayInSeconds` ─ 服務忙碌重試間隔
4. 加入其他屬性
    * 後面步驟會詳細說明
5. 實作建構函式



* **CustomAutoRetryAttribute.cs**

```c#
// [1] 建立一個 Job Filter Attribute
public class CustomAutoRetryAttribute : JobFilterAttribute, IElectStateFilter 
{
    // [2] 加入 Custom Job 參數
    public int ErrorRetryAttempts { get; set; } = 3;
    public int ErrorRetryDelayInSeconds { get; set; } = 5;
    public int BusyRetryAttempts { get; set; } = 3;
    public int BusyRetryDelayInSeconds { get; set; } = 5;
    
    // [3]
    public bool LogEvents { get; set; }
    public AttemptsExceededAction OnAttemptsExceeded { get; set; }
    
    // [4] 建構函式
    public CustomAutoRetryAttribute() 
    {
        LogEvents = true;
        OnAttemptsExceeded = AttemptsExceededAction.Fail;
        Order = 20;
    }
    
    // [Step.3] 實作 Custom Job Filter 的 OnStateElection
    public void OnStateElection(ElectStateContext context)
    {
        // TODO: do something
    }
}
```



---

## Step.3 實作 Custom Job Filter 的 `OnStateElection`

1. 覆寫 `OnStateElection`
    * 不論 Job 成功與否，在 Job 執行後會進到這個 Method
2. 可以依據 Exception 類型做對應的處理
3. 取出 Custom Job Filter Options
    * 取出的來源有兩個：
        1. 來自 `CustomAutoRetryAttribute`  設定的值 (固定值)
        2. 來自 Job Function Arguments (動態值)

* **CustomAutoRetryAttribute.cs**

```c#
// [1] 覆寫 OnStateElection
public void OnStateElection(ElectStateContext context)
{
    // Job 執行結束，檢查 Job 執行狀態
    if (context.CandidateState is FailedState &&
        context.CandidateState != null)
    {   // Job 執行失敗 (發生 Exception) 時的處理
        var failedState = context.CandidateState as FailedState;

        // [3] 取出 Custom Job Filter Options
        var options = GetAutoRetryOptions(context);
        
        // [2] 依據 Exception 類型做對應的處理
        if (failedState.Exception is BusyServiceException)
        {   // 服務忙碌時
            // TODO: do something
        }
        else
        {   // 其他錯誤時
            // TODO: do something
        }
    }
}

// [3] 試著從 Job Function Arguments 中取出 Custom Job Filter Options
private CustomAutoRetryOptions GetAutoRetryOptions(ElectStateContext context)
{
    // (動態) 從 Job Function Arguments 中取出 Custom Job Filter Options
    var args = context.BackgroundJob.Job.Args.ToList();
    foreach (var arg in args)
    {
        if (arg.GetType() == typeof(CustomAutoRetryOptions))
        {
            return new CustomAutoRetryOptions((CustomAutoRetryOptions)arg);
        }
    }
	
    // (固定) 取出 Custom Job Filter 的設定作為 Custom Job Filter Options
    return new CustomAutoRetryOptions() {
        ErrorRetryAttempts = ErrorRetryAttempts,
        ErrorRetryDelayInSeconds = ErrorRetryDelayInSeconds,
        BusyRetryAttempts = BusyRetryAttempts,
        BusyRetryDelayInSeconds = BusyRetryDelayInSeconds
    };
}
```

* **BusyServiceException**

```c#
public class BusyServiceException : Exception 
{
}
```



---

## Step.4 加入 Custom Job Filter

> * **將 Custom Job Filter 加入到 Hangfire Filters**
>     * 這樣的寫法屬於全域的 Filter

* **Startup.cs**

```c#
using Hangfire;
//...

public class Startup 
{
    //...
    
    public void Configure(IApplicationBuilder app, IHostingEnvironment env) 
    {
        //...
        
        // 將 Custom Job Filter 加入到 Hangfire Filters
        GlobalJobFilters.Filters.Add(new CustomAutoRetryAttribute());
        
        //...
    }
}
```



---

## Step.5 使用 Custom Auto Retry

* 在 Job Method 前面加上  `CustomAutoRetry`
    * 可以靜態指派最大重試次數、重試間隔時間
    * 原生的 Auto Retry 處理必須關閉，不然 `CustomAutoRetry` 處理完後，緊接的執行原生的 Auto Retry
* 可以透過 Throw Exception 來觸發 Job Retry

* **DemoJob.cs**

```c#
[CustomAutoRetry(BusyRetryAttempts = 2, BusyRetryDelayInSeconds = 2)] // 自訂 Retry 處理
[AutomaticRetry(Attempts = 0)]                                        // 關閉原生的 Retry 處理
public Task DoBusyThing()
{
    // TODO: Do SomeThing

    // 透過 Throw Exception 來觸發 Job Retry
    throw new BusyServiceException();
}
```



* 執行工作
    * 透過建立 `CustomAutoRetryOptions` 可以動態指派最大重試次數、重試間隔時間

```c#
// 非同步處理 Request
var retryOptions = new CustomAutoRetryOptions();

// 設定錯誤重試、服務忙碌重試的次數和間隔時間
int errorRetryAttempts = 5;
int errorRetryDelayInSeconds = 3;
int busyRetryAttempts = 5;
int busyRetryDelayInSeconds = 3;
retryOptions = retryOptions.SetErrorRetry(errorRetryAttempts, errorRetryDelayInSeconds)
    .SetBusyRetry(busyRetryAttempts, busyRetryDelayInSeconds);

// 錯誤重試、服務忙碌重試的次數和間隔時間的設定需要傳入
BackgroundJob.Enqueue<DemoJob>(job => job.DoErrorThing(retryOptions));
```



