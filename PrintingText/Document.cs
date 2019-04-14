using Constructor.View;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PrintingText
{
    public class Document
    {
        public Grid Place(Page page,TableView TemplateArea)
        {          
            Grid place = new Grid
            {
                Margin = new Thickness(page.Left, page.Top, page.Right, page.Bottom),
                Width = page.Width,
                Height = page.Height
            };
            var bitmap = CreatePngFromTemplate(TemplateArea);
            Image image = new Image() { Height = TemplateArea.ActualHeight, Width = TemplateArea.ActualWidth, Source = bitmap,
                                        HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            place.Children.Add(image);
            return place;
        }

        private BitmapImage CreatePngFromTemplate(TableView TemplateArea)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)TemplateArea.ActualWidth, (int)TemplateArea.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(TemplateArea);
            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));
            MemoryStream stream = new MemoryStream();
            BitmapImage image = new BitmapImage();
            png.Save(stream);
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }
    }
}