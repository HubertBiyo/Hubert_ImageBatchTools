using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.IO.Compression;  // 添加到文件开头

namespace BatchFileRenamer
{
    public partial class BatchRenameForm : Form
    {
        // 添加类级别的常量
        private const int margin = 20;
        private const int controlSpacing = 25;
        private string currentTempPath = string.Empty;  // 添加临时路径变量
        public BatchRenameForm()
        {
            InitializeComponent();
            AdjustFormSize();       // 只保留一个方法处理所有布局
        }

        private void AdjustFormSize()
        {
            // 基础窗体设置
            this.Width = 700;
            this.Height = 550;
            this.MinimumSize = new Size(700, 550);

            // 调整源文件夹标签和选择控件
            Label lblSourceFolder = new Label
            {
                Text = "源文件夹：",
                AutoSize = true,
                Font = new Font("Microsoft YaHei UI", 9F)
            };
            this.Controls.Add(lblSourceFolder);
            lblSourceFolder.Left = margin;
            lblSourceFolder.Top = margin;

            txtSourceFolder.Left = margin;
            txtSourceFolder.Top = lblSourceFolder.Bottom + 5;
            txtSourceFolder.Width = 550;
            btnSelectFolder.Left = txtSourceFolder.Right + controlSpacing;
            btnSelectFolder.Top = txtSourceFolder.Top;

            // 调整输出文件夹标签和选择控件
            Label lblOutputFolder = new Label
            {
                Text = "输出文件夹：",
                AutoSize = true,
                Font = new Font("Microsoft YaHei UI", 9F)
            };
            this.Controls.Add(lblOutputFolder);
            lblOutputFolder.Left = margin;
            lblOutputFolder.Top = txtSourceFolder.Bottom + controlSpacing;

            txtOutputFolder.Left = margin;
            txtOutputFolder.Top = lblOutputFolder.Bottom + 5;
            txtOutputFolder.Width = 550;
            btnSelectOutputFolder.Left = txtOutputFolder.Right + controlSpacing;
            btnSelectOutputFolder.Top = txtOutputFolder.Top;

            // 创建并调整智能标签容器
            FlowLayoutPanel tagPanel = new FlowLayoutPanel
            {
                Location = new Point(txtOutputFolder.Left, txtOutputFolder.Bottom + 35),
                Width = txtOutputFolder.Width,
                Height = 40,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 8, 0, 8)
            };

            // 添加标签说明和按钮
            Label lblTags = new Label
            {
                Text = "快捷标签：",
                AutoSize = true,
                Font = new Font("Microsoft YaHei UI", 9F),
                Margin = new Padding(0, 3, 10, 0)
            };
            tagPanel.Controls.Add(lblTags);

            var tags = new[]
            {
                ("视觉中国", "SJ_" + DateTime.Now.ToString("yyyyMMdd") + "_"),
                ("图虫", "TC_" + DateTime.Now.ToString("yyyyMMdd") + "_"),
                ("小红书", "S_XHS_"),
                ("光厂", "GC_" + DateTime.Now.ToString("yyyyMMdd") + "_")
            };

            foreach (var (text, prefix) in tags)
            {
                Button tagButton = new Button
                {
                    Text = text,
                    Width = 100,
                    Height = 25,
                    Margin = new Padding(5, 0, 5, 0),
                    Cursor = Cursors.Hand,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(64, 64, 64)
                };
                tagButton.Click += (s, e) => txtPrefix.Text = prefix;
                tagPanel.Controls.Add(tagButton);
            }
            this.Controls.Add(tagPanel);

            // 调整前缀和后缀标签位置
            var lblPrefix = this.Controls.OfType<Label>().FirstOrDefault(l => l.Text == "文件名前缀：");
            var lblSuffix = this.Controls.OfType<Label>().FirstOrDefault(l => l.Text == "后缀：");
            if (lblPrefix != null)
            {
                lblPrefix.Text = "文件名前缀：";
                lblPrefix.Left = margin;
                lblPrefix.Top = tagPanel.Bottom + 15;
            }
            if (lblSuffix != null)
            {
                lblSuffix.Text = "文件名后缀：";
                lblSuffix.Left = this.Width / 2;
                lblSuffix.Top = lblPrefix?.Top ?? tagPanel.Bottom + 15;
            }

            // 调整前缀和后缀输入框
            txtPrefix.Left = margin;
            txtPrefix.Top = lblPrefix?.Bottom + controlSpacing ?? tagPanel.Bottom + controlSpacing * 3;
            txtPrefix.Width = this.Width / 2 - margin * 2;
            txtPrefix.Height = 28;

            txtSuffix.Left = this.Width / 2;
            txtSuffix.Top = txtPrefix.Top;
            txtSuffix.Width = txtPrefix.Width;
            txtSuffix.Height = txtPrefix.Height;

            // 调整起始编号标签和控件
            var lblStartNumber = new Label
            {
                Text = "起始编号：",
                AutoSize = true,
                Font = new Font("Microsoft YaHei UI", 9F)
            };
            this.Controls.Add(lblStartNumber);
            lblStartNumber.Left = margin;
            lblStartNumber.Top = txtPrefix.Bottom + controlSpacing * 2;

            numStartNumber.Left = lblStartNumber.Right + 10;
            numStartNumber.Top = lblStartNumber.Top - 4;
            numStartNumber.Height = 28;
            numStartNumber.Width = 80;

            // 调整删除源文件复选框位置
            chkDeleteSource.Left = numStartNumber.Right + controlSpacing * 2;
            chkDeleteSource.Top = numStartNumber.Top + 4;

            // 调整进度条
            progressBar.Left = margin;
            progressBar.Top = numStartNumber.Bottom + controlSpacing * 2;
            progressBar.Width = this.ClientSize.Width - (margin * 2);
            progressBar.Height = 25;

            // 调整处理按钮
            btnRename.Left = margin;
            btnRename.Top = progressBar.Bottom + controlSpacing;
            btnRename.Width = this.ClientSize.Width - (margin * 2);
            btnRename.Height = 35;
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            // 先清理可能存在的旧临时目录
            CleanupTempDirectory();
            
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "选择包含压缩包或图片的文件夹";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderDialog.SelectedPath;
                    
                    try
                    {
                        // 检查是否包含zip文件
                        bool hasZipFiles = Directory.GetFiles(selectedPath, "*.zip").Any();
                        
                        if (hasZipFiles)
                        {
                            // 如果有压缩包，使用临时目录处理
                            currentTempPath = Path.Combine(Path.GetTempPath(), "IBT_" + DateTime.Now.ToString("HHmmss"));
                            MessageBox.Show("检测到压缩包文件，系统将自动解压到临时目录进行处理。\n解压过程可能需要一点时间，请耐心等待。", 
                                "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                            if (Directory.Exists(currentTempPath))
                            {
                                Directory.Delete(currentTempPath, true);
                            }
                            Directory.CreateDirectory(currentTempPath);
                    
                            // 处理压缩包
                            int zipIndex = 1;
                            foreach (string zipFile in Directory.GetFiles(selectedPath, "*.zip"))
                            {
                                try
                                {
                                    string zipTempPath = Path.Combine(currentTempPath, $"YS_{zipIndex}");
                                    Directory.CreateDirectory(zipTempPath);
                                    ZipFile.ExtractToDirectory(zipFile, zipTempPath);
                                    zipIndex++;
                                }
                                catch (Exception zipEx)
                                {
                                    MessageBox.Show($"解压文件 {Path.GetFileName(zipFile)} 时出错：{zipEx.Message}", 
                                        "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                    
                            // 复制其他文件到临时目录
                            foreach (string imgFile in Directory.GetFiles(selectedPath, "*.*")
                                .Where(f => new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" }
                                .Contains(Path.GetExtension(f).ToLower())))
                            {
                                string destFile = Path.Combine(currentTempPath, Path.GetFileName(imgFile));
                                File.Copy(imgFile, destFile, true);
                            }
                    
                            foreach (string dir in Directory.GetDirectories(selectedPath))
                            {
                                string shortDirName = Path.GetFileName(dir).Substring(0, 
                                    Math.Min(8, Path.GetFileName(dir).Length));
                                string destDir = Path.Combine(currentTempPath, shortDirName);
                                CopyDirectory(dir, destDir);
                            }
                    
                            txtSourceFolder.Text = currentTempPath;
                            MessageBox.Show("已准备好所有文件，处理完成后将自动清理临时文件。", "提示", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // 如果没有压缩包，直接使用原始目录
                            txtSourceFolder.Text = selectedPath;
                            currentTempPath = string.Empty;
                        }
                        
                        Tag = selectedPath;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"处理文件时出错：{ex.Message}", "错误", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CleanupTempDirectory();
                    }
                }
            }
        }

        private void btnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "选择输出文件夹";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputFolder.Text = dialog.SelectedPath;
                }
            }
        }
        private void btnRename_Click(object sender, EventArgs e)
        {
            try
            {
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                int totalFiles = 0;
                int processedFiles = 0;
                string sourceFolder = txtSourceFolder.Text;
                string outputFolder = txtOutputFolder.Text;
                string prefix = txtPrefix.Text.Trim();
                string suffix = txtSuffix.Text.Trim();
                int startNumber = (int)numStartNumber.Value;
                bool deleteSource = chkDeleteSource.Checked;
                if (string.IsNullOrEmpty(sourceFolder))
                {
                    MessageBox.Show("请选择源文件夹！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(outputFolder))
                {
                    MessageBox.Show("请选择输出文件夹！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }
                totalFiles = CountImageFiles(sourceFolder);
                progressBar.Maximum = totalFiles;
                progressBar.Value = 0;

                ProcessDirectory(sourceFolder, outputFolder, prefix, suffix, startNumber, ref processedFiles, deleteSource);
                
                progressBar.Value = totalFiles; // 确保进度条显示完成
                stopwatch.Stop();
                string timeSpent = stopwatch.ElapsedMilliseconds < 1000 ?
                    $"{stopwatch.ElapsedMilliseconds}毫秒" :
                    $"{stopwatch.ElapsedMilliseconds / 1000.0:F1}秒";

                MessageBox.Show(
                    $"重命名完成！\n" +
                    $"共发现：{totalFiles}个文件\n" +
                    $"成功处理：{processedFiles}个文件\n" +
                    $"耗时：{timeSpent}",
                    "处理完成",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"处理过程中出现错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar.Value = 0; // 重置进度条
            }
        }

        private int CountImageFiles(string directory)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" };
            int count = Directory.GetFiles(directory)
                       .Count(f => imageExtensions.Contains(Path.GetExtension(f).ToLower()));
            foreach (string dir in Directory.GetDirectories(directory))
            {
                count += CountImageFiles(dir);
            }
            return count;
        }

        private void ProcessDirectory(string sourceDir, string outputDir, string prefix, string suffix, 
            int startNumber, ref int processedCount, bool deleteSource)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" };
            var files = Directory.GetFiles(sourceDir)
                       .Where(f => imageExtensions.Contains(Path.GetExtension(f).ToLower()));
            foreach (string file in files)
            {
                string extension = Path.GetExtension(file);
                string newName;
                string newPath;
                int duplicateCount = 0;
                
                // 处理文件名重复的情况
                do
                {
                    newName = duplicateCount == 0 
                        ? $"{prefix}{startNumber + processedCount}{suffix}{extension}"
                        : $"{prefix}{startNumber + processedCount}{suffix}_{duplicateCount}{extension}";
                    newPath = Path.Combine(outputDir, newName);
                    duplicateCount++;
                } while (File.Exists(newPath));
                File.Copy(file, newPath);
                processedCount++;
                progressBar.Value = processedCount; // 更新进度条
                Application.DoEvents(); // 确保界面响应
            }
            // 递归处理子文件夹
            foreach (string dir in Directory.GetDirectories(sourceDir))
            {
                ProcessDirectory(dir, outputDir, prefix, suffix, startNumber, ref processedCount, deleteSource);
            }
            // 如果需要删除源文件
            if (deleteSource && !string.IsNullOrEmpty(Tag?.ToString()))
            {
                string originalPath = Tag.ToString();
                try
                {
                    // 删除原始压缩包
                    foreach (string zipFile in Directory.GetFiles(originalPath, "*.zip"))
                    {
                        File.Delete(zipFile);
                    }
                    // 删除原始图片文件
                    foreach (string imgFile in Directory.GetFiles(originalPath, "*.*")
                        .Where(f => new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" }
                        .Contains(Path.GetExtension(f).ToLower())))
                    {
                        File.Delete(imgFile);
                    }
                    // 删除子文件夹
                    foreach (string dir in Directory.GetDirectories(originalPath))
                    {
                        Directory.Delete(dir, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"删除源文件时出错：{ex.Message}", "警告", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        // 添加到类的最后，与其他方法平级
        private void CopyDirectory(string sourceDir, string destDir)
        {
            // 创建目标目录
            Directory.CreateDirectory(destDir);

            // 复制文件
            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                File.Copy(file, destFile);
            }

            // 递归复制子目录
            foreach (string dir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(destDir, Path.GetFileName(dir));
                CopyDirectory(dir, destSubDir);
            }
        }
        private void CleanupTempDirectory()
        {
            if (!string.IsNullOrEmpty(currentTempPath) && Directory.Exists(currentTempPath))
            {
                try
                {
                    Directory.Delete(currentTempPath, true);
                    currentTempPath = string.Empty;
                }
                catch { /* 忽略清理错误 */ }
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            CleanupTempDirectory();
        }
    }
}