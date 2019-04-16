using PrintingText.Windows.WindowsVM;
using System.Windows;

namespace PrintingText.Windows
{
    /// <summary>
    /// Логика взаимодействия для SettingTable.xaml
    /// </summary>
    public partial class SettingTable : Window
    {
        public SettingTable()
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
            var setting = ((Header)DataContext).Setting();
            if (setting != null)
            {
                ((WindowsApiVM)Owner.DataContext).SetSettings(setting);
                Close();
            }
            else
            {
                MessageBox.Show("Необходимо выбрать семейство", "Ошибка",
                                            MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
