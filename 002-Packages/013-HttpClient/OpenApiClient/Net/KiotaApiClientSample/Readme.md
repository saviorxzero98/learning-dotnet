# Kiota

## 安裝 CLI Tools

* [Install Doc](https://learn.microsoft.com/zh-tw/openapi/kiota/install)

* .NET CLI Tools

```
dotnet tool install --global Microsoft.OpenApi.Kiota
```



## 安裝 Pagckages



```
dotnet add package Microsoft.Kiota.Abstractions
dotnet add package Microsoft.Kiota.Http.HttpClientLibrary
dotnet add package Microsoft.Kiota.Serialization.Form
dotnet add package Microsoft.Kiota.Serialization.Json
dotnet add package Microsoft.Kiota.Serialization.Text
dotnet add package Microsoft.Kiota.Serialization.Multipart
```



---

## Source Generator

* **Example**

```
kiota generate -l CSharp -c DemoHttpClient -n KiotaApiClient -d ./swagger.json -o ./
```



---

## Reference

* [Kiota - Github](https://github.com/microsoft/kiota)