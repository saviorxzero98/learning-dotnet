# UUID

## UUID 版本



| 版本 | 產生方式               | 描述                                                         | 範例                                   |
| ---- | ---------------------- | ------------------------------------------------------------ | -------------------------------------- |
| 1    | 日期時間 + MAC Address | 安全性較低                                                   | `xxxxxxxx-xxxx-1xxx-xxxx-xxxxxxxxxxxx` |
| 2    | 日期時間 + IP Address  | 安全性較低                                                   | `xxxxxxxx-xxxx-2xxx-xxxx-xxxxxxxxxxxx` |
| 3    | 命名空間 + MD5雜湊     | 安全性高                                                     | `xxxxxxxx-xxxx-3xxx-xxxx-xxxxxxxxxxxx` |
| 4    | 隨機亂數               | 安全性高                                                     | `xxxxxxxx-xxxx-4xxx-xxxx-xxxxxxxxxxxx` |
| 5    | 命名空間 + SHA1雜湊    | 安全性高                                                     | `xxxxxxxx-xxxx-5xxx-xxxx-xxxxxxxxxxxx` |
| 6    | 日期時間 + 隨機亂數    | 實驗性版本，改善資料庫排序上的效能                           | `xxxxxxxx-xxxx-6xxx-xxxx-xxxxxxxxxxxx` |
| 7    | 日期時間 + 隨機亂數    | 實驗性版本，改善資料庫排序上的效能<br />常用於 PostgreSQL、SQLite 和 MySQL 上 | `xxxxxxxx-xxxx-7xxx-xxxx-xxxxxxxxxxxx` |
| 8    | 隨機亂數 + 日期時間    | 實驗性版本，改善資料庫排序上的效能<br />常用於 Microsoft SQL Server 上 | `xxxxxxxx-xxxx-8xxx-xxxx-xxxxxxxxxxxx` |



