using System.Globalization;
using OopGame.Core.Game;

namespace OopGame.WinForms;

/// <summary>
/// Form nhập hàng: chọn nguyên liệu, số lượng rồi mua từ nhà cung cấp.
/// Chỉ gọi phương thức public của Restaurant, không tự tính tiền hay sửa kho.
/// </summary>
public partial class BuyForm : Form
{
    private static readonly CultureInfo _vietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");

    private readonly Restaurant _restaurant;

    /// <summary>
    /// Tạo form nhập hàng cho quán đang chơi.
    /// </summary>
    public BuyForm(Restaurant restaurant)
    {
        InitializeComponent();
        _restaurant = restaurant;

        foreach (string ingredient in _restaurant.Supplier.GetAvailableIngredients())
        {
            cboIngredient.Items.Add(ingredient);
        }
        if (cboIngredient.Items.Count > 0)
        {
            cboIngredient.SelectedIndex = 0;
        }
        RefreshLabels();
    }

    private void RefreshLabels()
    {
        lblMoney.Text = "Tiền hiện có: " + FormatMoney(_restaurant.Money);

        string? ingredient = cboIngredient.SelectedItem as string;
        if (ingredient == null)
        {
            lblPrice.Text = "Tổng giá: 0đ";
            lblStock.Text = "Trong kho: 0";
            return;
        }

        int amount = (int)numAmount.Value;
        lblPrice.Text = "Tổng giá: " + FormatMoney(_restaurant.Supplier.GetTotalPrice(ingredient, amount));
        lblStock.Text = "Trong kho: " + _restaurant.Inventory.GetAmount(ingredient);
    }

    private void OnSelectionChanged(object? sender, EventArgs e)
    {
        RefreshLabels();
    }

    private void btnBuyNow_Click(object? sender, EventArgs e)
    {
        string? ingredient = cboIngredient.SelectedItem as string;
        if (ingredient == null)
        {
            return;
        }

        bool bought = _restaurant.BuyIngredient(ingredient, (int)numAmount.Value);
        if (bought)
        {
            RefreshLabels();
        }
        else
        {
            MessageBox.Show("Không đủ tiền hoặc số lượng không hợp lệ.", "Nhập hàng");
        }
    }

    private void btnClose_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private static string FormatMoney(int amount)
    {
        return amount.ToString("N0", _vietnameseCulture) + "đ";
    }
}
