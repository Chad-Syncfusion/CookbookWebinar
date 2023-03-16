using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp5.Helper;
using SkiaSharp;
using SQLite;


namespace MauiApp5.Models;

public partial class AddRecipeModel : ObservableObject, IQueryAttributable
{
    private readonly SQLiteAsyncConnection _connection;
    
    private Recipe Rcp { get; set; }
    
    [ObservableProperty] private string _imgName;
    [ObservableProperty] private string _pdfName;
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _description;
    [ObservableProperty] private Category _category;

    private ImageModel ImgMdl { get; set; }
    [ObservableProperty] private string _path;
    [ObservableProperty] private string _title;

    public AddRecipeModel(SQLiteAsyncConnection conn)
    {
        _connection = conn;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Rcp = (Recipe)query[nameof(Rcp)]; 

        Name = Rcp.Name;
        ImgName = Rcp.ImgName;
        Description = Rcp.Description;
        Category = CategoryConverter.Categories.FirstOrDefault(c => c.Id == Rcp.Categoryid);
    }

    [RelayCommand]
    public async Task Save()
    {
        await _connection.CreateTableAsync<Recipe>();

        Rcp.Name = Name;
        Rcp.Description = Description;
        Rcp.Categoryid = Category.Id;
        if(_path is null) {Rcp.ImgName = "chocolatenirvanacake.jpg";}
        else {Rcp.ImgName = Path;}

        Rcp.PdfName = ConvertImgeToPdf(Rcp.ImgName);
        
        if (Rcp.Id == 0)
        {
            await _connection.InsertAsync(Rcp);
            
        }
        else
        {
            await _connection.UpdateAsync(Rcp);
        }

        await Shell.Current.GoToAsync(".."); //todo what is this??
    }

    private string ConvertImgeToPdf(string filename)
    {
        return filename;

        // Stream? jpgImageStream = assembly.GetManifestResourceStream(filename);
        //
        // //Create image to PDF converter.
        // ImageToPdfConverter imageToPdfConverter = new ImageToPdfConverter();
        //
        // //Set image position.
        // imageToPdfConverter.ImagePosition = PdfImagePosition.FitToPageAndMaintainAspectRatio;
        //
        // //Convert images to PDF file.
        // PdfDocument document = imageToPdfConverter.Convert(imageStreams);


    }
    [RelayCommand]
    private async Task TakePhoto()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                //Use database path in Constants
                string localFilePath = System.IO.Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                //
                using Stream sourceStream = await photo.OpenReadAsync();
                using SKBitmap sourceBitmap=SKBitmap.Decode(sourceStream);
                //  int height = Math.Min(794, sourceBitmap.Height);
                //  int width = Math.Min(794, sourceBitmap.Width);
                //
                // using SKBitmap scaledBitmap = sourceBitmap.Resize(new SKImageInfo(width, height),SKFilterQuality.Medium);
                // using SKImage scaledImage = SKImage.FromBitmap(scaledBitmap);
                //
                //using (SKData data = scaledImage.Encode())
                using (SKData data = sourceBitmap.Encode(SKEncodedImageFormat.Png,100))
                {
                    File.WriteAllBytes(localFilePath, data.ToArray());
                }

                Path = localFilePath;
                Title = "sample" ;
            }
        }
    }
 
}