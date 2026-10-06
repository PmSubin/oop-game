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
        // lblIngredient
        //
        lblIngredient.AutoSize = true;
        lblIngredient.Location = new Point(20, 23);
        lblIngredient.Name = "lblIngredient";
        lblIngredient.Size = new Size(85, 20);
        lblIngredient.TabIndex = 0;
        lblIngredient.Text = "Nguyên liệu";
        //
        // cboIngredient
        //
        cboIngredient.DropDownStyle = ComboBoxStyle.DropDownList;
        cboIngredient.FormattingEnabled = true;
        cboIngredient.Location = new Point(130, 20);
        cboIngredient.Name = "cboIngredient";
        cboIngredient.Size = new Size(250, 28);
        cboIngredient.TabIndex = 1;
        cboIngredient.SelectedIndexChanged += OnSelectionChanged;
        //
        // lblAmount
        //
        lblAmount.AutoSize = true;
        lblAmount.Location = new Point(20, 63);
        lblAmount.Name = "lblAmount";
        lblAmount.Size = new Size(69, 20);
        lblAmount.TabIndex = 2;
        lblAmount.Text = "Số lượng";
        //
        // numAmount
        //
        numAmount.Location = new Point(130, 60);
        numAmount.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
        numAmount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numAmount.Name = "numAmount";
        numAmount.Size = new Size(120, 27);
        numAmount.TabIndex = 3;
        numAmount.Value = new decimal(new int[] { 1, 0, 0, 0 });
        numAmount.ValueChanged += OnSelectionChanged;
        //
        // lblPrice
        //
        lblPrice.AutoSize = true;
        lblPrice.Location = new Point(20, 105);
        lblPrice.Name = "lblPrice";
        lblPrice.Size = new Size(90, 20);
        lblPrice.TabIndex = 4;
        lblPrice.Text = "Tổng giá: 0đ";
        //
        // lblStock
        //
        lblStock.AutoSize = true;
        lblStock.Location = new Point(20, 135);
        lblStock.Name = "lblStock";
        lblStock.Size = new Size(90, 20);
        lblStock.TabIndex = 5;
        lblStock.Text = "Trong kho: 0";
        //
        // lblMoney
        //
        lblMoney.AutoSize = true;
        lblMoney.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblMoney.Location = new Point(20, 165);
        lblMoney.Name = "lblMoney";
        lblMoney.Size = new Size(110, 20);
        lblMoney.TabIndex = 6;
        lblMoney.Text = "Tiền hiện có: 0đ";
        //
        // btnBuyNow
        //
        btnBuyNow.Location = new Point(170, 200);
        btnBuyNow.Name = "btnBuyNow";
        btnBuyNow.Size = new Size(100, 36);
        btnBuyNow.TabIndex = 7;
        btnBuyNow.Text = "Mua";
        btnBuyNow.UseVisualStyleBackColor = true;
        btnBuyNow.Click += btnBuyNow_Click;
        //
        // btnClose
        //
        btnClose.Location = new Point(280, 200);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(100, 36);
        btnClose.TabIndex = 8;
        btnClose.Text = "Đóng";
        btnClose.UseVisualStyleBackColor = true;
        btnClose.Click += btnClose_Click;
        //
        // BuyForm
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(402, 253);
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
