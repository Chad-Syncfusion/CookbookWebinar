using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp5.Helper;

namespace MauiApp5.Models;

public partial class OpenRecipeModel : ObservableObject, IQueryAttributable
{
    public ObservableCollection<Recipe> Recipes { get; set; } = new();
    private Recipe Rcp { get; set; }
    
    [ObservableProperty] private string _imgName;
    [ObservableProperty] private string _pdfName;
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _description;
    [ObservableProperty] private Category _category;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Rcp = (Recipe)query[nameof(Rcp)]; 

        Name = Rcp.Name;
        ImgName = Rcp.ImgName;
        Description = Rcp.Description;
        Category = CategoryConverter.Categories.FirstOrDefault(c => c.Id == Rcp.Categoryid);
        PdfName = Rcp.PdfName;
    }
    
}