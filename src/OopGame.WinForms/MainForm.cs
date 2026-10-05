namespace OopGame.WinForms;

/// <summary>
/// Màn hình chính của quán. Hiện tại các nút chỉ ghi log,
/// phần nối với logic quán là task 4 trong TASKS.md.
/// </summary>
public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        AddLog("Chào mừng đến Quán Ăn Bận Rộn! Các nút chưa có logic, xem TASKS.md.");
    }

    private void AddLog(string message)
    {
        lstLog.Items.Add(message);
        lstLog.TopIndex = lstLog.Items.Count - 1;
    }

    private void btnOpen_Click(object? sender, EventArgs e)
    {
        AddLog("Bạn bấm: Mở cửa");
    }

    private void btnCook_Click(object? sender, EventArgs e)
    {
        AddLog("Bạn bấm: Nấu món");
    }

    private void btnServe_Click(object? sender, EventArgs e)
    {
        AddLog("Bạn bấm: Phục vụ");
    }

    private void btnBuy_Click(object? sender, EventArgs e)
    {
        AddLog("Bạn bấm: Nhập hàng");
    }
}
