using API;
using Constructor.Model;
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

namespace Constructor.Windows
{
    public partial class SimpleApi : Window
    {
        public SimpleApi()
        {
            InitializeComponent();
            DataContext = new Dossier();
        }

        private void ClickButton_Cancel(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void ClickButton_Send(object sender, ExecutedRoutedEventArgs e)
        {
            if (((Dossier)DataContext).SelectNameAttribute != null)
            {
                ((MainVM)Owner.DataContext).AddSimpleAPI(((Dossier)DataContext).SelectNameAttribute);
                Close();
            }
            else MessageBox.Show("Выберите имя атрибута");
        }

    }
}
