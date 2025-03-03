using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using BatchFileRenamer;
using ImageBatchTools.Utils;
namespace ImageBatchTools
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();

            // 设置窗口属性
            this.Text = "图片工具";
            this.MinimumSize = new Size(1000, 600);
            this.Size = new Size(1000, 600);
            this.BackColor = Color.FromArgb(240, 242, 245);  // 更柔和的背景色

            // 创建面板
            Panel sideMenu = new Panel
            {
                Dock = DockStyle.Left,
                Width = 200,
                BackColor = Color.FromArgb(24, 28, 36)  // 更深邃的侧边栏颜色
            };

            // 创建内容面板
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(236, 239, 241)  // 更柔和的灰色背景
            };

            // 创建子菜单面板
            FlowLayoutPanel fileSubMenu = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(236, 239, 241),  // 匹配内容区背景色
                Padding = new Padding(220, 40, 40, 40),
                AutoScroll = true,
                Visible = true
            };

            FlowLayoutPanel imageSubMenu = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(236, 239, 241),  // 匹配内容区背景色
                Padding = new Padding(220, 40, 40, 40),
                AutoScroll = true,
                Visible = false
            };

            // 设置功能按钮样式
            void SetFunctionButtonStyle(Button btn)
            {
                btn.Size = new Size(160, 160);
                btn.BackColor = Color.FromArgb(51, 122, 183);
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Margin = new Padding(15);
                btn.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular);
                btn.TextAlign = ContentAlignment.MiddleCenter;  // 文字居中

                // 添加阴影效果
                btn.Paint += (s, e) =>
                {
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddRoundedRectangle(new Rectangle(0, 0, btn.Width, btn.Height), 8);
                        using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
                        {
                            e.Graphics.TranslateTransform(2, 2);
                            e.Graphics.FillPath(shadowBrush, path);
                            e.Graphics.TranslateTransform(-2, -2);
                        }
                        e.Graphics.FillPath(new SolidBrush(btn.BackColor), path);

                        // 绘制文字
                        TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, 
                            new Rectangle(0, 0, btn.Width, btn.Height),
                            btn.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    }
                };

                btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(71, 135, 190);
                btn.MouseLeave += (s, e) => btn.BackColor = Color.FromArgb(51, 122, 183);
            }

            // 设置按钮样式
            SetFunctionButtonStyle(btnBatchRename);
            SetFunctionButtonStyle(btnDpiModify);

            // 添加按钮点击事件
            btnBatchRename.Click += btnBatchRename_Click;
            btnDpiModify.Click += btnDpiModify_Click;

            // 添加按钮到面板
            fileSubMenu.Controls.Add(btnBatchRename);
            imageSubMenu.Controls.Add(btnDpiModify);

            // 创建菜单按钮
            Button fileToolsBtn = CreateMenuButton("文件工具", 0);
            Button imageToolsBtn = CreateMenuButton("图片工具", 46);  // 调整间距

            // 添加菜单切换事件
            fileToolsBtn.Click += (s, e) =>
            {
                fileSubMenu.Visible = true;
                imageSubMenu.Visible = false;
                fileToolsBtn.BackColor = Color.FromArgb(64, 158, 255);
                imageToolsBtn.BackColor = Color.FromArgb(48, 65, 86);
            };

            imageToolsBtn.Click += (s, e) =>
            {
                fileSubMenu.Visible = false;
                imageSubMenu.Visible = true;
                imageToolsBtn.BackColor = Color.FromArgb(64, 158, 255);
                fileToolsBtn.BackColor = Color.FromArgb(48, 65, 86);
            };

            // 构建控件层次
            this.Controls.Clear();
            sideMenu.Controls.AddRange(new Control[] { fileToolsBtn, imageToolsBtn });
            contentPanel.Controls.AddRange(new Control[] { fileSubMenu, imageSubMenu });
            this.Controls.AddRange(new Control[] { sideMenu, contentPanel });

            // 设置默认选中状态
            fileToolsBtn.BackColor = Color.FromArgb(64, 158, 255);
        }
        private void btnBatchRename_Click(object sender, EventArgs e)
        {
            try
            {
                using (BatchRenameForm batchRenameForm = new BatchRenameForm())
                {
                    batchRenameForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开窗口时出现错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDpiModify_Click(object sender, EventArgs e)
        {
            try
            {
                using (DpiModifyForm dpiModifyForm = new DpiModifyForm())
                {
                    dpiModifyForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开窗口时出现错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Button CreateMenuButton(string text, int yOffset)
        {
            return new Button
            {
                Text = text,
                Size = new Size(200, 45),  // 调整高度
                Location = new Point(0, yOffset),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(230, 230, 230),  // 更柔和的文字颜色
                Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular),  // 调整字体大小
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0),  // 减小左边距
                BackColor = Color.FromArgb(48, 65, 86),  // 匹配面板背景色
                FlatAppearance = { 
                    BorderSize = 0,
                    MouseOverBackColor = Color.FromArgb(57, 62, 70),  // 调整悬停颜色
                    MouseDownBackColor = Color.FromArgb(64, 158, 255)  // 保持点击颜色不变
                },
                Cursor = Cursors.Hand  // 添加手型光标
            };
        }
    }
}