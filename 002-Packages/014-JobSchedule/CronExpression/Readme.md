# CRON Expression Parser

## 1. [Hangfire Cronos](https://github.com/HangfireIO/Cronos)

* 目前 Hangfire 最新版本使用的 Cron Expression 套件
* **CRON Expression Format (Exclude Second)**

```
                                   Allowed values  Allowed special characters Comment
┌───────────── minute              0-59            * , - /                      
│ ┌───────────── hour              0-23            * , - /                      
│ │ ┌───────────── day of month    1-31            * , - / L W ?                
│ │ │ ┌───────────── month         1-12 or JAN-DEC * , - /                      
│ │ │ │ ┌───────────── day of week 0-6  or SUN-SAT * , - / # L ?              Both 0 and 7 means SUN
│ │ │ │ │
* * * * *
```

* **CRON Expression Format (Include Second)**

```
                                     Allowed values  Allowed special characters Comment
┌───────────── second (optional)     0-59            * , - /                      
│ ┌───────────── minute              0-59            * , - /                      
│ │ ┌───────────── hour              0-23            * , - /                      
│ │ │ ┌───────────── day of month    1-31            * , - / L W ?                
│ │ │ │ ┌───────────── month         1-12 or JAN-DEC * , - /                      
│ │ │ │ │ ┌───────────── day of week 0-6  or SUN-SAT * , - / # L ?              Both 0 and 7 means SUN
│ │ │ │ │ │
* * * * * *
```



## 2. [NCrontab Advanced](https://github.com/jcoutch/NCrontab-Advanced)

* 改善 [NCrontab](#3-ncrontab) 對於 CRON Expression 的支援程度和問題
* **CRON Expression Format (Exclude Second)**
    * 支援 `#`, `L` and `W`

```
* * * * *
- - - - -
| | | | |
| | | | +----- day of week (0 - 6) (Sunday=0)
| | | +------- month (1 - 12)
| | +--------- day of month (1 - 31)
| +----------- hour (0 - 23)
+------------- min (0 - 59)
```

* **CRON Expression Format (Include Second)**

```
* * * * * *
- - - - - -
| | | | | |
| | | | | +--- day of week (0 - 6) (Sunday=0)
| | | | +----- month (1 - 12)
| | | +------- day of month (1 - 31)
| | +--------- hour (0 - 23)
| +----------- min (0 - 59)
+------------- sec (0 - 59)
```


```
Field name   | Allowed values  | Allowed special characters
------------------------------------------------------------
Minutes      | 0-59            | * , - /
Hours        | 0-23            | * , - /
Day of month | 1-31            | * , - / ? L W
Month        | 1-12 or JAN-DEC | * , - /
Day of week  | 0-6 or SUN-SAT  | * , - / ? L #
Year         | 0001–9999       | * , - /
```



## 3. [NCrontab](https://github.com/atifaziz/NCrontab)

* Hangfire 早期版本使用的 Cron Expression 套件
* **CRON Expression Format (Exclude Second)**

```
* * * * *
- - - - -
| | | | |
| | | | +----- day of week (0 - 6) (Sunday=0)
| | | +------- month (1 - 12)
| | +--------- day of month (1 - 31)
| +----------- hour (0 - 23)
+------------- min (0 - 59)
```

* **CRON Expression Format (Include Second)**

```
* * * * * *
- - - - - -
| | | | | |
| | | | | +--- day of week (0 - 6) (Sunday=0)
| | | | +----- month (1 - 12)
| | | +------- day of month (1 - 31)
| | +--------- hour (0 - 23)
| +----------- min (0 - 59)
+------------- sec (0 - 59)
```



## 4. [Quartz.NET](https://www.quartz-scheduler.net/)

* **[CRON Expression Format](https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/crontrigger.html#examples)**

| **Field Name** | **Mandatory** | **Allowed Values** | **Allowed Special Characters** |
| -------------- | ------------- | ------------------ | ------------------------------ |
| Seconds        | YES           | 0-59               | , - * /                        |
| Minutes        | YES           | 0-59               | , - * /                        |
| Hours          | YES           | 0-23               | , - * /                        |
| Day of month   | YES           | 1-31               | , - * ? / L W                  |
| Month          | YES           | 1-12 or JAN-DEC    | , - * /                        |
| Day of week    | YES           | 1-7 or SUN-SAT     | , - * ? / L #                  |
| Year           | NO            | empty, 1970-2099   | , - * /                        |



---

## 套件比較

* **Vaild Cron Expression & Exclude Second**

```
===> 5 * * * ?
[NCrontab]    Fail;    '?' is not a valid [DayOfWeek] crontab field expression.
[NCrontabAdv] Success; Next Date: 2021-11-24 15:05:00
[Cronos]      Success; Next Date: 2021-11-24 15:05:00
[Quartz]      Fail;    '?' can only be specified for Day-of-Month or Day-of-Week.

===> 5 * * * *
[NCrontab]    Success; Next Date: 2021-11-24 15:05:00
[NCrontabAdv] Success; Next Date: 2021-11-24 15:05:00
[Cronos]      Success; Next Date: 2021-11-24 15:05:00
[Quartz]      Fail;    Unexpected end of expression.

===> 40/59 * * * ?
[NCrontab]    Fail;    '?' is not a valid [DayOfWeek] crontab field expression.
[NCrontabAdv] Success; Next Date: 2021-11-24 15:40:00
[Cronos]      Success; Next Date: 2021-11-24 15:40:00
[Quartz]      Fail;    '?' can only be specified for Day-of-Month or Day-of-Week.

===> 40/59 * * * *
[NCrontab]    Success; Next Date: 2021-11-24 15:40:00
[NCrontabAdv] Success; Next Date: 2021-11-24 15:40:00
[Cronos]      Success; Next Date: 2021-11-24 15:40:00
[Quartz]      Fail;    Unexpected end of expression.
```



* **Vaild Cron Expression & Include Second**

```
===> 5 * * * * ?
[NCrontab]    Fail; '?' is not a valid [DayOfWeek] crontab field expression.
[NCrontabAdv] Success; Next Date: 2021-11-24 14:34:05
[Cronos]      Success; Next Date: 2021-11-24 14:34:05
[Quartz]      Success; Next Date: 2021-11-24 14:34:05

===> 5 * * * * *
[NCrontab]    Success; Next Date: 2021-11-24 14:34:05
[NCrontabAdv] Success; Next Date: 2021-11-24 14:34:05
[Cronos]      Success; Next Date: 2021-11-24 14:34:05
[Quartz]      Fail;    Support for specifying both a day-of-week AND a day-of-month parameter is not implemented.

===> 40/59 * * * * ?
[NCrontab]    Fail;    '?' is not a valid [DayOfWeek] crontab field expression.
[NCrontabAdv] Success; Next Date: 2021-11-24 14:44:40
[Cronos]      Success; Next Date: 2021-11-24 14:44:40
[Quartz]      Success; Next Date: 2021-11-24 14:44:40

===> 40/59 * * * * *
[NCrontab]    Success; Next Date: 2021-11-24 14:44:40
[NCrontabAdv] Success; Next Date: 2021-11-24 14:44:40
[Cronos]      Success; Next Date: 2021-11-24 14:44:40
[Quartz]      Fail;    Support for specifying both a day-of-week AND a day-of-month parameter is not implemented.
```



* **Vaild Cron Expression & Include Second and Year**

```
===> 5 * * * * * 2022
[NCrontab]    Fail;    '5 * * * * * 2022' is an invalid crontab expression. It must contain 6 components of a schedule in the sequence of seconds, minutes, hours, days, months, and days of week.
[NCrontabAdv] Success; Next Date: 2022-01-01 00:00:05
[Cronos]      Fail;    Unexpected character '*'.
[Quartz]      Fail;    Support for specifying both a day-of-week AND a day-of-month parameter is not implemented.
```



* **Invalid Cron Expression**

```
===> 40/60 * * * *
[NCrontab]    Success; Next Date: 2021-11-24 14:40:00
[NCrontabAdv] Fail;    There was an error parsing '40/60' for the Minute field
[Cronos]      Fail;    Minutes: Value must be a number between 1 and 59 (all inclusive).
[Quartz]      Fail;    Increment > 60 : 60

===> 40/60 * * * * *
[NCrontab]    Success; Next Date: 2021-11-24 14:44:40
[NCrontabAdv] Fail;    There was an error parsing '40/60' for the Second field
[Cronos]      Fail;    Seconds: Value must be a number between 1 and 59 (all inclusive).
[Quartz]      Fail;    Increment > 60 : 60

===> 1/10 40/60 * * * *
[NCrontab]    Success; Next Date: 2021-11-24 14:40:41
[NCrontabAdv] Fail;    There was an error parsing '40/60' for the Minute field
[Cronos]      Fail;    Minutes: Value must be a number between 1 and 59 (all inclusive).
[Quartz]      Fail;    Increment > 60 : 60
```

