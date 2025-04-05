# Entity Framework Core



* 安裝 Entity Framework Core CLI

```
dotnet tool install --global dotnet-ef
```

* 產生 Database Migration

```
dotnet ef migrations add <Migration Description>
```

* 執行 Database Migration

```json
dotnet ef database update --context <DBContext>
```

