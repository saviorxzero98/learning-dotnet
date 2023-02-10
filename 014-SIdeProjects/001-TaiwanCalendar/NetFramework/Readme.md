# 取得中華民國例假日、放假日

## 資料來源
* 使用台北市政府資料開放平台的資料 (政府行政機關辦公日曆表)  [連結](http://data.taipei/opendata/datalist/datasetMeta?oid=9cfba4c6-3caa-48ff-a926-f903c74c5736)
* 使用新北市政府資料開放平台的資料 (政府行政機關辦公日曆表)  [連結](http://data.ntpc.gov.tw/od/detail?oid=308DCD75-6434-45BC-A95F-584DA4FED251)

---

## 使用範例

```cs
// 建立 (設定資料來源)
CalendarService service = CalendarService.GetInstance(CalendarDataSource.Taipei);

// 日期
DateTime date = new DateTime(2017, 1, 29);

// 取得放假日或彈性上班日，一般上班日則為null
var holiday = service.GetHoliday(date);

// 取得是否為放假日
var isHoliday = service.IsHoliday(date);
```