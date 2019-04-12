using PrintingText.ViewModel;
using System.IO;
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
                ((MainVM)DataContext).ChangeTab(true);
            }
            else
            {
                ((MainVM)DataContext).ChangeTab(false);
            }
        }

        private void ClickButton_CreateNewTemplate(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Constructor.MainWindow constructor = new Constructor.MainWindow();
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
            Constructor.MainWindow constructor = new Constructor.MainWindow();
            constructor.Focusable = true;
            constructor.Owner = this;
            ((Constructor.ViewModel.MainVM)constructor.DataContext).Deseriliz(((MainVM)DataContext).ConstructorTab.SelectedFiles.Url);
            constructor.Show();
        }

        private void ClickButton_Print(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ((MainVM)DataContext).Print(TemplateArea);
        }

        private void ClickButton_Refresh(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            /*RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)constructor.ActualWidth, (int)constructor.ActualHeight , 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(constructor);
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            using (Stream fileStream = File.Create("Template\\"+ "image.png"))
            {
                pngImage.Save(fileStream);
            }*/
            ((MainVM)DataContext).ConstructorTab.ReloadingCollectionFiles();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PreviewArea.Children.Clear();
            PreviewArea.Children.Add(((MainVM)DataContext).RefreshPreviewArea(TemplateArea));
        }

        private void TemplateArea_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

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
