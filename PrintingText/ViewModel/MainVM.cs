using Constructor.ViewModel;
using PrintingText.View;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Packaging;
using System.Printing;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Xps.Packaging;

namespace PrintingText.ViewModel
{
    class MainVM: BaseVM
    {
        private ITab selectTab;
        private PrintersSetting printersSetting = new PrintersSetting();
        private ConstructorTab constructorTab = new ConstructorTab();
        private ApiTab apiTab = new ApiTab();

        private TemplateVM template;
        private Document document;
        private int numderPage = 0;
        public int column;
        public int row;
        public int OldTextinTextBox = 0;

        public MainVM()
        {
            SelectTab = constructorTab;
            Template = null;
            document = new Document();
        }

        public ConstructorTab ConstructorTab
        {
            get
            {
                return constructorTab;
            }
        }

        public PrintersSetting PrintersSetting
        {
            get
            {
                return printersSetting;
            }
        }

        public ApiTab ApiTab
        {
            get
            {
                return apiTab;
            }
        }

        public Document Document
        {
            get
            {
                return document;
            }
        }

        public TemplateVM Template
        {
            get
            {
                return template;
            }
            set
            {
                template = value;
                OnPropertyChanged("Template");
            }
        }

        public ITab SelectTab
        {
            get
            {
                return selectTab;
            }
            set
            {
                selectTab = value;
                OnPropertyChanged("SelectTab");
            }
        }

        public void ChangeTab(int constructor)
        {
            if (constructor == 1)
            {
                SelectTab = constructorTab;
            }
            else if (constructor == 2)
            {
                SelectTab = printersSetting;
            }
            else if (constructor == 3)
            {
                SelectTab = apiTab;
            }
        }

        public Grid ChangePage(bool IsToRight)
        {
            if (IsToRight && numderPage != (column * row) - 1)
            {
                numderPage++;
                return RefreshPreviewArea();
            }
            else if (!IsToRight && numderPage != 0)
            {
                numderPage--;
                return RefreshPreviewArea();
            }
            return null;
        }

        public void CuttingPages(Grid TemplateArea)
        {
            numderPage = 0;
            var bitmapImage = Document.CreatePngFromTemplate(TemplateArea);

            Bitmap bitmap;
            using (MemoryStream outStream = new MemoryStream())
            {   
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmaps = new Bitmap(outStream);
                bitmap = new Bitmap(bitmaps);
            }

            Bitmap destination_bmp = new Bitmap((int)printersSetting.Page.Width, (int)printersSetting.Page.Height);
            Graphics g = Graphics.FromImage(destination_bmp);
            g.Clear(System.Drawing.Color.White);
            g.DrawImageUnscaled(destination_bmp, 0, 0);
            MemoryStream ms;
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    g.DrawImage(bitmap, new Rectangle((int)printersSetting.Page.Left, (int)printersSetting.Page.Top,
                               (int)printersSetting.Page.Width - (int)printersSetting.Page.Left, (int)printersSetting.Page.Height - (int)printersSetting.Page.Top),
                               (int)printersSetting.Page.Width * i, (int)printersSetting.Page.Height * j, //координаты
                               (int)printersSetting.Page.Width, (int)printersSetting.Page.Height, //размеры
                               GraphicsUnit.Pixel);
                    ms = new MemoryStream();
                    destination_bmp.Save(ms, ImageFormat.Bmp);
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    ms.Seek(0, SeekOrigin.Begin);
                    image.StreamSource = ms;
                    image.EndInit();
                    Document.Pages.Add(image);
                    ms = null;
                }
            }
        }

        public Grid RefreshPreviewArea()
        {
           return Document.Place(printersSetting.Page, numderPage);
        }

        #region Печать
        public void Print(Grid TemplateArea)
        {
            // Необходимое количество страниц для печати всего докупента
            var package = Package.Open(printersSetting.path, FileMode.Create);
            var doc = new XpsDocument(package);
            var writers = XpsDocument.CreateXpsDocumentWriter(doc);
            var fixedDocument = new FixedDocument();
            //
            foreach (var item in Document.Pages)
            {
                var fixedPage = new FixedPage()
                {
                    Width = printersSetting.Page.Width,
                    Height = printersSetting.Page.Height
                };
                System.Windows.Controls.Image image = new System.Windows.Controls.Image()
                {
                    Height = printersSetting.Page.Height,
                    Width = printersSetting.Page.Width,
                    Source = item,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top
                };
                fixedPage.Children.Add(image);
                var pageContent = new PageContent();
                ((IAddChild)pageContent).AddChild(fixedPage);
                fixedDocument.Pages.Add(pageContent);
            }
            writers.Write(fixedDocument, printersSetting.SelectPrinter.PrintTicket);
            doc.Close();
            package.Close();

            using (var fileStream = new StreamReader(printersSetting.path))
            {
                try
                {
                    using (var printStream = new PrintQueueStream
                    (printersSetting.SelectPrinter.PrintQueue, "Print Template", false, printersSetting.SelectPrinter.PrintTicket))
                    {
                        fileStream.BaseStream.CopyTo(printStream);
                    }
                }
                catch
                {}
            }
            File.Delete(printersSetting.path);
        }
        #endregion

        #region Сохранение файла
        public void SaveFile(Grid TemplateArea)
        {
            if (PrintersSetting.IsSaveToFile && PrintersSetting.IsSaveToPNG)
            {
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)TemplateArea.ActualWidth, (int)TemplateArea.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                renderTargetBitmap.Render(TemplateArea);
                PngBitmapEncoder pngImage = new PngBitmapEncoder();
                pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                using (Stream fileStream = File.Create("Template\\" + "image.png"))
                {
                    pngImage.Save(fileStream);
                }
            }
            else if (PrintersSetting.IsSaveToFile && PrintersSetting.IsSaveToPDF)
            {
                var fixedPage = new FixedPage();
                fixedPage.Width = TemplateArea.ActualWidth;
                fixedPage.Height = TemplateArea.ActualHeight;
                var place = Document.SaveInPdf(printersSetting.Page, TemplateArea);
                fixedPage.Children.Add(place);
                var pageContent = new PageContent();
                ((IAddChild)pageContent).AddChild(fixedPage);
                var package = Package.Open(printersSetting.path, FileMode.Create);
                var doc = new XpsDocument(package);
                var writers = XpsDocument.CreateXpsDocumentWriter(doc);
                var fixedDocument = new FixedDocument();
                fixedDocument.Pages.Add(pageContent);
                PrintQueue print = new PrintQueue(new PrintServer(), "Microsoft Print to PDF");
                writers.Write(fixedDocument, print.DefaultPrintTicket);
                doc.Close();
                package.Close();
                using (var fileStream = new StreamReader(printersSetting.path))
                {
                    try
                    {
                        using (var printStream = new PrintQueueStream
                        (print, "Print Template", false, print.DefaultPrintTicket))
                        {
                            fileStream.BaseStream.CopyTo(printStream);
                        }
                    }
                    catch
                    { }
                }
                File.Delete(printersSetting.path);
            }
        }
        #endregion

        #region Десериализация
        DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(TemplateVM));
        public void Deseriliz(string nameFile)
        {
            using (FileStream fs = new FileStream(nameFile, FileMode.OpenOrCreate))
            {
                Template = (TemplateVM)jsonFormatter.ReadObject(fs);
            }
            if (ApiTab.Dossier.SelectField != null)
            {
                Template.FillInTheData(ApiTab.Dossier.SelectField);
            }
        }
        #endregion

        #region Удаление шаблона
        public void DeleteTemplate()
        {
            if (constructorTab.SelectedFiles != null )
            {
                File.Delete(constructorTab.SelectedFiles.Url);
                constructorTab.ReloadingCollectionFiles();
                Template = null;
            }
        }
        #endregion
    }
}
