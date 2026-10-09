namespace OopGame.WinForms;

partial class HelpForm
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
        rtbHelp = new RichTextBox();
        btnClose = new Button();
        SuspendLayout();
        //
        // rtbHelp
        //
        rtbHelp.BorderStyle = BorderStyle.None;
        rtbHelp.Location = new Point(20, 20);
        rtbHelp.Name = "rtbHelp";
        rtbHelp.ReadOnly = true;
        rtbHelp.Size = new Size(820, 600);
        rtbHelp.TabIndex = 0;
        rtbHelp.Text = "";
        rtbHelp.WordWrap = true;
        //
        // btnClose
        //
        btnClose.Location = new Point(640, 636);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(200, 40);
        btnClose.TabIndex = 1;
        btnClose.Text = "Đóng";
        btnClose.Click += btnClose_Click;
        //
        // HelpForm
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(860, 696);
        Controls.Add(rtbHelp);
        Controls.Add(btnClose);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "HelpForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Hướng dẫn chơi";
        ResumeLayout(false);
    }

    #endregion

    private RichTextBox rtbHelp;
    private Button btnClose;
}
