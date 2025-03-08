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
            txtSourceFolder = new TextBox();
            txtPrefix = new TextBox();
            txtSuffix = new TextBox();
            numStartNumber = new NumericUpDown();
            btnSelectFolder = new Button();
            btnRename = new Button();
            lblSourceFolder = new Label();
            lblPrefix = new Label();
            lblSuffix = new Label();
            lblStartNumber = new Label();
            txtOutputFolder = new TextBox();
            btnSelectOutputFolder = new Button();
            lblOutputFolder = new Label();
            progressBar = new ProgressBar();
            chkDeleteSource = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)numStartNumber).BeginInit();
            SuspendLayout();
            // 
            // txtSourceFolder
            // 
            txtSourceFolder.Location = new Point(20, 45);
            txtSourceFolder.Name = "txtSourceFolder";
            txtSourceFolder.ReadOnly = true;
            txtSourceFolder.Size = new Size(350, 23);
            txtSourceFolder.TabIndex = 1;
            // 
            // txtPrefix
            // 
            txtPrefix.Location = new Point(100, 142);
            txtPrefix.Name = "txtPrefix";
            txtPrefix.Size = new Size(100, 23);
            txtPrefix.TabIndex = 7;
            // 
            // txtSuffix
            // 
            txtSuffix.Location = new Point(270, 142);
            txtSuffix.Name = "txtSuffix";
            txtSuffix.Size = new Size(100, 23);
            txtSuffix.TabIndex = 9;
            // 
            // numStartNumber
            // 
            numStartNumber.Location = new Point(100, 183);
            numStartNumber.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numStartNumber.Name = "numStartNumber";
            numStartNumber.Size = new Size(80, 23);
            numStartNumber.TabIndex = 11;
            numStartNumber.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(376, 44);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(75, 23);
            btnSelectFolder.TabIndex = 2;
            btnSelectFolder.Text = "浏览...";
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // btnRename
            // 
            btnRename.Location = new Point(20, 250);
            btnRename.Name = "btnRename";
            btnRename.Size = new Size(431, 40);
            btnRename.TabIndex = 14;
            btnRename.Text = "开始重命名";
            btnRename.Click += btnRename_Click;
            // 
            // lblPrefix
            // 
            lblPrefix.AutoSize = true;
            lblPrefix.Location = new Point(20, 145);
            lblPrefix.Name = "lblPrefix";
            lblPrefix.Size = new Size(80, 17);
            lblPrefix.TabIndex = 6;
            lblPrefix.Text = "文件名前缀：";
            // 
            // lblSuffix
            // 
            lblSuffix.AutoSize = true;
            lblSuffix.Location = new Point(220, 145);
            lblSuffix.Name = "lblSuffix";
            lblSuffix.Size = new Size(44, 17);
            lblSuffix.TabIndex = 8;
            lblSuffix.Text = "后缀：";
            // 
            // txtOutputFolder
            // 
            txtOutputFolder.Location = new Point(20, 105);
            txtOutputFolder.Name = "txtOutputFolder";
            txtOutputFolder.ReadOnly = true;
            txtOutputFolder.Size = new Size(350, 23);
            txtOutputFolder.TabIndex = 4;
            // 
            // btnSelectOutputFolder
            // 
            btnSelectOutputFolder.Location = new Point(376, 104);
            btnSelectOutputFolder.Name = "btnSelectOutputFolder";
            btnSelectOutputFolder.Size = new Size(75, 23);
            btnSelectOutputFolder.TabIndex = 5;
            btnSelectOutputFolder.Text = "浏览...";
            btnSelectOutputFolder.Click += btnSelectOutputFolder_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(20, 220);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(431, 23);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 13;
            // 
            // chkDeleteSource
            // 
            chkDeleteSource.AutoSize = true;
            chkDeleteSource.Location = new Point(200, 183);
            chkDeleteSource.Name = "chkDeleteSource";
            chkDeleteSource.Size = new Size(147, 21);
            chkDeleteSource.TabIndex = 12;
            chkDeleteSource.Text = "处理完成后删除源文件";
            chkDeleteSource.UseVisualStyleBackColor = true;
            // 
            // BatchRenameForm
            // 
            ClientSize = new Size(473, 310);
            Controls.Add(lblSourceFolder);
            Controls.Add(txtSourceFolder);
            Controls.Add(btnSelectFolder);
            Controls.Add(lblOutputFolder);
            Controls.Add(txtOutputFolder);
            Controls.Add(btnSelectOutputFolder);
            Controls.Add(lblPrefix);
            Controls.Add(txtPrefix);
            Controls.Add(lblSuffix);
            Controls.Add(txtSuffix);
            Controls.Add(lblStartNumber);
            Controls.Add(numStartNumber);
            Controls.Add(chkDeleteSource);
            Controls.Add(progressBar);
            Controls.Add(btnRename);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "BatchRenameForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "批量重命名";
            ((System.ComponentModel.ISupportInitialize)numStartNumber).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
    }
}