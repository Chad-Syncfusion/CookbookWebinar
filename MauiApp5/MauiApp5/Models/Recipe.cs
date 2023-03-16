using SQLite;

namespace MauiApp5.Models;

public  class Recipe
{
    [PrimaryKey,AutoIncrement]
    public int Id { get; set; }

    public string ImgName { get; set; }
    public string PdfName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Categoryid { get; set; }
    
}