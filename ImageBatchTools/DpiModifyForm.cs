using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using ImageMagick;

namespace BatchFileRenamer
{
    public partial class DpiModifyForm : Form
    {
        public DpiModifyForm()
        {
            InitializeComponent();
            MagickNET.Initialize();  // 初始化 ImageMagick
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "选择要修改DPI的图片文件夹";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtSourceFolder.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void btnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "选择输出文件夹";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputFolder.Text = folderDialog.SelectedPath;
                }
            }
        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSourceFolder.Text))
            {
                MessageBox.Show("请选择源文件夹！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtOutputFolder.Text))
            {
                MessageBox.Show("请选择输出文件夹！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnProcess.Enabled = false;
                btnSelectFolder.Enabled = false;
                btnSelectOutputFolder.Enabled = false;
                progressBar.Value = 0;

                string sourceFolder = txtSourceFolder.Text;
                string outputFolder = txtOutputFolder.Text;
                int targetDpi = (int)numDpi.Value;

                // 确保输出目录存在
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                await Task.Run(() =>
                {
                    string[] supportedExtensions = { ".jpg", ".jpeg", ".png", ".tiff", ".tif" };
                    var imageFiles = Directory.GetFiles(sourceFolder, "*.*", SearchOption.AllDirectories)
                        .Where(f => supportedExtensions.Contains(Path.GetExtension(f).ToLower()))
                        .ToList();

                    int totalFiles = imageFiles.Count;
                    int processedFiles = 0;

                    this.Invoke(() => progressBar.Maximum = totalFiles);

                    foreach (string file in imageFiles)
                    {
                        try
                        {
                            using (var image = new MagickImage(file))
                            {
                                var originalFormat = image.Format;
                                var originalQuality = image.Quality;
                                
                                image.Density = new Density(targetDpi);
                                
                                // 使用输出文件夹
                                string relativePath = Path.GetRelativePath(sourceFolder, Path.GetDirectoryName(file));
                                string outputPath = Path.Combine(outputFolder, relativePath);
                                Directory.CreateDirectory(outputPath);
                                
                                string newFile = Path.Combine(
                                    outputPath,
                                    Path.GetFileNameWithoutExtension(file) + Path.GetExtension(file)
                                );
                                
                                image.Format = originalFormat;
                                image.Quality = originalQuality;
                                image.Write(newFile);
                            }
                        }
                        catch (Exception imageEx)
                        {
                            this.Invoke(() => MessageBox.Show($"处理文件 {Path.GetFileName(file)} 时出错：{imageEx.Message}", 
                                "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                            continue;
                        }
                    
                        processedFiles++;
                        if (processedFiles % 5 == 0)
                        {
                            this.Invoke(() => progressBar.Value = processedFiles);
                        }
                    }
                    this.Invoke(() =>
                    {
                        progressBar.Value = totalFiles;
                        MessageBox.Show($"DPI修改完成！共处理{totalFiles}个文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"处理过程中出现错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnProcess.Enabled = true;
                btnSelectFolder.Enabled = true;
                btnSelectOutputFolder.Enabled = true;
                progressBar.Value = 0;
            }
        }
    }
}