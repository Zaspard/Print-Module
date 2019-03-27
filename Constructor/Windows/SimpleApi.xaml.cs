using Constructor.Model;
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
    /// <summary>
    /// Логика взаимодействия для SimpleApi.xaml
    /// </summary>
    public partial class SimpleApi : Window
    {
        public SimpleApi()
        {
            InitializeComponent();
            DataContext = new API();
        }

        private void ClickButton_Cancel(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void ClickButton_Send(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

    }
}
