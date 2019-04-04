using Constructor.Model.api;
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

namespace Constructor.Windows
{
    public partial class TableApi : Window
    {

        public TableApi()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((Header)DataContext).AddFamilies(((WindowsApiVM)Owner.DataContext).Api.Classificator.Families);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SendSelectedApi(object sender, RoutedEventArgs e)
        {
            ((WindowsApiVM)Owner.DataContext).SendSettings(((Header)DataContext).Setting());
            Close();
        }
    }
}
