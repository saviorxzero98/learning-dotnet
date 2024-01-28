# Refitter



## 安裝 CLI Tools



```
dotnet tool install --global Refitter
```



---

## 安裝 Pagckages



```json
dotnet add package Refit
dotnet add package Refit.HttpClientFactory
dotnet add package Refit.Newtonsoft.Json
```



---

## Source Generator

* **Example**

```
refitter ./swagger.json --namespace "RefitterApiClient" --output ./DemoHttpClient.cs --use-api-response --no-operation-headers --no-auto-generated-header
```



---

## Reference

* [Refitter - Github](https://github.com/christianhelle/refitter)
* [Refit - Github](https://github.com/reactiveui/refit)

