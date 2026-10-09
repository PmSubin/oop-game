namespace OopGame.WinForms;

partial class BuyForm
{
    private System.ComponentModel.IContainer components = null;

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
        lblStockTitle = new Label();
        lstStock = new ListBox();
        lblIngredient = new Label();
        cboIngredient = new ComboBox();
        lblAmount = new Label();
        numAmount = new NumericUpDown();
        lblPrice = new Label();
        lblStock = new Label();
        lblMoney = new Label();
        btnBuyNow = new Button();
        btnClose = new Button();
        ((System.ComponentModel.ISupportInitialize)numAmount).BeginInit();
        SuspendLayout();
        //
        // lblStockTitle
        //
        lblStockTitle.AutoSize = true;
        lblStockTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblStockTitle.Location = new Point(20, 14);
        lblStockTitle.Name = "lblStockTitle";
        lblStockTitle.Size = new Size(200, 28);
        lblStockTitle.TabIndex = 0;
        lblStockTitle.Text = "Kho hiện có (bấm để chọn)";
        //
        // lstStock
        //
        lstStock.Location = new Point(20, 46);
        lstStock.Name = "lstStock";
        lstStock.Size = new Size(320, 338);
        lstStock.TabIndex = 1;
        lstStock.DrawItem += lstStock_DrawItem;
        lstStock.SelectedIndexChanged += lstStock_SelectedIndexChanged;
        //
        // lblIngredient
        //
        lblIngredient.AutoSize = true;
        lblIngredient.Font = new Font("Segoe UI", 10F);
        lblIngredient.Location = new Point(365, 46);
        lblIngredient.Name = "lblIngredient";
        lblIngredient.Size = new Size(85, 23);
        lblIngredient.TabIndex = 2;
        lblIngredient.Text = "Nguyên liệu";
        //
        // cboIngredient
        //
        cboIngredient.DropDownStyle = ComboBoxStyle.DropDownList;
        cboIngredient.FlatStyle = FlatStyle.Flat;
        cboIngredient.Font = new Font("Segoe UI", 10F);
        cboIngredient.Location = new Point(365, 70);
        cboIngredient.Name = "cboIngredient";
        cboIngredient.Size = new Size(215, 31);
        cboIngredient.TabIndex = 3;
        cboIngredient.SelectedIndexChanged += OnSelectionChanged;
        //
        // lblAmount
        //
        lblAmount.AutoSize = true;
        lblAmount.Font = new Font("Segoe UI", 10F);
        lblAmount.Location = new Point(365, 114);
        lblAmount.Name = "lblAmount";
        lblAmount.Size = new Size(69, 23);
        lblAmount.TabIndex = 4;
        lblAmount.Text = "Số lượng";
        //
        // numAmount
        //
        numAmount.Font = new Font("Segoe UI", 11F);
        numAmount.Location = new Point(365, 138);
        numAmount.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
        numAmount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numAmount.Name = "numAmount";
        numAmount.Size = new Size(110, 32);
        numAmount.TabIndex = 5;
        numAmount.Value = new decimal(new int[] { 1, 0, 0, 0 });
        numAmount.ValueChanged += OnSelectionChanged;
        //
        // lblPrice
        //
        lblPrice.AutoSize = true;
        lblPrice.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblPrice.Location = new Point(365, 190);
        lblPrice.Name = "lblPrice";
        lblPrice.Size = new Size(110, 28);
        lblPrice.TabIndex = 6;
        lblPrice.Text = "Tổng giá: 0đ";
        //
        // lblStock
        //
        lblStock.AutoSize = true;
        lblStock.Font = new Font("Segoe UI", 10F);
        lblStock.Location = new Point(365, 224);
        lblStock.Name = "lblStock";
        lblStock.Size = new Size(100, 23);
        lblStock.TabIndex = 7;
        lblStock.Text = "Trong kho: 0";
        //
        // lblMoney
        //
        lblMoney.AutoSize = true;
        lblMoney.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblMoney.Location = new Point(365, 252);
        lblMoney.Name = "lblMoney";
        lblMoney.Size = new Size(130, 23);
        lblMoney.TabIndex = 8;
        lblMoney.Text = "Tiền hiện có: 0đ";
        //
        // btnBuyNow
        //
        btnBuyNow.Location = new Point(365, 296);
        btnBuyNow.Name = "btnBuyNow";
        btnBuyNow.Size = new Size(215, 46);
        btnBuyNow.TabIndex = 9;
        btnBuyNow.Text = "Mua";
        btnBuyNow.Click += btnBuyNow_Click;
        //
        // btnClose
        //
        btnClose.Location = new Point(365, 348);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(215, 36);
        btnClose.TabIndex = 10;
        btnClose.Text = "Đóng";
        btnClose.Click += btnClose_Click;
        //
        // BuyForm
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(600, 404);
        Controls.Add(lblStockTitle);
        Controls.Add(lstStock);
        Controls.Add(lblIngredient);
        Controls.Add(cboIngredient);
        Controls.Add(lblAmount);
        Controls.Add(numAmount);
        Controls.Add(lblPrice);
        Controls.Add(lblStock);
        Controls.Add(lblMoney);
        Controls.Add(btnBuyNow);
        Controls.Add(btnClose);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "BuyForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Nhập hàng";
        ((System.ComponentModel.ISupportInitialize)numAmount).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblStockTitle;
    private ListBox lstStock;
    private Label lblIngredient;
    private ComboBox cboIngredient;
    private Label lblAmount;
    private NumericUpDown numAmount;
    private Label lblPrice;
    private Label lblStock;
    private Label lblMoney;
    private Button btnBuyNow;
    private Button btnClose;
}
