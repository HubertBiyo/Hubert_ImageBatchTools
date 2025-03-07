# Image Batch Tools

一个简单实用的图片批处理工具集。

## 功能特点

### 1. 批量重命名
- 支持批量重命名图片文件
- 可自定义前缀、后缀和起始编号
- 支持递归处理子文件夹
- 可选择是否删除源文件
- 支持的图片格式：jpg、jpeg、png、gif、bmp、tiff

### 2. 批量修改DPI
- 支持批量修改图片DPI值
- 保持原始图片质量
- 支持递归处理子文件夹
- 支持的图片格式：jpg、jpeg、png、tiff、tif
- 保持原始文件夹结构

## 系统要求
- Windows 操作系统
- .NET 6.0 或更高版本
- ImageMagick.NET 依赖库

## 发布命令
```bash
# 发布为单文件应用（Windows x64）
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true

# 使用 WiX Toolset 创建安装包
# 编译 WiX 源文件
"C:\Program Files (x86)\WiX Toolset v3.14\bin\candle.exe" "Product.wxs"

# 生成 MSI 安装包
"C:\Program Files (x86)\WiX Toolset v3.14\bin\light.exe" -ext WixUIExtension "Product.wixobj" -out "ImageBatchTools.msi"