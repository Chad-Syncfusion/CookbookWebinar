
using SkiaSharp;
using MauiApp5.Models;


namespace MauiApp5;

public partial class AddRecipePage : ContentPage
{
    public AddRecipePage(AddRecipeModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}
