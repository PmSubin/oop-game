namespace OopGame.WinForms;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // loi chua bat duoc thi hien ra va ghi file, thay vi de game tat ngang khong ro ly do
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += OnThreadException;
        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

        Application.Run(new MainForm());
    }

    private static void OnThreadException(object? sender, ThreadExceptionEventArgs e)
    {
        ReportCrash(e.Exception);
    }

    private static void OnUnhandledException(object? sender, UnhandledExceptionEventArgs e)
    {
        Exception? exception = e.ExceptionObject as Exception;
        if (exception != null)
        {
            ReportCrash(exception);
        }
    }

    private static void ReportCrash(Exception exception)
    {
        string logPath = Path.Combine(AppContext.BaseDirectory, "crash.log");
        try
        {
            File.AppendAllText(logPath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine + exception + Environment.NewLine + Environment.NewLine);
        }
        catch (Exception)
        {
            // khong ghi duoc file thi van hien hop thoai ben duoi
        }

        MessageBox.Show(
            "Game gặp lỗi:\n\n" + exception.GetType().Name + ": " + exception.Message + "\n\nChi tiết đã ghi vào:\n" + logPath,
            "Quán Ăn Bận Rộn - lỗi",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
}
