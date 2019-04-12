using Constructor.ViewModel;
using PrintingText.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Printing;
using System.Windows.Controls;
using System.Windows.Input;

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
        private bool isPrint = false;
        private bool isSaveToFile = false;
        private bool isSaveToPDF = false;
        private bool isSaveToPNG = false;
        private double width;
        private double height;
        public readonly string path = "filename.xps";

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
            width = 0;
            height = 0;
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
                Width= size.Width.Value;
                Height = size.Height.Value;
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

        public bool IsPrint
        {
            get { return isPrint; }
            set
            {
                isPrint = value;
                if (isPrint)
                {
                    IsSaveToFile = false;
                }
                OnPropertyChanged("IsPrint");
            }
        }

        public bool IsSaveToFile
        {
            get { return isSaveToFile; }
            set
            {
                isSaveToFile = value;
                if (isSaveToFile)
                {
                    IsPrint = false;
                }
                OnPropertyChanged("IsSaveToFile");
            }
        }

        public bool IsSaveToPDF
        {
            get { return isSaveToPDF; }
            set
            {
                isSaveToPDF = value;
                if (isSaveToPDF)
                {
                    isSaveToPNG = false;
                }
                OnPropertyChanged("isSaveToPDF");
            }
        }

        public bool IsSaveToPNG
        {
            get { return isSaveToPNG; }
            set
            {
                isSaveToPNG = value;
                if (isSaveToPNG)
                {
                    isSaveToPDF = false;
                }
                OnPropertyChanged("isSaveToPNG");
            }
        }

        public double Width
        {
            get { return width; }
            set
            {
                width = value;
                OnPropertyChanged("Width");
            }
        }

        public double Height
        {
            get { return height; }
            set
            {
                height = value;
                OnPropertyChanged("Height");
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
                        foreach (var printer in Printers)
                        {
                            if (pDialog.PrintQueue == printer.PrintQueue)
                            {
                                printer.PrintTicket = pDialog.PrintTicket;
                            }
                        }
                    }
                });
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
    }
}
