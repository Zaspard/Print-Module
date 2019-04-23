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
            ((MainVM)DataContext).Print(Place);
        }

        private void ClickButton_Refresh(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ((MainVM)DataContext).ConstructorTab.ReloadingCollectionFiles();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem != null)
            {
                ((MainVM)DataContext).ConstructorTab.SelectedFiles = (FindedTemplate)((ListView)sender).SelectedItem;
            }
            if (((ListView)sender).SelectedItem != null && ((MainVM)DataContext).ConstructorTab.IsAwait == true
                                                        && ((MainVM)DataContext).PrintersSetting.SelectPrinter != null)
            {
                ((MainVM)DataContext).Document.Pages.Clear();
                ((MainVM)DataContext).ConstructorTab.IsAwait = false;
                ((MainVM)DataContext).PrintersSetting.IsAwait = false;
                ((MainVM)DataContext).Deseriliz(((FindedTemplate)((ListView)sender).SelectedItem).Url);

                ((MainVM)DataContext).column = (int)(((MainVM)DataContext).Template.Width / (((MainVM)DataContext).PrintersSetting.Page.Width - 40)) + 1; // 40 отступы. 20 для каждой стороны
                ((MainVM)DataContext).row = (int)(((MainVM)DataContext).Template.Height / (((MainVM)DataContext).PrintersSetting.Page.Height - 40)) + 1;
                ((MainVM)DataContext).Template.Width = ((MainVM)DataContext).PrintersSetting.Page.Width * ((MainVM)DataContext).column;
                ((MainVM)DataContext).Template.Height = ((MainVM)DataContext).PrintersSetting.Page.Height * ((MainVM)DataContext).row;
                Task.Run(() =>
                {
                    Thread.Sleep(7000);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ((MainVM)DataContext).CuttingPages(Place);
                        PreviewArea.Children.Clear();
                        PreviewArea.Children.Add(((MainVM)DataContext).RefreshPreviewArea());
                        ((MainVM)DataContext).ConstructorTab.IsAwait = true;
                        ((MainVM)DataContext).PrintersSetting.IsAwait = true;
                    });
                });
            }
        }

        private void ClickButton_SaveFile(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ((MainVM)DataContext).SaveFile(Place);
        }

        private void ClickButton_Left(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var result = ((MainVM)DataContext).ChangePage(false);
            if (result != null)
            {
                PreviewArea.Children.Clear();
                PreviewArea.Children.Add(result);
            }
        }

        private void ClickButton_Right(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var result = ((MainVM)DataContext).ChangePage(true);
            if (result != null)
            {
                PreviewArea.Children.Clear();
                PreviewArea.Children.Add(result);
            }
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem != null && ((MainVM)DataContext).ConstructorTab.IsAwait == true
                                                        && ((MainVM)DataContext).PrintersSetting.SelectPrinter != null
                                                        && ((MainVM)DataContext).ConstructorTab.SelectedFiles != null
                                                        && ((MainVM)DataContext).PrintersSetting.IsSizeSelected != false)
            {
                ((MainVM)DataContext).Document.Pages.Clear();
                ((MainVM)DataContext).ConstructorTab.IsAwait = false;
                ((MainVM)DataContext).PrintersSetting.IsAwait = false;
                ((MainVM)DataContext).Deseriliz(((MainVM)DataContext).ConstructorTab.SelectedFiles.Url);

                ((MainVM)DataContext).column = (int)(((MainVM)DataContext).Template.Width / ((MainVM)DataContext).PrintersSetting.Page.Width) + 1;
                ((MainVM)DataContext).row = (int)(((MainVM)DataContext).Template.Height / ((MainVM)DataContext).PrintersSetting.Page.Height) + 1;
                ((MainVM)DataContext).Template.Width = ((MainVM)DataContext).PrintersSetting.Page.Width * ((MainVM)DataContext).column;
                ((MainVM)DataContext).Template.Height = ((MainVM)DataContext).PrintersSetting.Page.Height * ((MainVM)DataContext).row;

                Task.Run(() =>
                {
                    Thread.Sleep(7000);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ((MainVM)DataContext).CuttingPages(Place);
                        PreviewArea.Children.Clear();
                        PreviewArea.Children.Add(((MainVM)DataContext).RefreshPreviewArea());
                        ((MainVM)DataContext).ConstructorTab.IsAwait = true;
                        ((MainVM)DataContext).PrintersSetting.IsAwait = true;
                    });
                });
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text != "")
            {
                int.TryParse(((TextBox)sender).Text, out ((MainVM)DataContext).OldTextinTextBox);
            }
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (((MainVM)DataContext).PrintersSetting.IsPrint && ((TextBox)sender).Text != ""
                                                              && ((TextBox)sender).Text != ((MainVM)DataContext).OldTextinTextBox.ToString()
                                                              && ((MainVM)DataContext).ConstructorTab.IsAwait == true
                                                              && ((MainVM)DataContext).PrintersSetting.SelectPrinter != null
                                                              && ((MainVM)DataContext).ConstructorTab.SelectedFiles != null)
            {

                ((MainVM)DataContext).Document.Pages.Clear();
                ((MainVM)DataContext).ConstructorTab.IsAwait = false;
                ((MainVM)DataContext).PrintersSetting.IsAwait = false;
                ((MainVM)DataContext).Deseriliz(((MainVM)DataContext).ConstructorTab.SelectedFiles.Url);

                ((MainVM)DataContext).column = (int)(((MainVM)DataContext).Template.Width / ((MainVM)DataContext).PrintersSetting.Page.Width) + 1;
                ((MainVM)DataContext).row = (int)(((MainVM)DataContext).Template.Height / ((MainVM)DataContext).PrintersSetting.Page.Height) + 1;
                ((MainVM)DataContext).Template.Width = ((MainVM)DataContext).PrintersSetting.Page.Width * ((MainVM)DataContext).column;
                ((MainVM)DataContext).Template.Height = ((MainVM)DataContext).PrintersSetting.Page.Height * ((MainVM)DataContext).row;

                Task.Run(() =>
                {
                    Thread.Sleep(7000);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ((MainVM)DataContext).CuttingPages(Place);
                        PreviewArea.Children.Clear();
                        PreviewArea.Children.Add(((MainVM)DataContext).RefreshPreviewArea());
                        ((MainVM)DataContext).ConstructorTab.IsAwait = true;
                        ((MainVM)DataContext).PrintersSetting.IsAwait = true;
                    });
                });
            }
            else if (((MainVM)DataContext).PrintersSetting.IsSaveToFile && ((MainVM)DataContext).ConstructorTab.SelectedFiles != null
                                                                        && ((TextBox)sender).Text != ""
                                                                        && ((TextBox)sender).Text != ((MainVM)DataContext).OldTextinTextBox.ToString())
            {
                ((MainVM)DataContext).Document.Pages.Clear();
                ((MainVM)DataContext).ConstructorTab.IsAwait = false;
                ((MainVM)DataContext).PrintersSetting.IsAwait = false;
                ((MainVM)DataContext).PrintersSetting.Height = ((MainVM)DataContext).Template.Height;
                ((MainVM)DataContext).PrintersSetting.Width = ((MainVM)DataContext).Template.Width;
                Task.Run(() =>
                {
                    Thread.Sleep(7000);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        PreviewArea.Children.Clear();
                        PreviewArea.Children.Add(((MainVM)DataContext).RefreshPreviewArea(Place));
                        ((MainVM)DataContext).ConstructorTab.IsAwait = true;
                        ((MainVM)DataContext).PrintersSetting.IsAwait = true;
                    });
                });
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (((MainVM)DataContext).ConstructorTab.SelectedFiles != null)
            {
                ((MainVM)DataContext).Document.Pages.Clear();
                ((MainVM)DataContext).ConstructorTab.IsAwait = false;
                ((MainVM)DataContext).PrintersSetting.IsAwait = false;
                ((MainVM)DataContext).Deseriliz(((MainVM)DataContext).ConstructorTab.SelectedFiles.Url);
                ((MainVM)DataContext).PrintersSetting.Height = ((MainVM)DataContext).Template.Height;
                ((MainVM)DataContext).PrintersSetting.Width = ((MainVM)DataContext).Template.Width;
                Task.Run(() =>
                {
                    Thread.Sleep(7000);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        PreviewArea.Children.Clear();
                        PreviewArea.Children.Add(((MainVM)DataContext).RefreshPreviewArea(Place));
                        ((MainVM)DataContext).ConstructorTab.IsAwait = true;
                        ((MainVM)DataContext).PrintersSetting.IsAwait = true;
                    });
                });
            }
        }
    }
}
