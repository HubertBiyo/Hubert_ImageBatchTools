using System;
using System.Windows.Forms;
using System.Drawing;

namespace BatchFileRenamer
{
    partial class DpiModifyForm : Form
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtSourceFolder;
        private Button btnSelectFolder;
        private Button btnProcess;
        private Label lblSourceFolder;
        private Label lblDpi;
        private NumericUpDown numDpi;
        private ProgressBar progressBar;
        private TextBox txtOutputFolder;          // 添加输出文件夹文本框
        private Button btnSelectOutputFolder;      // 添加输出文件夹选择按钮
        private Label lblOutputFolder;            // 添加输出文件夹标签

        private void InitializeComponent()
        {
            this.txtSourceFolder = new TextBox();
            this.btnSelectFolder = new Button();
            this.btnProcess = new Button();
            this.lblSourceFolder = new Label();
            this.lblDpi = new Label();
            this.numDpi = new NumericUpDown();
            this.progressBar = new ProgressBar();
            
            // 初始化输出文件夹相关控件
            this.txtOutputFolder = new TextBox();
            this.btnSelectOutputFolder = new Button();
            this.lblOutputFolder = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.numDpi)).BeginInit();

            // 源文件夹标签
            this.lblSourceFolder.AutoSize = true;
            this.lblSourceFolder.Location = new Point(20, 25);
            this.lblSourceFolder.Text = "源文件夹：";

            // 源文件夹文本框
            this.txtSourceFolder.Location = new Point(20, 45);
            this.txtSourceFolder.Size = new Size(350, 23);
            this.txtSourceFolder.ReadOnly = true;

            // 源文件夹浏览按钮
            this.btnSelectFolder.Location = new Point(376, 44);
            this.btnSelectFolder.Size = new Size(75, 23);
            this.btnSelectFolder.Text = "浏览...";
            this.btnSelectFolder.Click += new EventHandler(btnSelectFolder_Click);

            // 输出文件夹标签
            this.lblOutputFolder.AutoSize = true;
            this.lblOutputFolder.Location = new Point(20, 85);
            this.lblOutputFolder.Text = "输出文件夹：";

            // 输出文件夹文本框
            this.txtOutputFolder.Location = new Point(20, 105);
            this.txtOutputFolder.Size = new Size(350, 23);
            this.txtOutputFolder.ReadOnly = true;

            // 输出文件夹浏览按钮
            this.btnSelectOutputFolder.Location = new Point(376, 104);
            this.btnSelectOutputFolder.Size = new Size(75, 23);
            this.btnSelectOutputFolder.Text = "浏览...";
            this.btnSelectOutputFolder.Click += new EventHandler(btnSelectOutputFolder_Click);

            // DPI设置标签
            this.lblDpi.AutoSize = true;
            this.lblDpi.Location = new Point(20, 145);
            this.lblDpi.Text = "目标DPI值：";

            // DPI数值选择器
            this.numDpi.Location = new Point(100, 143);
            this.numDpi.Size = new Size(80, 23);
            this.numDpi.Minimum = 72;
            this.numDpi.Maximum = 2400;
            this.numDpi.Value = 300;

            // 进度条
            this.progressBar.Location = new Point(20, 180);
            this.progressBar.Size = new Size(431, 23);

            // 处理按钮
            this.btnProcess.Location = new Point(20, 220);
            this.btnProcess.Size = new Size(431, 40);
            this.btnProcess.Text = "开始处理";
            this.btnProcess.Click += new EventHandler(btnProcess_Click);  // 添加点击事件处理

            // 窗体设置
            this.ClientSize = new Size(473, 280);
            this.Controls.AddRange(new Control[] {
                this.lblSourceFolder,
                this.txtSourceFolder,
                this.btnSelectFolder,
                this.lblOutputFolder,
                this.txtOutputFolder,
                this.btnSelectOutputFolder,
                this.lblDpi,
                this.numDpi,
                this.progressBar,
                this.btnProcess
            });

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.Text = "批量修改图片DPI";

            ((System.ComponentModel.ISupportInitialize)(this.numDpi)).EndInit();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}