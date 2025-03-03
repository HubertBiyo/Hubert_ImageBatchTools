using System;
using System.Windows.Forms;
using BatchFileRenamer;

public partial class MainMenu : Form
{
    public MainMenu()
    {
        InitializeComponent();
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
}