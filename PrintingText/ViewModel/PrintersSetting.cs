using Constructor.ViewModel;
using PrintingText.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Packaging;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Xps.Packaging;

namespace PrintingText.ViewModel
{
    class PrintersSetting : BaseVM, ITab
    {
        private PrintServer printServer = new PrintServer();
        private Printer selectPrinter;
        private string nameSelectPrinter;
        public ObservableCollection<Printer> Printers { get; set; }
        public ObservableCollection<string> NamePrinters { get; set; }
        public Page Page { get; set; }
        //public Template template = new Template();
        private readonly string path = "filename.xps"; //!!!

        public PrintersSetting()
        {
            Page = new Page();
            Printers = new ObservableCollection<Printer>();
            NamePrinters = new ObservableCollection<string>();
            foreach (var item in printServer.GetPrintQueues())
            {
                Printers.Add(new Printer{ PrintQueue = item });
                NamePrinters.Add(item.Name);
            }
        }

        public Printer SelectPrinter
        {
            get { return selectPrinter; }
            set
            {
                selectPrinter = value;
                if (!selectPrinter.IsOffline)
                {
                    ReFillPage();
                }
                OnPropertyChanged("SelectPrinter");
                selectPrinter.PropertyChanged += SelectPrinter_PropertyChanged;
            }
        }

        private void ReFillPage()
        {
            if (selectPrinter.PageOrientation == PageOrientation.Portrait)
            { Page.IsPortrait = true; }
            else { Page.IsPortrait = false; }
            Page.Height = selectPrinter.PageMediaSize.Height.Value;
            Page.Width = selectPrinter.PageMediaSize.Width.Value;
        }
       
        private void SelectPrinter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsPortrait")
            { Page.IsPortrait = true; }
            else if (e.PropertyName == "IsLandscape")
            { Page.IsPortrait = false; }
            else if (e.PropertyName == "PageMediaSize")
            {
                var size = ((Printer)sender).PageMediaSize;
                Page.Height = size.Height.Value;
                Page.Width = size.Width.Value;
            }
        }

        public string NameSelectPrinter
        {
            get { return nameSelectPrinter; }
            set
            {
                nameSelectPrinter = value;
                foreach (var item in Printers)
                {
                    if (item.PrinterName == NameSelectPrinter)
                    {
                        SelectPrinter = item;
                        break;
                    }
                }
                OnPropertyChanged("NameSelectPrinter");
            }
        }

        /*public ICommand Print
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var fixedPage = new FixedPage();
                    if (Page.IsPortrait)
                    {
                        fixedPage.Width = Page.Width;
                        fixedPage.Height = Page.Height;
                    }
                    else
                    {
                        fixedPage.Width = Page.Height;
                        fixedPage.Height = Page.Width;
                    }

                    fixedPage.Children.Add(template.Place(Page));
                    var pageContent = new PageContent();
                    ((IAddChild)pageContent).AddChild(fixedPage);

                    var package = Package.Open(path, FileMode.Create);
                    var doc = new XpsDocument(package);
                    var writers = XpsDocument.CreateXpsDocumentWriter(doc);
                    var fixedDocument = new FixedDocument();
                    fixedDocument.Pages.Add(pageContent);
                    writers.Write(fixedDocument, selectPrinter.PrintTicket);
                    doc.Close();
                    package.Close();

                    using (var fileStream = new StreamReader(path))
                    {
                        using (var printStream = new PrintQueueStream
                        (selectPrinter.PrintQueue, "Print Template", false, selectPrinter.PrintTicket))
                        {
                            fileStream.BaseStream.CopyTo(printStream);
                        }

                    }
                    File.Delete(path);
                });
            }
        }*/

        public ICommand PrintDialog
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var pDialog = new PrintDialog();
                    pDialog.PageRangeSelection = PageRangeSelection.AllPages;
                    pDialog.UserPageRangeEnabled = true;
                    var print = pDialog.ShowDialog();
                    if (print == true)
                    {
                        /*foreach (var printer in Printers)
                        {
                            if (pDialog.PrintQueue == printer.PrintQueue)
                            {
                                printer.PrintTicket = pDialog.PrintTicket;
                            }
                        }*/
                    }
                });
            }
        }
    }
}
