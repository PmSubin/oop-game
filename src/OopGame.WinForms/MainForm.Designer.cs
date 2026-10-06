namespace OopGame.WinForms;

partial class MainForm
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
        lblMoney = new Label();
        lblReputation = new Label();
        lblDay = new Label();
        lblClock = new Label();
        lblCustomersTitle = new Label();
        lstCustomers = new ListBox();
        lblMenuTitle = new Label();
        lstMenu = new ListBox();
        lblReadyTitle = new Label();
        lstReady = new ListBox();
        lstLog = new ListBox();
        btnOpen = new Button();
        btnCook = new Button();
        btnServe = new Button();
        btnBuy = new Button();
        SuspendLayout();
        //
        // lblMoney
        //
        lblMoney.AutoSize = true;
        lblMoney.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblMoney.Location = new Point(20, 15);
        lblMoney.Name = "lblMoney";
        lblMoney.Size = new Size(90, 25);
        lblMoney.TabIndex = 0;
        lblMoney.Text = "Tiền: 0đ";
        //
        // lblReputation
        //
        lblReputation.AutoSize = true;
        lblReputation.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblReputation.Location = new Point(300, 15);
        lblReputation.Name = "lblReputation";
        lblReputation.Size = new Size(120, 25);
        lblReputation.TabIndex = 1;
        lblReputation.Text = "Uy tín: 100";
        //
        // lblDay
        //
        lblDay.AutoSize = true;
        lblDay.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblDay.Location = new Point(580, 15);
        lblDay.Name = "lblDay";
        lblDay.Size = new Size(70, 25);
        lblDay.TabIndex = 2;
        lblDay.Text = "Ngày 1";
        //
        // lblClock
        //
        lblClock.AutoSize = true;
        lblClock.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblClock.Location = new Point(860, 15);
        lblClock.Name = "lblClock";
        lblClock.Size = new Size(60, 25);
        lblClock.TabIndex = 3;
        lblClock.Text = "08:00";
        //
        // lblCustomersTitle
        //
        lblCustomersTitle.AutoSize = true;
        lblCustomersTitle.Location = new Point(20, 52);
        lblCustomersTitle.Name = "lblCustomersTitle";
        lblCustomersTitle.Size = new Size(110, 20);
        lblCustomersTitle.TabIndex = 4;
        lblCustomersTitle.Text = "Khách đang chờ";
        //
        // lstCustomers
        //
        lstCustomers.FormattingEnabled = true;
        lstCustomers.Location = new Point(20, 75);
        lstCustomers.Name = "lstCustomers";
        lstCustomers.Size = new Size(420, 224);
        lstCustomers.HorizontalScrollbar = true;
        lstCustomers.TabIndex = 5;
        //
        // lblMenuTitle
        //
        lblMenuTitle.AutoSize = true;
        lblMenuTitle.Location = new Point(455, 52);
        lblMenuTitle.Name = "lblMenuTitle";
        lblMenuTitle.Size = new Size(70, 20);
        lblMenuTitle.TabIndex = 6;
        lblMenuTitle.Text = "Thực đơn";
        //
        // lstMenu
        //
        lstMenu.FormattingEnabled = true;
        lstMenu.Location = new Point(455, 75);
        lstMenu.Name = "lstMenu";
        lstMenu.Size = new Size(330, 224);
        lstMenu.HorizontalScrollbar = true;
        lstMenu.TabIndex = 7;
        lstMenu.SelectedIndexChanged += lstMenu_SelectedIndexChanged;
        //
        // lblReadyTitle
        //
        lblReadyTitle.AutoSize = true;
        lblReadyTitle.Location = new Point(800, 52);
        lblReadyTitle.Name = "lblReadyTitle";
        lblReadyTitle.Size = new Size(120, 20);
        lblReadyTitle.TabIndex = 8;
        lblReadyTitle.Text = "Bếp";
        //
        // lstReady
        //
        lstReady.FormattingEnabled = true;
        lstReady.Location = new Point(800, 75);
        lstReady.Name = "lstReady";
        lstReady.Size = new Size(280, 224);
        lstReady.HorizontalScrollbar = true;
        lstReady.TabIndex = 9;
        //
        // lstLog
        //
        lstLog.FormattingEnabled = true;
        lstLog.Location = new Point(20, 315);
        lstLog.Name = "lstLog";
        lstLog.Size = new Size(1060, 144);
        lstLog.HorizontalScrollbar = true;
        lstLog.TabIndex = 10;
        //
        // btnOpen
        //
        btnOpen.Location = new Point(20, 475);
        btnOpen.Name = "btnOpen";
        btnOpen.Size = new Size(250, 50);
        btnOpen.TabIndex = 11;
        btnOpen.Text = "Mở cửa";
        btnOpen.UseVisualStyleBackColor = true;
        btnOpen.Click += btnOpen_Click;
        //
        // btnCook
        //
        btnCook.Location = new Point(290, 475);
        btnCook.Name = "btnCook";
        btnCook.Size = new Size(250, 50);
        btnCook.TabIndex = 12;
        btnCook.Text = "Nấu món";
        btnCook.UseVisualStyleBackColor = true;
        btnCook.Click += btnCook_Click;
        //
        // btnServe
        //
        btnServe.Location = new Point(560, 475);
        btnServe.Name = "btnServe";
        btnServe.Size = new Size(250, 50);
        btnServe.TabIndex = 13;
        btnServe.Text = "Phục vụ";
        btnServe.UseVisualStyleBackColor = true;
        btnServe.Click += btnServe_Click;
        //
        // btnBuy
        //
        btnBuy.Location = new Point(830, 475);
        btnBuy.Name = "btnBuy";
        btnBuy.Size = new Size(250, 50);
        btnBuy.TabIndex = 14;
        btnBuy.Text = "Nhập hàng";
        btnBuy.UseVisualStyleBackColor = true;
        btnBuy.Click += btnBuy_Click;
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 545);
        Controls.Add(lblMoney);
        Controls.Add(lblReputation);
        Controls.Add(lblDay);
        Controls.Add(lblClock);
        Controls.Add(lblCustomersTitle);
        Controls.Add(lstCustomers);
        Controls.Add(lblMenuTitle);
        Controls.Add(lstMenu);
        Controls.Add(lblReadyTitle);
        Controls.Add(lstReady);
        Controls.Add(lstLog);
        Controls.Add(btnOpen);
        Controls.Add(btnCook);
        Controls.Add(btnServe);
        Controls.Add(btnBuy);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quán Ăn Bận Rộn";
        FormClosed += MainForm_FormClosed;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblMoney;
    private Label lblReputation;
    private Label lblDay;
    private Label lblClock;
    private Label lblCustomersTitle;
    private ListBox lstCustomers;
    private Label lblMenuTitle;
    private ListBox lstMenu;
    private Label lblReadyTitle;
    private ListBox lstReady;
    private ListBox lstLog;
    private Button btnOpen;
    private Button btnCook;
    private Button btnServe;
    private Button btnBuy;
}
