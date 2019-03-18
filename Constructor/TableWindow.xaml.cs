using Constructor.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Constructor
{
    public partial class TableWindow : Window
    {
        public TableWindow()
        {
            InitializeComponent();
        }

        private void ClickButton_Cancel(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void ClickButton_Send(object sender, ExecutedRoutedEventArgs e)
        {
            ((MainVM)Owner.DataContext).CreateTable(new Point(0, 0), int.Parse(CountColumn.Text), int.Parse(CountRow.Text));
            Close();
        }
    }
}
