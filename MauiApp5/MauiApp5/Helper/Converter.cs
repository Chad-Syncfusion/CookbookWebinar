using System.Globalization;
using MauiApp5.Models;

namespace MauiApp5.Helper;

public class CategoryConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Categories.Where(c => c.Id == (int)value).Select(c => c.CatName).FirstOrDefault("missing");
    }

    public static List<Category> Categories { get; } =
        new()
        {
            new Category { CatName = "Appetizers", Id = 1 },
            new Category { CatName = "Breakfast", Id = 2 },
            new Category { CatName = "Lunch", Id = 3 },
            new Category { CatName = "Dinner", Id = 4 },
            new Category { CatName = "Dessert", Id = 5 }
        };
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}