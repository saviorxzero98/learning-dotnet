# E-Mail 發送



## E-Mail 套件

| Package Name                                                 | Description                                                  |
| ------------------------------------------------------------ | ------------------------------------------------------------ |
| System.Net.Mail                                              | .NET 內建的套件，支援 SMTP，但被 Microsoft [標註為已過時](https://learn.microsoft.com/en-us/dotnet/api/system.net.mail.smtpclient?view=net-6.0#remarks) |
| [MailKit](https://github.com/jstedfast/MailKit)              | 支援 IMAP、POP3 和 SMTP                                      |
| [FluentEmail](https://github.com/lukencode/FluentEmail)      | 支援 IMAP、POP3 和 SMTP，整合 [MailGun](https://www.mailgun.com)、[SendGrid](https://sendgrid.com) 和 [Mailtrap](https://mailtrap.io) 等 E-mail 服務<br />同時提供 Razor 和 Liquid 兩個 E-mail Template Renderer |
| [Jcamp FluentEmail](https://github.com/jcamp-code/FluentEmail) | Fork 自 [FluentEmail](https://github.com/lukencode/FluentEmail)，額外再整合 [Postmark](https://postmarkapp.com)、[MailerSend](https://www.mailersend.com) 的等 E-mail 服務 |



---

## 提供 E-mail 寄送測試的服務

| Name                                                         | Description                   | Limit           |
| ------------------------------------------------------------ | ----------------------------- | --------------- |
| [Mailtrap](https://mailtrap.io)                              | 雲端 E-mail 服務              | 每月免費 100封  |
| [SendGrid](https://sendgrid.com)                             | 雲端 E-mail 服務              | 每月免費 100封  |
| [Postmark](https://postmarkapp.com)                          | 雲端 E-mail 服務              | 每月免費 100封  |
| [MailerSend](https://www.mailersend.com)                     | 雲端 E-mail 服務              | 每月免費 3000封 |
| [MailGun](https://www.mailgun.com)                           | 雲端 E-mail 服務              | 每月免費 5000封 |
| [smtp4dev](https://github.com/rnwood/smtp4dev)               | 地端 Mail Server 測試服務     | N/A             |
| [papercut](https://github.com/ChangemakerStudios/Papercut-SMTP) | 地端 Mail Server  測試服務    | N/A             |
| [hMailServer](https://github.com/hmailserver/hmailserver/)   | 地端開源的 Mail Server        | N/A             |
| [IIS](https://learn.microsoft.com/en-US/iis/application-frameworks/install-and-configure-php-on-iis/configure-smtp-e-mail-in-iis-7-and-above) | 地端 IIS 上提供的 Mail Server | N/A             |



