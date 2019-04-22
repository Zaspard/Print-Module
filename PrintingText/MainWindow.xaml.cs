using PrintingText.View;
using PrintingText.Model;
using PrintingText.ViewModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PrintingText
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM();
        }

        private void TabItem_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (((TabItem)sender).Header.ToString() == "Шаблоны")
            {
                ((MainVM)DataContext).ChangeTab(1);
            }
            else if (((TabItem)sender).Header.ToString() == "Настройка бумаги и печати")
            {
                ((MainVM)DataContext).ChangeTab(2);
            }
            else 
            {
                ((MainVM)DataContext).ChangeTab(3);
            }
        }

        private void ClickButton_CreateNewTemplate(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Windows.Constructor constructor = new Windows.Constructor();
            constructor.Focusable = true;
            constructor.Owner = this;
            constructor.Show();
        }

        private void ClickButton_DeleteSelectTemplate(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ((MainVM)DataContext).DeleteTemplate();
        }

        private void ClickButton_EditSelectTemplate(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Windows.Constructor constructor = new Windows.Constructor();
            constructor.Focusable = true;
            constructor.Owner = this;
            ((Constructor.ViewModel.ConstructorMainVM)constructor.DataContext).Deseriliz(((MainVM)DataContext).ConstructorTab.SelectedFiles.Url);
            constructor.Show();
        }

        private void ClickButton_Print(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ((MainVM)DataContext).Print(TemplateArea);
        }

        private void ClickButton_Refresh(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ((MainVM)DataContext).ConstructorTab.ReloadingCollectionFiles();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem != null)
            {
                ((MainVM)DataContext).ConstructorTab.SelectedFiles = (FindedTemplate)((ListView)sender).SelectedItem;
            }

            if (((ListView)sender).SelectedItem != null && ((MainVM)DataContext).ConstructorTab.IsAwait == true
                                                        && ((MainVM)DataContext).PrintersSetting.SelectPrinter != null)
            {
                ((MainVM)DataContext).Document.Pages.Clear();
                ((MainVM)DataContext).ConstructorTab.IsAwait = false;
                ((MainVM)DataContext).PrintersSetting.IsAwait = false;
                ((MainVM)DataContext).Deseriliz(((FindedTemplate)((ListView)sender).SelectedItem).Url);

                ((MainVM)DataContext).column = (int)(((MainVM)DataContext).Template.Width / (((MainVM)DataContext).PrintersSetting.Page.Width - 40)) + 1; // 40 отступы. 20 для каждой стороны
                ((MainVM)DataContext).row = (int)(((MainVM)DataContext).Template.Height / (((MainVM)DataContext).PrintersSetting.Page.Height - 40)) + 1;
                ((MainVM)DataContext).Template.Width = ((MainVM)DataContext).PrintersSetting.Page.Width * ((MainVM)DataContext).column;
                ((MainVM)DataContext).Template.Height = ((MainVM)DataContext).PrintersSetting.Page.Height * ((MainVM)DataContext).row;
                Task.Run(() =>
                {
                    Thread.Sleep(7000);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ((MainVM)DataContext).CuttingPages(TemplateArea);
                        PreviewArea.Children.Clear();
                        PreviewArea.Children.Add(((MainVM)DataContext).RefreshPreviewArea());
                        ((MainVM)DataContext).ConstructorTab.IsAwait = true;
                        ((MainVM)DataContext).PrintersSetting.IsAwait = true;
                    });
                });
            }
        }

        private void ClickButton_SaveFile(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ((MainVM)DataContext).SaveFile(TemplateArea);
        }

        private void ClickButton_Left(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var result = ((MainVM)DataContext).ChangePage(false);
            if (result !=null)
            {
                PreviewArea.Children.Clear();
                PreviewArea.Children.Add(result);
            }
        }

        private void ClickButton_Right(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var result = ((MainVM)DataContext).ChangePage(true);
            if (result != null)
            {
                PreviewArea.Children.Clear();
                PreviewArea.Children.Add(result);
            }
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem != null && ((MainVM)DataContext).ConstructorTab.IsAwait == true
                                                        && ((MainVM)DataContext).PrintersSetting.SelectPrinter != null 
                                                        && ((MainVM)DataContext).ConstructorTab.SelectedFiles != null)
            {
                ((MainVM)DataContext).Document.Pages.Clear();
                ((MainVM)DataContext).ConstructorTab.IsAwait = false;
                ((MainVM)DataContext).PrintersSetting.IsAwait = false;
                ((MainVM)DataContext).Deseriliz(((MainVM)DataContext).ConstructorTab.SelectedFiles.Url);

                ((MainVM)DataContext).column = (int)(((MainVM)DataContext).Template.Width / ((MainVM)DataContext).PrintersSetting.Page.Width) + 1;
                ((MainVM)DataContext).row = (int)(((MainVM)DataContext).Template.Height / ((MainVM)DataContext).PrintersSetting.Page.Height) + 1;
                ((MainVM)DataContext).Template.Width = ((MainVM)DataContext).PrintersSetting.Page.Width * ((MainVM)DataContext).column;
                ((MainVM)DataContext).Template.Height = ((MainVM)DataContext).PrintersSetting.Page.Height * ((MainVM)DataContext).row;

                Task.Run(() =>
                {
                    Thread.Sleep(7000);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ((MainVM)DataContext).CuttingPages(TemplateArea);
                        PreviewArea.Children.Clear();
                        PreviewArea.Children.Add(((MainVM)DataContext).RefreshPreviewArea());
                        ((MainVM)DataContext).ConstructorTab.IsAwait = true;
                        ((MainVM)DataContext).PrintersSetting.IsAwait = true;
                    });
                });
            }
        }
    }
}


/*printTicket.PageResolution = new PageResolution(1, 1); //DPI
  printQueue.UserPrintTicket.PageResolution = new PageResolution(1, 1);*/

#region StreamReader
/*
reader = new StreamReader(@"D:\pdfurl-guide.pdf", Encoding.Default);
verdana10Font = new Font("Verdana", 10);
PrintDocument pd = new PrintDocument();
pd.PrintPage += new PrintPageEventHandler(PrintTextFileHandler);
pd.Print();
if (reader != null)
{
    reader.Close();
}
}
private void PrintTextFileHandler(object sender, PrintPageEventArgs ppeArgs)
{ 
Graphics g = ppeArgs.Graphics;
float linesPerPage = 0;
float yPos = 0;
int count = 0;
float leftMargin = ppeArgs.MarginBounds.Left;
float topMargin = ppeArgs.MarginBounds.Top;
string line = null;
linesPerPage = ppeArgs.MarginBounds.Height / verdana10Font.GetHeight(g);
while (count < linesPerPage && ((line = reader.ReadLine()) != null))
{
    //Calculate the starting position  
    yPos = topMargin + (count * verdana10Font.GetHeight(g));
    //Draw text  
    g.DrawString(line, verdana10Font, System.Drawing.Brushes.Black, leftMargin, yPos, new StringFormat());
    //Move to next line  
    count++;
}
if (line != null)
{
    ppeArgs.HasMorePages = true;
}
else
{
    ppeArgs.HasMorePages = false;
}
}*/
#endregion
#region StreamReader_msdn
/*
streamToPrint = new StreamReader(filePath, Encoding.Default);
try
{
    printFont = new Font("Arial", 10);
    PrintDocument pd = new PrintDocument();
    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
    pd.Print();
}
finally
{
    streamToPrint.Close();
}
}
private void pd_PrintPage(object sender, PrintPageEventArgs ev)
{
float linesPerPage = 0;
int count = 0;
float yPos = 0;
float leftMargin = ev.MarginBounds.Left;
float topMargin = ev.MarginBounds.Top;
string line = null;
linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);
while (count < linesPerPage && ((line = streamToPrint.ReadLine()) != null))
{
    yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
    ev.Graphics.DrawString(line, printFont, System.Drawing.Brushes.Black,leftMargin, yPos, new StringFormat());
    count++;
}
if (line != null)
    ev.HasMorePages = true;
else
    ev.HasMorePages = false;
}
*/
#endregion
