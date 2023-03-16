using CommunityToolkit.Maui;
using MauiApp5.Models;
using SQLite;
using Syncfusion.Maui.Core.Hosting;

namespace MauiApp5;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiCommunityToolkit()
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddSingleton(_ => 
            new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags));
        
        builder.Services.AddSingleton<MainPageModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<AddRecipeModel>();
        builder.Services.AddSingleton<AddRecipePage>();
        builder.Services.AddSingleton<OpenRecipePage>();
        builder.Services.AddSingleton<OpenRecipeModel>();
        
        builder.ConfigureSyncfusionCore();

        return builder.Build();
    }
}