using PrintingText.View;
using System.Collections.Generic;
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
        public List<BitmapImage> Pages = new List<BitmapImage>();


        public Grid Place(Page page, int numberPage)
        {          
            Grid place = new Grid
            {
                Width = page.Width,
                Height = page.Height
            };
            var bitmap = Pages[numberPage];
            Image image = new Image() { Height = page.Height, Width = page.Width, Source = bitmap,
                                        HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            place.Children.Add(image);
            return place;
        }

        public Grid Place(Grid TemplateArea, Page page)
        {
            Grid place = new Grid
            {
                Width = TemplateArea.Width,
                Height = TemplateArea.Height,
                Margin = new Thickness(page.Left,page.Top, page.Right, page.Bottom)
                
            };
            var bitmap = CreatePngFromTemplate(TemplateArea);
            Image image = new Image()
            {
                Height = TemplateArea.Height,
                Width = TemplateArea.Width,
                Source = bitmap,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
            place.Children.Add(image);
            return place;
        }

        public Grid SaveInPdf(Page page, Grid TemplateArea)
        {
            Grid place = new Grid
            {
                Width = TemplateArea.ActualWidth,
                Height = TemplateArea.ActualHeight
            };
            var bitmap = CreatePngFromTemplate(TemplateArea);
            Image image = new Image()
            {
                Height = TemplateArea.ActualHeight,
                Width = TemplateArea.ActualWidth,
                Source = bitmap,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
            place.Children.Add(image);
            return place;
        }

        public BitmapImage CreatePngFromTemplate(Grid TemplateArea)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)TemplateArea.ActualHeight, (int)TemplateArea.ActualHeight, 96, 96, PixelFormats.Pbgra32);
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