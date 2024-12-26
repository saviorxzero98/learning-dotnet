# 圖片壓縮

## 套件

| 套件             | 官網                                                         | 授權                   | 備註                               |
| ---------------- | ------------------------------------------------------------ | ---------------------- | ---------------------------------- |
| `System.Drawing` | [文件](https://learn.microsoft.com/en-us/dotnet/api/system.drawing) | .NET 內建元件          | 限 Windows                         |
| `SkiaSharp`      | [Github](https://github.com/mono/SkiaSharp)                  | MIT                    |                                    |
| `Magick.NET`     | [Github](https://github.com/dlemstra/Magick.NET)             | Apache 2.0             |                                    |
| `NetVips`        | [Github](https://github.com/kleisauke/net-vips)              | MIT                    | 支援的平台是依據安裝的 NuGet 套件  |
| `ImageSharp`     | [官網](https://sixlabors.com/products/imagesharp/)           | Commercial Use License | 只允許非商業使用，商業使用需買授權 |



## 常見的圖片檔格式

|          | 壓縮方式                 | 支援背景透明 | 支援動畫 | 瀏覽器支援                                           |
| -------- | ------------------------ | ------------ | -------- | ---------------------------------------------------- |
| **JPEG** | 有損壓縮                 | 不支援       | 不支援   | 幾乎都支援                                           |
| **PNG**  | 無損壓縮                 | 支援         | 不支援   | 幾乎都支援                                           |
| **BMP**  | 無壓縮 /<br />無損壓縮   | 不支援       | 不支援   | 幾乎都支援                                           |
| **WEBP** | 無損壓縮 /<br />有損壓縮 | 支援         | 支援     | [舊版瀏覽器不支援](https://caniuse.com/?search=webp) |
| **GIF**  | 無損壓縮                 | 支援         | 支援     | 幾乎都支援                                           |
| **SVG**  | 向量圖                   | 支援         | 支援     | 幾乎都支援                                           |

* **壓縮後的圖片大小**
    * **無損壓縮** ： BMP >> PNG > WebP > JPEG
    * **有損壓縮**： PNG > JPEG > WebP



---

## 參考資料

1. 來自 ChatGPT
2. [How to efficiently resize images in .NET core](https://medium.com/@jeroenverhaeghe/how-to-resize-images-in-dotnet-core-2024-edition-acf6dca09afb)