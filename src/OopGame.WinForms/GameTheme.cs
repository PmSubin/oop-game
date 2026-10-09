namespace OopGame.WinForms;

// mau sac, font va vai ham ve dung chung cho moi form, de giao dien dong bo mot kieu
internal static class GameTheme
{
    public static readonly Color Background = Color.FromArgb(30, 26, 23);
    public static readonly Color Panel = Color.FromArgb(44, 38, 34);
    public static readonly Color PanelLight = Color.FromArgb(62, 54, 48);
    public static readonly Color Border = Color.FromArgb(84, 73, 65);
    public static readonly Color Text = Color.FromArgb(245, 237, 224);
    public static readonly Color TextMuted = Color.FromArgb(172, 160, 146);
    public static readonly Color Orange = Color.FromArgb(242, 153, 74);
    public static readonly Color Green = Color.FromArgb(111, 207, 151);
    public static readonly Color Red = Color.FromArgb(235, 87, 87);
    public static readonly Color Yellow = Color.FromArgb(242, 201, 76);
    public static readonly Color Blue = Color.FromArgb(100, 170, 230);
    public static readonly Color Purple = Color.FromArgb(190, 140, 250);
    public static readonly Color Selection = Color.FromArgb(96, 74, 52);

    public static readonly Font Title = new Font("Segoe UI", 12F, FontStyle.Bold);
    public static readonly Font Body = new Font("Segoe UI", 10F);
    public static readonly Font BodyBold = new Font("Segoe UI", 10F, FontStyle.Bold);
    public static readonly Font Small = new Font("Segoe UI", 8.5F);
    public static readonly Font SmallBold = new Font("Segoe UI", 8.5F, FontStyle.Bold);
    public static readonly Font ButtonFont = new Font("Segoe UI", 11.5F, FontStyle.Bold);

    // ham ve mot dong cua ListBox len vung bounds bat dau tu (0, 0)
    public delegate void ItemPainter(Graphics g, Rectangle bounds, bool selected, object item);

    // ve mot dong vao anh dem roi dan len man hinh mot lan
    public static void DrawBuffered(DrawItemEventArgs e, object item, ItemPainter painter)
    {
        if (e.Bounds.Width <= 0 || e.Bounds.Height <= 0)
        {
            return;
        }
        bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
        using (Bitmap buffer = new Bitmap(e.Bounds.Width, e.Bounds.Height))
        {
            using (Graphics g = Graphics.FromImage(buffer))
            {
                painter(g, new Rectangle(0, 0, buffer.Width, buffer.Height), selected, item);
            }
            e.Graphics.DrawImageUnscaled(buffer, e.Bounds.Location);
        }
    }

    // nut phang, nen mau, chu toi
    public static void StyleButton(Button button, Color color)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(color, 0.2f);
        button.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(color, 0.1f);
        button.BackColor = color;
        button.ForeColor = Background;
        button.Font = ButtonFont;
        button.Cursor = Cursors.Hand;
        button.Tag = color;
    }

    // nut bi tat thi xam di de nguoi choi biet luc nay khong bam duoc
    public static void SetButtonEnabled(Button button, bool enabled)
    {
        button.Enabled = enabled;
        Color color = button.Tag is Color tagColor ? tagColor : Orange;
        button.BackColor = enabled ? color : PanelLight;
        button.ForeColor = enabled ? Background : TextMuted;
    }

    // ListBox nen toi, tu ve tung dong
    public static void StyleList(ListBox list, int itemHeight)
    {
        list.BackColor = Panel;
        list.ForeColor = Text;
        list.BorderStyle = BorderStyle.None;
        list.DrawMode = DrawMode.OwnerDrawFixed;
        list.ItemHeight = itemHeight;
        list.IntegralHeight = false;
        list.Font = Body;
    }

    // nen cua mot dong trong ListBox: dong dang chon sang hon va co vien cam
    public static void DrawItemBackground(Graphics g, Rectangle bounds, bool selected)
    {
        using (SolidBrush brush = new SolidBrush(selected ? Selection : Panel))
        {
            g.FillRectangle(brush, bounds);
        }
        using (Pen pen = new Pen(Border))
        {
            g.DrawLine(pen, bounds.Left, bounds.Bottom - 1, bounds.Right, bounds.Bottom - 1);
        }
        if (selected)
        {
            using (Pen pen = new Pen(Orange, 2))
            {
                Rectangle r = bounds;
                r.Inflate(-1, -1);
                g.DrawRectangle(pen, r);
            }
        }
    }

    // thanh tien do: nen toi, phan day to mau theo ti le 0 den 1
    public static void DrawBar(Graphics g, Rectangle rect, double ratio, Color color)
    {
        if (ratio < 0)
        {
            ratio = 0;
        }
        if (ratio > 1)
        {
            ratio = 1;
        }
        using (SolidBrush back = new SolidBrush(Color.FromArgb(20, 17, 15)))
        {
            g.FillRectangle(back, rect);
        }
        int width = (int)(rect.Width * ratio);
        if (width > 0)
        {
            using (SolidBrush fill = new SolidBrush(color))
            {
                g.FillRectangle(fill, new Rectangle(rect.X, rect.Y, width, rect.Height));
            }
        }
    }

    // mau thanh kien nhan: xanh khi con nhieu, vang khi con mot nua, do khi sap het
    public static Color BarColor(double ratio)
    {
        if (ratio > 0.5)
        {
            return Green;
        }
        if (ratio > 0.25)
        {
            return Yellow;
        }
        return Red;
    }

    // nhan nho co nen mau (vi du loai khach)
    public static int DrawBadge(Graphics g, string text, int x, int y, Color back, Color fore)
    {
        Size size = TextRenderer.MeasureText(g, text, SmallBold);
        Rectangle rect = new Rectangle(x, y, size.Width + 10, size.Height + 2);
        using (SolidBrush brush = new SolidBrush(back))
        {
            g.FillRectangle(brush, rect);
        }
        TextRenderer.DrawText(g, text, SmallBold, new Point(x + 5, y + 1), fore);
        return rect.Width;
    }

    // chu thuong, khong tu xuong dong, cat bang dau ba cham khi qua dai
    public static void DrawText(Graphics g, string text, Font font, Rectangle rect, Color color, bool alignRight = false)
    {
        if (rect.Width <= 0)
        {
            return;
        }
        TextFormatFlags flags = TextFormatFlags.NoPadding | TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter;
        if (alignRight)
        {
            flags |= TextFormatFlags.Right;
        }
        TextRenderer.DrawText(g, text, font, rect, color, flags);
    }
}
