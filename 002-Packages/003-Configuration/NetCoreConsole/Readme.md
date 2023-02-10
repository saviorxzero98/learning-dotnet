# 如何在 .NET Core Console 應用程式中取得 App Settings 和 Connection String

## 方法一、使用 `System.Configuration.ConfigurationManager`

> 同 .NET Framework 的處理方式

### 1. 安裝 NuGet 套件

* 安裝 `System.Configuration.ConfigurationManager`

```powershell
Install-Package System.Configuration.ConfigurationManager
```



### 2. 建立 `App.Config`

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <appSettings>
        <add key="MySetting" value="0806449"  />
    </appSettings>

    <connectionStrings>
        <add name="MY_DB" connectionString="Data Source=(localdb)\ProjectsV13;Initial Catalog=MyDB;Application Name=MyDB" />
    </connectionStrings>
</configuration>
```



### 3. 加入 Using

```c#
using System.Configuration;
```



### 4. 取得設定

```c#
string mySetting = ConfigurationManager.AppSettings.Get("MySetting");
string myConnection = ConfigurationManager.ConnectionStrings["MY_DB"].ToString();
```



---

## 方法二、使用 `Microsoft.Extensions.Configuration`

### 1. 安裝 NuGet 套件

* `Microsoft.Extensions.Configuration.Json`
    * 支援 JSON 設定檔
* `Microsoft.Extensions.Configuration.Binder`
    * 設定檔資料處理

```powershell
Install-Package Microsoft.Extensions.Configuration.Json
Install-Package Microsoft.Extensions.Configuration.Binder
```



### 2. 建立 appsettings.json

```json
{
    "ConnectionStrings": {
        "Default": "Data Source=(localdb)\\ProjectsV13;Initial Catalog=MyDB;Application Name=MyDB"
    },
    "MySetting": {
        "Phone": "0806449"
    }
}
```



### 3. 加入 Using

```c#
using Microsoft.Extensions.Configuration;
```



### 4. 建立 IConfiguration 物件

```c#
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
```



### 5. 取得設定

```c#
// Get App Settings (Object Data)
MySetting mySetting = config.GetSection("MySetting").Get<MySetting>();
string myPhone = mySetting.Phone

// Get App Settings (Key-Value Data)
string mySetting = config.GetSection("MySetting:Phone").Value;

// Get Connection String
string myConnection = config.GetConnectionString("Default");
```



```c#
public class MySetting
{
    public string Phone { get; set; }
}
```



---



## 比較

* **方法一、使用 `System.Configuration.ConfigurationManager`**
    * **優點**
        1. 可以直接存取設定檔
        2. 從 .NET Framework 轉移到 .NET Core 不需要改寫作法
    * **缺點**
        1. 無結構化設定
        2. 僅支援 XML 格式
* **方法二、使用 `Microsoft.Extensions.Configuration`**
    * **優點**
        1. 提供結構化設定
        2. 支援 JSON File、XML File、INI File、User Secrets (加密檔案)、Command Line (命令列)、Environment Variables (環境變數) 等設定格式
    * **缺點**
        1. 需要一層一層傳遞 `IConfiguration` 物件



---



## 參考資料

* **[How to Use System.Configuration.ConfigurationManager on .NET CORE](https://www.itnota.com/use-system-configuration-configurationmanager-dot-net-core/)**