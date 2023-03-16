

using CommunityToolkit.Maui.Converters;
using MauiApp5.Helper;
using MauiApp5.Models;
using Syncfusion.Maui.DataSource;
using Syncfusion.Maui.ListView;

namespace MauiApp5;

public partial class MainPage : ContentPage
{
    
    public MainPage(MainPageModel model)
    {
        InitializeComponent(); 
        BindingContext = model;
        Routing.RegisterRoute(nameof(AddRecipePage), typeof(AddRecipePage));
        Routing.RegisterRoute(nameof(OpenRecipePage), typeof(OpenRecipePage));

        RecipeList.Loaded += ListView_Loaded;
    }

    private void ListView_Loaded(object sender, ListViewLoadedEventArgs e)
    {
        RecipeList.DataSource.GroupDescriptors.Add(new GroupDescriptor()
        {
            PropertyName = "Categoryid",
        });
        
        RecipeList.GroupHeaderTemplate = new DataTemplate(() =>
        {
            var grid = new Grid();

            var label1 = new Label
            {
                FontSize = 20, FontFamily="Roboto-Medium", TextColor=Colors.Black,
                VerticalOptions = LayoutOptions.Center, CharacterSpacing=0.15,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 0, 0),
                
            };
            //label1.SetBinding(Label.TextProperty, new Binding("Key"));;
            Binding binding = new Binding("Key");
            binding.Converter = new CategoryConverter();
            label1.SetBinding(Label.TextProperty,binding);
            grid.Children.Add(label1);
            return grid;
            //all styling for this grouped header must me defined here
        });
        
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        if (BindingContext is MainPageModel model)
        {
            await model.NavigateToCommand.ExecuteAsync(null);
        }
    }
    
}