using System;
using System.Windows.Forms;
using System.Drawing;

namespace BatchFileRenamer
{
    partial class BatchRenameForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtSourceFolder;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.NumericUpDown numStartNumber;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Label lblSourceFolder;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.Label lblSuffix;
        private System.Windows.Forms.Label lblStartNumber;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Button btnSelectOutputFolder;
        private System.Windows.Forms.Label lblOutputFolder;
        private System.Windows.Forms.CheckBox chkDeleteSource;
        private System.Windows.Forms.ProgressBar progressBar;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        
        #region Windows Form Designer generated code
        
        private void InitializeComponent()
        {
            this.txtSourceFolder = new System.Windows.Forms.TextBox();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.numStartNumber = new System.Windows.Forms.NumericUpDown();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.lblSourceFolder = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.lblSuffix = new System.Windows.Forms.Label();
            this.lblStartNumber = new System.Windows.Forms.Label();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.btnSelectOutputFolder = new System.Windows.Forms.Button();
            this.lblOutputFolder = new System.Windows.Forms.Label();
        
            ((System.ComponentModel.ISupportInitialize)(this.numStartNumber)).BeginInit();
        
            // 源文件夹标签
            this.lblSourceFolder.AutoSize = true;
            this.lblSourceFolder.Location = new System.Drawing.Point(20, 25);
            this.lblSourceFolder.Text = "源文件夹：";
        
            // 源文件夹文本框
            this.txtSourceFolder.Location = new System.Drawing.Point(20, 45);
            this.txtSourceFolder.Size = new System.Drawing.Size(350, 23);
            this.txtSourceFolder.ReadOnly = true;
        
            // 浏览按钮
            this.btnSelectFolder.Location = new System.Drawing.Point(376, 44);
            this.btnSelectFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFolder.Text = "浏览...";
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
        
            // 输出文件夹标签
            this.lblOutputFolder.AutoSize = true;
            this.lblOutputFolder.Location = new System.Drawing.Point(20, 85);
            this.lblOutputFolder.Text = "输出文件夹：";
        
            // 输出文件夹文本框
            this.txtOutputFolder.Location = new System.Drawing.Point(20, 105);
            this.txtOutputFolder.Size = new System.Drawing.Size(350, 23);
            this.txtOutputFolder.ReadOnly = true;
        
            // 输出文件夹浏览按钮
            this.btnSelectOutputFolder.Location = new System.Drawing.Point(376, 104);
            this.btnSelectOutputFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSelectOutputFolder.Text = "浏览...";
            this.btnSelectOutputFolder.Click += new System.EventHandler(this.btnSelectOutputFolder_Click);
        
            // 前缀标签
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(20, 145);
            this.lblPrefix.Text = "文件名前缀：";
        
            // 前缀文本框
            this.txtPrefix.Location = new System.Drawing.Point(100, 142);
            this.txtPrefix.Size = new System.Drawing.Size(100, 23);
        
            // 后缀标签
            this.lblSuffix.AutoSize = true;
            this.lblSuffix.Location = new System.Drawing.Point(220, 145);
            this.lblSuffix.Text = "后缀：";
        
            // 后缀文本框
            this.txtSuffix.Location = new System.Drawing.Point(270, 142);
            this.txtSuffix.Size = new System.Drawing.Size(100, 23);
        
            // 起始编号标签
            this.lblStartNumber.AutoSize = true;
            this.lblStartNumber.Location = new System.Drawing.Point(20, 185);
            this.lblStartNumber.Text = "起始编号：";
        
            // 起始编号选择器
            this.numStartNumber.Location = new System.Drawing.Point(100, 183);
            this.numStartNumber.Size = new System.Drawing.Size(80, 23);
            this.numStartNumber.Minimum = 0;
            this.numStartNumber.Maximum = 999999;
            this.numStartNumber.Value = 1;
            // 删除源文件选项
            // 在其他控件初始化后添加
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.chkDeleteSource = new System.Windows.Forms.CheckBox();
            this.chkDeleteSource.Name = "chkDeleteSourceFiles";  // 更规范的命名
            this.chkDeleteSource.Text = "处理完成后删除源文件";
            this.chkDeleteSource.AutoSize = true;
            this.chkDeleteSource.Location = new System.Drawing.Point(200, 183);  // 调整到与起始编号选择器同一行
            this.chkDeleteSource.Size = new System.Drawing.Size(150, 21);
            this.chkDeleteSource.UseVisualStyleBackColor = true;
        
            // 进度条
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.progressBar.Location = new System.Drawing.Point(20, 220);
            this.progressBar.Size = new System.Drawing.Size(431, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.Value = 0;
        
            // 重命名按钮 - 位置下移
            this.btnRename.Location = new System.Drawing.Point(20, 250);
            this.btnRename.Size = new System.Drawing.Size(431, 40);
            this.btnRename.Text = "开始重命名";
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
        // 窗体设置
            this.ClientSize = new System.Drawing.Size(473, 310);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;  // 修改为屏幕居中
            this.MaximizeBox = false;
            this.Text = "批量重命名";
        
            ((System.ComponentModel.ISupportInitialize)(this.numStartNumber)).EndInit();
        
            // 在 Controls.AddRange 中添加进度条
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblSourceFolder,
                this.txtSourceFolder,
                this.btnSelectFolder,
                this.lblOutputFolder,
                this.txtOutputFolder,
                this.btnSelectOutputFolder,
                this.lblPrefix,
                this.txtPrefix,
                this.lblSuffix,
                this.txtSuffix,
                this.lblStartNumber,
                this.numStartNumber,
                this.chkDeleteSource,    // 添加到控件集合中
                this.progressBar,
                this.btnRename
            });
        }
        #endregion
    }
}