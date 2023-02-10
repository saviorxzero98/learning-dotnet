# 輕量級資料庫工具

* **[Fluent Mirgriation](https://fluentmigrator.github.io/index.html)**
    * 用於管理資料庫各個版本的異動
    * 資料庫支援常見的 SQL Server、PostgreSQL、MySQL、SQLite、Oracle... ([支援列表](https://fluentmigrator.github.io/articles/multi-db-support.html))
    * 提供資料庫 Table 的新增、刪除、修改
    * 目前沒有提供資料庫 View 新增、刪除、修改專屬的函式
        * 需要依照各個資料庫各自去寫 SQL Statement
        * 可以搭配 Sqlkata 產生 SELECT SQL Statement
* **[Sqlkata](https://sqlkata.com/)**
    * 可以不同的資料庫，產生對應的 SQL Statement 
    * 亦提供 ORM 的功能
    * 資料庫支援 SQL Server、PostgreSQL、MySQL、SQLite、Oracle、Firebird
    * [Playground](https://sqlkata.com/playground)
* **[Dapper](https://dapper-tutorial.net/dapper)**
    * 輕量級的 ORM 工具



---

## ● Fluent Mirgriation

### 1. 環境支援

* **Fluent Mirgriation 3**
    * .NETFramework 4.6.1+
    * .NETStandard 2.0+
* **Fluent Mirgriation 2**
    * .NETFramework 4.0+
    * .NETStandard 2.0+



---

### 2. 安裝

* **必要套件**

```powershell
Install-Package FluentMigrator
Install-Package FluentMigrator.Runner
```

* **可選套件 (依據使用資料庫)**

```powershell
Install-Package FluentMigrator.Runner.SqlServer
Install-Package FluentMigrator.Runner.Postgres
Install-Package FluentMigrator.Runner.SQLite
Install-Package FluentMigrator.Runner.MySql
```



---

### 3. 使用

* [查看範例專案](https://git.gss.com.tw/MartyLearnSample/csharp/fluent-mirgriation)



---



## ● Sqlkata

### 1. 環境支援

* **Sqlkata 2**
    * .NETFramework 4.5+
    * .NETStandard 1.0+
* **SqlKata.Execution**
    * .NETFramework 4.5.1+
    * .NETStandard 1.3+



---

### 2. 安裝

* **必要套件**

```powershell
Install-Package SqlKata
```

* **可選套件 (ORM 執行套件)**

```powershell
Install-Package SqlKata.Execution
```



---

### 3. 使用

* [查看範例專案](https://git.gss.com.tw/MartyLearnSample/csharp/sqlkata)



## ● Dapper

### 1. 環境支援

* **Dapper 2**
    * .NETFramework 4.6.1+
    * .NETStandard 2.0+
* **Dapper 1**
    * .NETFramework 4.5.1+
    * .NETStandard 1.3+



---

### 2. 安裝

* **必要套件**

```powershell
Install-Package Dapper
```



---

### 3. 使用

* [查看範例專案](https://git.gss.com.tw/MartyLearnSample/csharp/Dapper)