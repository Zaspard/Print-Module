using PrintingText.ViewModel;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using Constructor;

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
            /*WindowConstructor simpleApi = new WindowConstructor();
            simpleApi.Focusable = true;
            simpleApi.Owner = this;
            simpleApi.Show();*/
        }
    }
}



//@"D:\print.xps"
//@"D:\sample1.xps"
//@"D:\pdfurl-guide.pdf"
//@"D:\topic1.docx"



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
