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
        pnlHud = new Panel();
        lblDay = new Label();
        lblClock = new Label();
        lblMoney = new Label();
        lblReputation = new Label();
        pnlReputationBar = new Panel();
        lblSpeed = new Label();
        cboSpeed = new ComboBox();
        btnHelp = new Button();
        lblCustomersTitle = new Label();
        lstCustomers = new ListBox();
        lblMenuTitle = new Label();
        lstMenu = new ListBox();
        lblSlogan = new Label();
        lblReadyTitle = new Label();
        lstReady = new ListBox();
        lblLogTitle = new Label();
        lstLog = new ListBox();
        btnOpen = new Button();
        btnCook = new Button();
        btnServe = new Button();
        btnBuy = new Button();
        pnlHud.SuspendLayout();
        SuspendLayout();
        //
        // pnlHud
        //
        pnlHud.Location = new Point(0, 0);
        pnlHud.Name = "pnlHud";
        pnlHud.Size = new Size(1280, 76);
        pnlHud.TabIndex = 0;
        pnlHud.Controls.Add(lblDay);
        pnlHud.Controls.Add(lblClock);
        pnlHud.Controls.Add(lblMoney);
        pnlHud.Controls.Add(lblReputation);
        pnlHud.Controls.Add(pnlReputationBar);
        pnlHud.Controls.Add(lblSpeed);
        pnlHud.Controls.Add(cboSpeed);
        pnlHud.Controls.Add(btnHelp);
        //
        // lblDay
        //
        lblDay.AutoSize = true;
        lblDay.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
        lblDay.Location = new Point(20, 22);
        lblDay.Name = "lblDay";
        lblDay.Size = new Size(90, 35);
        lblDay.TabIndex = 0;
        lblDay.Text = "Ngày 0";
        //
        // lblClock
        //
        lblClock.AutoSize = true;
        lblClock.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblClock.Location = new Point(165, 12);
        lblClock.Name = "lblClock";
        lblClock.Size = new Size(100, 50);
        lblClock.TabIndex = 1;
        lblClock.Text = "08:00";
        //
        // lblMoney
        //
        lblMoney.AutoSize = true;
        lblMoney.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblMoney.Location = new Point(330, 24);
        lblMoney.Name = "lblMoney";
        lblMoney.Size = new Size(150, 32);
        lblMoney.TabIndex = 2;
        lblMoney.Text = "Tiền: 0đ";
        //
        // lblReputation
        //
        lblReputation.AutoSize = true;
        lblReputation.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblReputation.Location = new Point(640, 14);
        lblReputation.Name = "lblReputation";
        lblReputation.Size = new Size(120, 23);
        lblReputation.TabIndex = 3;
        lblReputation.Text = "Uy tín: 100/100";
        //
        // pnlReputationBar
        //
        pnlReputationBar.Location = new Point(640, 44);
        pnlReputationBar.Name = "pnlReputationBar";
        pnlReputationBar.Size = new Size(240, 14);
        pnlReputationBar.TabIndex = 4;
        pnlReputationBar.Paint += pnlReputationBar_Paint;
        //
        // lblSpeed
        //
        lblSpeed.AutoSize = true;
        lblSpeed.Font = new Font("Segoe UI", 10F);
        lblSpeed.Location = new Point(960, 27);
        lblSpeed.Name = "lblSpeed";
        lblSpeed.Size = new Size(60, 23);
        lblSpeed.TabIndex = 5;
        lblSpeed.Text = "Tốc độ";
        //
        // cboSpeed
        //
        cboSpeed.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSpeed.FlatStyle = FlatStyle.Flat;
        cboSpeed.Font = new Font("Segoe UI", 10F);
        cboSpeed.Location = new Point(1022, 23);
        cboSpeed.Name = "cboSpeed";
        cboSpeed.Size = new Size(188, 31);
        cboSpeed.TabIndex = 6;
        cboSpeed.SelectedIndexChanged += cboSpeed_SelectedIndexChanged;
        //
        // btnHelp
        //
        btnHelp.Location = new Point(1220, 19);
        btnHelp.Name = "btnHelp";
        btnHelp.Size = new Size(40, 38);
        btnHelp.TabIndex = 7;
        btnHelp.Text = "?";
        btnHelp.Click += btnHelp_Click;
        //
        // lblCustomersTitle
        //
        lblCustomersTitle.AutoSize = true;
        lblCustomersTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblCustomersTitle.Location = new Point(20, 90);
        lblCustomersTitle.Name = "lblCustomersTitle";
        lblCustomersTitle.Size = new Size(150, 28);
        lblCustomersTitle.TabIndex = 1;
        lblCustomersTitle.Text = "Khách đang chờ";
        //
        // lstCustomers
        //
        lstCustomers.Location = new Point(20, 120);
        lstCustomers.Name = "lstCustomers";
        lstCustomers.Size = new Size(440, 400);
        lstCustomers.TabIndex = 2;
        lstCustomers.DrawItem += lstCustomers_DrawItem;
        lstCustomers.SelectedIndexChanged += lstCustomers_SelectedIndexChanged;
        //
        // lblMenuTitle
        //
        lblMenuTitle.AutoSize = true;
        lblMenuTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblMenuTitle.Location = new Point(480, 90);
        lblMenuTitle.Name = "lblMenuTitle";
        lblMenuTitle.Size = new Size(90, 28);
        lblMenuTitle.TabIndex = 3;
        lblMenuTitle.Text = "Thực đơn";
        //
        // lstMenu
        //
        lstMenu.Location = new Point(480, 120);
        lstMenu.Name = "lstMenu";
        lstMenu.Size = new Size(420, 294);
        lstMenu.TabIndex = 4;
        lstMenu.DrawItem += lstMenu_DrawItem;
        lstMenu.SelectedIndexChanged += lstMenu_SelectedIndexChanged;
        //
        // lblSlogan
        //
        lblSlogan.AutoSize = false;
        lblSlogan.AutoEllipsis = true;
        lblSlogan.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
        lblSlogan.Location = new Point(480, 415);
        lblSlogan.Name = "lblSlogan";
        lblSlogan.Size = new Size(420, 24);
        lblSlogan.TabIndex = 5;
        lblSlogan.Text = "Chọn một món để xem câu slogan của nó.";
        //
        // lblReadyTitle
        //
        lblReadyTitle.AutoSize = true;
        lblReadyTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblReadyTitle.Location = new Point(920, 90);
        lblReadyTitle.Name = "lblReadyTitle";
        lblReadyTitle.Size = new Size(50, 28);
        lblReadyTitle.TabIndex = 6;
        lblReadyTitle.Text = "Bếp";
        //
        // lstReady
        //
        lstReady.Location = new Point(920, 120);
        lstReady.Name = "lstReady";
        lstReady.Size = new Size(340, 400);
        lstReady.TabIndex = 7;
        lstReady.DrawItem += lstReady_DrawItem;
        lstReady.SelectedIndexChanged += lstReady_SelectedIndexChanged;
        //
        // lblLogTitle
        //
        lblLogTitle.AutoSize = true;
        lblLogTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblLogTitle.Location = new Point(20, 532);
        lblLogTitle.Name = "lblLogTitle";
        lblLogTitle.Size = new Size(80, 28);
        lblLogTitle.TabIndex = 8;
        lblLogTitle.Text = "Nhật ký";
        //
        // lstLog
        //
        lstLog.Location = new Point(20, 562);
        lstLog.Name = "lstLog";
        lstLog.Size = new Size(1240, 130);
        lstLog.TabIndex = 9;
        lstLog.DrawItem += lstLog_DrawItem;
        //
        // btnOpen
        //
        btnOpen.Location = new Point(20, 712);
        btnOpen.Name = "btnOpen";
        btnOpen.Size = new Size(295, 60);
        btnOpen.TabIndex = 10;
        btnOpen.Text = "Mở cửa";
        btnOpen.Click += btnOpen_Click;
        //
        // btnCook
        //
        btnCook.Location = new Point(335, 712);
        btnCook.Name = "btnCook";
        btnCook.Size = new Size(295, 60);
        btnCook.TabIndex = 11;
        btnCook.Text = "Nấu món";
        btnCook.Click += btnCook_Click;
        //
        // btnServe
        //
        btnServe.Location = new Point(650, 712);
        btnServe.Name = "btnServe";
        btnServe.Size = new Size(295, 60);
        btnServe.TabIndex = 12;
        btnServe.Text = "Phục vụ";
        btnServe.Click += btnServe_Click;
        //
        // btnBuy
        //
        btnBuy.Location = new Point(965, 712);
        btnBuy.Name = "btnBuy";
        btnBuy.Size = new Size(295, 60);
        btnBuy.TabIndex = 13;
        btnBuy.Text = "Nhập hàng";
        btnBuy.Click += btnBuy_Click;
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1280, 792);
        Controls.Add(pnlHud);
        Controls.Add(lblCustomersTitle);
        Controls.Add(lstCustomers);
        Controls.Add(lblMenuTitle);
        Controls.Add(lstMenu);
        Controls.Add(lblSlogan);
        Controls.Add(lblReadyTitle);
        Controls.Add(lstReady);
        Controls.Add(lblLogTitle);
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
        pnlHud.ResumeLayout(false);
        pnlHud.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Panel pnlHud;
    private Label lblDay;
    private Label lblClock;
    private Label lblMoney;
    private Label lblReputation;
    private Panel pnlReputationBar;
    private Label lblSpeed;
    private ComboBox cboSpeed;
    private Button btnHelp;
    private Label lblCustomersTitle;
    private ListBox lstCustomers;
    private Label lblMenuTitle;
    private ListBox lstMenu;
    private Label lblSlogan;
    private Label lblReadyTitle;
    private ListBox lstReady;
    private Label lblLogTitle;
    private ListBox lstLog;
    private Button btnOpen;
    private Button btnCook;
    private Button btnServe;
    private Button btnBuy;
}
