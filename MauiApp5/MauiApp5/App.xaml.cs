namespace MauiApp5;

public partial class App : Application
{
    public App()
    {
        //Register Syncfusion license
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTMwMzM4NUAzMjMwMmUzNDJlMzBTcUQ5azNIVXZiVjlxN09WbHZkOTBWRTM5L0M5QmZHTHpKMXZCQWowV0tNPQ==");
        InitializeComponent();

        MainPage = new AppShell();
    }
}