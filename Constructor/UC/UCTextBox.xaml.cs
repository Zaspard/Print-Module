using Constructor.ViewModel;
using Constructor.ViewModel.Table;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Constructor.UC
{
    public partial class UCTextBox : UserControl
    {
        public UCTextBox()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TableWithTextOrImageVM)DataContext).SelectingCell(sender);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((TableWithTextOrImageVM)DataContext).SelectingImageCell(sender);
        }
    }
}
