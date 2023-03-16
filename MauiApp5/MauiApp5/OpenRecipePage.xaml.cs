
using MauiApp5.Models;

namespace MauiApp5;

public partial class OpenRecipePage : ContentPage
{
    public OpenRecipePage(OpenRecipeModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}