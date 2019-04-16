using API;
using PrintingText.View;
using Constructor.ViewModel;
using System.ComponentModel;
using System.IO;
using System.IO.Packaging;
using System.Printing;
using System.Runtime.Serialization.Json;
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
                //SelectTab.PropertyChanged += SelectTab_PropertyChanged;
            }
        }

        /*private void SelectTab_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Contains("SelectedFiles"))
            {
                Deseriliz(constructorTab.SelectedFiles.Url);
            }
        }*/

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

        public Grid RefreshPreviewArea(TableView TemplateArea)
        {
            return Document.Place(printersSetting.Page, TemplateArea);
        }

        #region Печать
        public void Print(TableView TemplateArea)
        {
            var fixedPage = new FixedPage();
            if (printersSetting.Page.IsPortrait)
            {
                fixedPage.Width = printersSetting.Page.Width;
                fixedPage.Height = printersSetting.Page.Height;
            }
            else
            {
                fixedPage.Width = printersSetting.Page.Height;
                fixedPage.Height = printersSetting.Page.Width;
            }
            var place = Document.Place(printersSetting.Page, TemplateArea);
            fixedPage.Children.Add(place);
            var pageContent = new PageContent();
            ((IAddChild)pageContent).AddChild(fixedPage);
            var package = Package.Open(printersSetting.path, FileMode.Create);
            var doc = new XpsDocument(package);
            var writers = XpsDocument.CreateXpsDocumentWriter(doc);
            var fixedDocument = new FixedDocument();
            fixedDocument.Pages.Add(pageContent);
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
                {
                    
                }
            }
            File.Delete(printersSetting.path);    
        }
        #endregion

        #region Сохранение файла
        public void SaveFile(TableView TemplateArea)
        {
            if (PrintersSetting.IsSaveToFile && PrintersSetting.IsSaveToPNG)
            {
                //var place = Document.Place(printersSetting.Page, TemplateArea);
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
                if (printersSetting.Page.IsPortrait)
                {
                    fixedPage.Width = printersSetting.Page.Width;
                    fixedPage.Height = printersSetting.Page.Height;
                }
                else
                {
                    fixedPage.Width = printersSetting.Page.Height;
                    fixedPage.Height = printersSetting.Page.Width;
                }
                var place = Document.Place(printersSetting.Page, TemplateArea);
                fixedPage.Children.Add(place);
                var pageContent = new PageContent();
                ((IAddChild)pageContent).AddChild(fixedPage);
                var package = Package.Open(printersSetting.path, FileMode.Create);
                var doc = new XpsDocument(package);
                var writers = XpsDocument.CreateXpsDocumentWriter(doc);
                var fixedDocument = new FixedDocument();
                fixedDocument.Pages.Add(pageContent);
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
