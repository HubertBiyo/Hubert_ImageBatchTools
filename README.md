# Image Batch Tools

一个简单实用的图片批量处理工具。

## 功能特点

- 支持批量重命名图片文件
- 支持自动解压处理 ZIP 压缩包
- 支持快捷标签（视觉中国、图虫等）
- 支持自定义前缀后缀
- 支持自定义起始编号
- 支持删除源文件选项
- 支持批量修改图片 DPI 值
- 支持保持原始图片质量

## 更新日志

### 2024-03-08
- 优化压缩包处理逻辑，支持多个压缩包的解压
- 添加压缩包处理提示信息
- 优化非压缩包文件夹的处理逻辑，无需创建临时目录
- 优化临时文件夹命名方式，使用 YS_1、YS_2 等形式
- 添加文件处理进度提示
- 完善错误处理机制

### 2024-03-07
- 添加临时文件自动清理功能
- 修复文件重名问题
- 优化进度条显示
- 添加处理时间统计
- 完善错误提示信息

### 2024-03-06
- 实现基础的文件重命名功能
- 添加快捷标签功能（视觉中国、图虫、小红书、光厂）
- 添加进度显示功能
- 支持子文件夹递归处理
- 添加文件删除选项

### 2024-03-05
- 项目初始化
- 设计用户界面布局
- 实现基础文件选择功能
- 添加基本配置选项

## 功能说明

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