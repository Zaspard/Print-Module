using PrintingText.Model;
using PrintingText.ViewModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PrintingText
{
    /// <summary>
    /// Логика взаимодействия для PrintModule.xaml
    /// </summary>
    public partial class PrintModule : Window
    {
        public PrintModule()
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
            //((MainVM)DataContext).Print(TemplateArea);
        }

        private void ClickButton_Refresh(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ((MainVM)DataContext).ConstructorTab.ReloadingCollectionFiles();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem != null && ((MainVM)DataContext).ConstructorTab.IsAwait == true)
            {
                ((MainVM)DataContext).ConstructorTab.IsAwait = false;
                ((MainVM)DataContext).ConstructorTab.SelectedFiles = (FindedTemplate)((ListView)sender).SelectedItem;
                ((MainVM)DataContext).Deseriliz(((FindedTemplate)((ListView)sender).SelectedItem).Url);
                Task.Run(() =>
                {
                    Thread.Sleep(7000);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        PreviewArea.Children.Clear();
                        PreviewArea.Children.Add(((MainVM)DataContext).RefreshPreviewArea());
                        ((MainVM)DataContext).ConstructorTab.IsAwait = true;
                    });
                });
            }
        }

        private void ClickButton_SaveFile(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ((MainVM)DataContext).SaveFile(TemplateArea);
        }
    }
}
