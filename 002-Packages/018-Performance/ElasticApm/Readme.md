# Elatic APM

## 1. 安裝

* 安裝 Elastic.Apm.AspNetCore

```shell
dotnet add package Elastic.Apm.AspNetCore
```



## 2. 加入 Elatic APM 設定

* 修改 appsettings.json，加入 Elatic APM 設定

```json
{
    "ElasticApm": {
        "ServiceName": "apm-demo-site",
        "SecretToken": "<YourSecretToken>",
        "ServerUrl": "http://localhost:8200",
        "TransactionSampleRate": 1.0,
        "Enabled": false
    }
}
```



## 3. 在 Starup.cs 使用 Elatic APM

* 在 Starup.cs 的 Configure 中加入 `UseElasticApm`
    * 可以依照專案的需求添加相關的 Subscriber
        * `HttpDiagnosticsSubscriber` ─ HTTP
        * `SqlClientDiagnosticSubscriber` ─ Microsoft SQL Server
        * ...

```c#
public class Startup
{
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // 加上 Elastic APM，Subscriber 可以依照專案的需求添加
        app.UseElasticApm(Configuration, new HttpDiagnosticsSubscriber());
    }
}
```

