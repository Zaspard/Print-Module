using PrintingText.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Packaging;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Xps.Packaging;

namespace PrintingText.ViewModel
{
    class PrintersSetting : INotifyPropertyChanged
    {
        private PrintServer printServer = new PrintServer();
        private Printer selectPrinter;
        private string nameSelectPrinter;
        public ObservableCollection<Printer> Printers { get; set; }
        public ObservableCollection<string> NamePrinters { get; set; }
        public Page Page { get; set; }


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

        public ICommand Print
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var fp = new FixedPage();
                    //Window.Children.Remove(place);
                    if (Page.IsPortrait)
                    {
                        fp.Width = Page.Width;
                        fp.Height = Page.Height;
                    }
                    else
                    {
                        fp.Width = Page.Height;
                        fp.Height = Page.Width;
                    }


                    Grid place = new Grid
                    {
                        Background = new SolidColorBrush(Colors.Green),
                        Margin = new Thickness(Page.Left, Page.Top, Page.Right, Page.Bottom),
                        Width = 780,
                        Height = 1050                  
                    };


                    fp.Children.Add(place);
                    //var oldMargin = place.Margin;
                    //place.Margin = margin;
                    var pageContent = new PageContent();
                    ((IAddChild)pageContent).AddChild(fp);

                    selectPrinter.PrintTicket.PageBorderless = PageBorderless.Borderless;
                    var package = Package.Open(@"D:\print.xps", FileMode.Create);
                    var doc = new XpsDocument(package);
                    var writers = XpsDocument.CreateXpsDocumentWriter(doc);
                    var fixedDocument = new FixedDocument();


                    fixedDocument.Pages.Add(pageContent);

                    writers.Write(fixedDocument, selectPrinter.PrintTicket);
                    doc.Close();
                    package.Close();
                    //fp.Children.Remove(place);
                    //Window.Children.Add(place);
                    //place.Margin = oldMargin;
                    using (var fileStream = new StreamReader(@"D:\print.xps"))
                    {
                        using (var printStream = new PrintQueueStream(selectPrinter.PrintQueue, "jobName", false, selectPrinter.PrintTicket))
                        {
                            fileStream.BaseStream.CopyTo(printStream);
                        }
                    }
                });
            }
        }

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
                        /*var xpsDocument = new XpsDocument(@"D:\print.xps", FileAccess.ReadWrite);
                        var fixedDocSeq = xpsDocument.GetFixedDocumentSequence();
                        pDialog.PrintDocument(fixedDocSeq.DocumentPaginator, "Test print job");*/
                    }
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
