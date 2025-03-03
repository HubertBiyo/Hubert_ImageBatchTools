using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BatchFileRenamer
{
    public partial class BatchRenameForm : Form
    {
        public BatchRenameForm()
        {
            InitializeComponent();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "选择要重命名的文件夹";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtSourceFolder.Text = folderDialog.SelectedPath;
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
                ProcessDirectory(sourceFolder, outputFolder, prefix, suffix, startNumber, ref processedFiles, deleteSource);
                
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
                string newName = $"{prefix}{startNumber + processedCount}{suffix}{extension}";
                string newPath = Path.Combine(outputDir, newName);
            
                if (File.Exists(newPath))
                {
                    throw new Exception($"文件 {newName} 已存在！");
                }
            
                File.Copy(file, newPath);
                processedCount++;
            }
            // 递归处理子文件夹
            foreach (string dir in Directory.GetDirectories(sourceDir))
            {
                ProcessDirectory(dir, outputDir, prefix, suffix, startNumber, ref processedCount, deleteSource);
            }
            // 如果需要删除源文件，在处理完当前文件夹后删除它
            if (deleteSource)
            {
                try
                {
                    Directory.Delete(sourceDir, true);  // true 表示递归删除所有内容
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"删除文件夹 {sourceDir} 时出错：{ex.Message}", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}