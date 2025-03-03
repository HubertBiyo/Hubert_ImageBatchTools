partial class MainMenu
{
    private Button btnBatchRename;
    private Button btnDpiModify;
    private Label lblTitle;

    private void InitializeComponent()
    {
        this.btnBatchRename = new Button();
        this.btnDpiModify = new Button();
        this.lblTitle = new Label();

        // 标题
        this.lblTitle.Text = "文件批处理工具";
        this.lblTitle.Font = new Font(this.Font.FontFamily, 20, FontStyle.Bold);
        this.lblTitle.AutoSize = true;
        this.lblTitle.Location = new Point(150, 40);

        // 批量重命名文件按钮
        this.btnBatchRename.Text = "批量重命名文件";
        this.btnBatchRename.Size = new Size(300, 60);
        this.btnBatchRename.Location = new Point(100, 120);
        this.btnBatchRename.Font = new Font(this.Font.FontFamily, 12);
        this.btnBatchRename.Click += new EventHandler(btnBatchRename_Click);

        // DPI修改按钮
        this.btnDpiModify.Text = "批量修改图片DPI";
        this.btnDpiModify.Size = new Size(300, 60);
        this.btnDpiModify.Location = new Point(100, 220);
        this.btnDpiModify.Font = new Font(this.Font.FontFamily, 12);
        this.btnDpiModify.Click += new EventHandler(btnDpiModify_Click);

        // 窗体设置
        this.ClientSize = new Size(500, 350);
        this.Controls.AddRange(new Control[] {
            this.lblTitle,
            this.btnBatchRename,
            this.btnDpiModify
        });
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.MaximizeBox = false;
        this.Text = "文件批处理工具";
    }
}