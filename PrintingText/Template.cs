using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace PrintingText
{
    class Template
    {
        public Grid Place(Page page)
        {
            //FlowDocument flowDocument = new FlowDocument();          
            Grid place = new Grid
            {
                //Background = Brushes.AntiqueWhite,
                Margin = new Thickness(page.Left, page.Top, page.Right, page.Bottom),
                Width = 780,
                Height = 1050
            };
            TextBlock s = new TextBlock() { Text = "Это строка не должна быть напечатана" };
            place.Children.Add(s);
            //Table table = new Table();
            //table.Columns.Add()
            //flowDocument.Blocks.Add();
            return place;
        }
    }
}