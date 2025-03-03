using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace ImageBatchTools
{
    partial class MainMenu
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnBatchRename;
        private Button btnDpiModify;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnBatchRename = new Button();
            btnDpiModify = new Button();

            SuspendLayout();

            // 设置按钮属性
            btnBatchRename.Font = new Font("Microsoft YaHei UI", 12F);
            btnBatchRename.Location = new Point(100, 120);
            btnBatchRename.Name = "btnBatchRename";
            btnBatchRename.Size = new Size(300, 60);
            btnBatchRename.TabIndex = 1;
            btnBatchRename.Text = "批量重命名文件";

            btnDpiModify.Font = new Font("Microsoft YaHei UI", 12F);
            btnDpiModify.Location = new Point(100, 220);
            btnDpiModify.Name = "btnDpiModify";
            btnDpiModify.Size = new Size(300, 60);
            btnDpiModify.TabIndex = 2;
            btnDpiModify.Text = "批量修改图片DPI";

            // 设置窗体属性
            this.AutoScaleDimensions = new SizeF(7F, 17F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(500, 350);
            this.Controls.AddRange(new Control[] { btnBatchRename, btnDpiModify });
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainMenu";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "文件批处理工具";

            ResumeLayout(false);
        }
    }
}