using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Printing;
using System.Runtime.CompilerServices;

namespace PrintingText.Model
{
    class Printer : INotifyPropertyChanged
    {
        private PrintQueue printQueue;//+
        private string printerName;
        private PrintTicket printTicket;//+
        private PrintCapabilities printCapabilities;//+
        private float copyCount;//+
        private InputBin inputBin;//+
        private OutputColor outputColor;//+
        private PageMediaSize pageMediaSize;//+
        private PageOrientation pageOrientation;//+
        private bool isOffline = false;

        public BlockingCollection<InputBin> InputBins { get; set; } = new BlockingCollection<InputBin>();
        public BlockingCollection<OutputColor> OutputColors { get; set; } = new BlockingCollection<OutputColor>();
        public BlockingCollection<PageMediaSize> PageMediaSizes { get; set; } = new BlockingCollection<PageMediaSize>();
        public BlockingCollection<PageOrientation> PageOrientations { get; set; } = new BlockingCollection<PageOrientation>();

        public PrintQueue PrintQueue
        {
            get { return printQueue; }
            set
            {
                printQueue = value;
                printerName = printQueue.Name;
                printTicket = printQueue.DefaultPrintTicket;
                printCapabilities = printQueue.GetPrintCapabilities();
                if (!printQueue.IsOffline)
                {
                    #region Filling BlockingCollection
                    foreach (var item in printCapabilities.InputBinCapability)
                    {
                        InputBins.Add(item);
                    }
                    foreach (var item in printCapabilities.OutputColorCapability)
                    {
                        OutputColors.Add(item);
                    }
                    foreach (var item in printCapabilities.PageMediaSizeCapability)
                    {
                        PageMediaSizes.Add(item);
                    }
                    foreach (var item in printCapabilities.PageOrientationCapability)
                    {
                        PageOrientations.Add(item);
                    }
                    #endregion
                    CopyCount = 1;
                    if (printTicket.InputBin != null)
                    {
                        InputBin = (InputBin)printTicket.InputBin;
                    }
                    if (printTicket.OutputColor != null)
                    {
                        OutputColor = (OutputColor)printTicket.OutputColor;
                    }
                    if (printTicket.PageOrientation != null)
                    {
                        PageOrientation = (PageOrientation)printTicket.PageOrientation;
                    }
                    PageMediaSize = printTicket.PageMediaSize;
                    OnPropertyChanged("PrintQueue");
                }
                else
                {
                    IsOffline = true;
                }
            }
        }

        public PrintTicket PrintTicket
        {
            get { return printTicket; }
            set
            {
                printTicket = value;
                OnPropertyChanged("PrintTicket");
            }
        }

        public bool IsOffline
        {
            get { return isOffline; }
            set
            {
                isOffline = value;
                OnPropertyChanged("IsOffline");
            }
        }

        public string PrinterName
        {
            get { return printerName; }
            set { printerName = value; }
        }

        public float CopyCount
        {
            get { return copyCount; }
            set
            {
                if (!IsOffline && printCapabilities.MaxCopyCount >= value && value != 0)
                {
                    copyCount = value;
                    OnPropertyChanged("CopyCount");
                }
            }
        }

        public InputBin InputBin
        {
            get { return inputBin; }
            set
            {
                if (!IsOffline)
                {
                    inputBin = value;
                    OnPropertyChanged("InputBin");
                }
            }
        }

        public OutputColor OutputColor
        {
            get { return outputColor; }
            set
            {
                if (!IsOffline)
                {
                    outputColor = value;
                    OnPropertyChanged("OutputColor");
                }
            }
        }

        public PageMediaSize PageMediaSize
        {
            get { return pageMediaSize; }
            set
            {
                if (!IsOffline)
                {
                    pageMediaSize = value;
                    OnPropertyChanged("PageMediaSize");
                }
            }
        }

        public PageOrientation PageOrientation
        {
            get { return pageOrientation; }
            set
            {
                if (!IsOffline)
                {
                    pageOrientation = value;
                    if (PageOrientation.Portrait.Equals(pageOrientation))
                    {
                        OnPropertyChanged("IsPortrait");
                    }
                    else
                    {
                        OnPropertyChanged("IsLandscape");
                    }                  
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
