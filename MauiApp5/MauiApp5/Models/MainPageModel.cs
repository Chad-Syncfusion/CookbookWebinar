using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SQLite;

namespace MauiApp5.Models;

public partial class MainPageModel : ObservableObject
{
    private readonly SQLiteAsyncConnection _connection;

    [ObservableProperty] private string _imgName;
    [ObservableProperty] private string _pdfName;
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _description;
    [ObservableProperty] private int _categoryid;
    [ObservableProperty] private int _isSelected;
    [ObservableProperty] private Recipe? _selectedItem;
    

    public ObservableCollection<Recipe> Recipes { get; set; } = new();

    public MainPageModel(SQLiteAsyncConnection Conn)
    {
        _connection = Conn;

    }

    [RelayCommand]
    private async Task OnNavigateTo()
    {
        Recipes.Clear();
        await _connection.CreateTableAsync<Recipe>();
        var items = await _connection.Table<Recipe>().ToListAsync();
        items.ForEach(Recipes.Add);

        //bad data testing for file path
        ParseModelFileNames();
    }

    private void ParseModelFileNames()
    {
        foreach (Recipe mm in Recipes)
        {
            if (mm.ImgName.ToString().Contains("/"))
            {
                //do nothing
            }
            else
            {
                mm.ImgName = "question.png";
            }
        }
    }

    [RelayCommand]
    private async Task OnAddItem()
    {
        await Shell.Current.GoToAsync(nameof(AddRecipePage), new Dictionary<string, object>
        {
            ["Rcp"] = new Recipe()
        });
    }
    [RelayCommand]
    private async Task OnEditItem()
    {
        if (SelectedItem is {})
        {
            await Shell.Current.GoToAsync(nameof(AddRecipePage), new Dictionary<string, object>
            {
                ["Rcp"] = SelectedItem
            });
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Alert", "Please Select an Item", "OK");
        }
    }
    [RelayCommand]
    private async Task OnOpenRecipe()
    {
        if (SelectedItem is { })
        {
            await Shell.Current.GoToAsync(nameof(OpenRecipePage), new Dictionary<string, object>
            {
                ["Rcp"] = SelectedItem
            });
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Alert", "Please Select an Item", "OK");
        }
    }

    [RelayCommand]
    //some comment
    private async Task OnItemTapped(Recipe recipe)
    {
        SelectedItem = recipe;
        
    }

}