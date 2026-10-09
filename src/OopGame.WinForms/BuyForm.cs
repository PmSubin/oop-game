using System.Globalization;
using OopGame.Core.Game;

namespace OopGame.WinForms;

// Form nhap hang: nhin ca kho mot luot, chon nguyen lieu, so luong roi mua tu nha cung cap
public partial class BuyForm : Form
{
    private static readonly CultureInfo _vietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");

    private readonly Restaurant _restaurant;

    // tranh vong lap: chon trong danh sach kho doi hop chon, va nguoc lai
    private bool _syncing;

    // tao form nhap hang cho quan dang choi
    public BuyForm(Restaurant restaurant)
    {
        InitializeComponent();
        ApplyTheme();
        _restaurant = restaurant;

        foreach (string ingredient in _restaurant.Supplier.GetAvailableIngredients())
        {
            cboIngredient.Items.Add(ingredient);
            lstStock.Items.Add(ingredient);
        }
        if (cboIngredient.Items.Count > 0)
        {
            cboIngredient.SelectedIndex = 0;
        }
        RefreshLabels();
    }

    private void ApplyTheme()
    {
        BackColor = GameTheme.Background;
        ForeColor = GameTheme.Text;
        lblStockTitle.ForeColor = GameTheme.Text;
        lblIngredient.ForeColor = GameTheme.TextMuted;
        lblAmount.ForeColor = GameTheme.TextMuted;
        lblPrice.ForeColor = GameTheme.Yellow;
        lblStock.ForeColor = GameTheme.Text;
        lblMoney.ForeColor = GameTheme.Text;

        cboIngredient.BackColor = GameTheme.PanelLight;
        cboIngredient.ForeColor = GameTheme.Text;
        numAmount.BackColor = GameTheme.PanelLight;
        numAmount.ForeColor = GameTheme.Text;
        numAmount.BorderStyle = BorderStyle.None;

        GameTheme.StyleList(lstStock, 26);
        GameTheme.StyleButton(btnBuyNow, GameTheme.Yellow);
        GameTheme.StyleButton(btnClose, GameTheme.PanelLight);
        btnClose.ForeColor = GameTheme.Text;
    }

    private void RefreshLabels()
    {
        lblMoney.Text = "Tiền hiện có: " + FormatMoney(_restaurant.Money);
        lstStock.Invalidate();

        string? ingredient = cboIngredient.SelectedItem as string;
        if (ingredient == null)
        {
            lblPrice.Text = "Tổng giá: 0đ";
            lblStock.Text = "Trong kho: 0";
            return;
        }

        int amount = (int)numAmount.Value;
        int totalPrice = _restaurant.Supplier.GetTotalPrice(ingredient, amount);
        lblPrice.Text = "Tổng giá: " + FormatMoney(totalPrice);
        lblPrice.ForeColor = totalPrice > _restaurant.Money ? GameTheme.Red : GameTheme.Yellow;
        lblStock.Text = "Trong kho: " + _restaurant.Inventory.GetAmount(ingredient) + " phần";
    }

    private void lstStock_DrawItem(object? sender, DrawItemEventArgs e)
    {
        if (e.Index < 0)
        {
            return;
        }
        GameTheme.DrawBuffered(e, lstStock.Items[e.Index], PaintStockItem);
    }

    private void PaintStockItem(Graphics g, Rectangle b, bool selected, object item)
    {
        GameTheme.DrawItemBackground(g, b, selected);

        string ingredient = item.ToString() ?? "";
        int amount = _restaurant.Inventory.GetAmount(ingredient);
        int price = _restaurant.Supplier.GetPrice(ingredient);

        // kho het thi to do, con it thi vang, de nguoi choi biet nen mua gi
        Color amountColor = GameTheme.Green;
        if (amount == 0)
        {
            amountColor = GameTheme.Red;
        }
        else if (amount <= 2)
        {
            amountColor = GameTheme.Yellow;
        }

        GameTheme.DrawText(g, ingredient, GameTheme.BodyBold, new Rectangle(b.X + 10, b.Y, 120, b.Height), GameTheme.Text);
        GameTheme.DrawText(g, "kho: " + amount, GameTheme.BodyBold, new Rectangle(b.X + 135, b.Y, 70, b.Height), amountColor);
        GameTheme.DrawText(g, FormatMoney(price) + "/phần", GameTheme.Small, new Rectangle(b.X + 205, b.Y, b.Width - 215, b.Height), GameTheme.TextMuted, true);
    }

    private void lstStock_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_syncing || lstStock.SelectedIndex < 0)
        {
            return;
        }
        _syncing = true;
        cboIngredient.SelectedIndex = lstStock.SelectedIndex;
        _syncing = false;
        RefreshLabels();
    }

    private void OnSelectionChanged(object? sender, EventArgs e)
    {
        if (!_syncing && cboIngredient.SelectedIndex >= 0 && lstStock.SelectedIndex != cboIngredient.SelectedIndex)
        {
            _syncing = true;
            lstStock.SelectedIndex = cboIngredient.SelectedIndex;
            _syncing = false;
        }
        RefreshLabels();
    }

    private void btnBuyNow_Click(object? sender, EventArgs e)
    {
        string? ingredient = cboIngredient.SelectedItem as string;
        if (ingredient == null)
        {
            return;
        }

        int amount = (int)numAmount.Value;
        int totalPrice = _restaurant.Supplier.GetTotalPrice(ingredient, amount);
        bool bought = _restaurant.BuyIngredient(ingredient, amount);
        if (bought)
        {
            RefreshLabels();
        }
        else if (totalPrice > _restaurant.Money)
        {
            MessageBox.Show(this, "Không đủ tiền: cần " + FormatMoney(totalPrice) + ", đang có " + FormatMoney(_restaurant.Money) + ".", "Nhập hàng");
        }
        else
        {
            MessageBox.Show(this, "Không mua được, số lượng không hợp lệ.", "Nhập hàng");
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
