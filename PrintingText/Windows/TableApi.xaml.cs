using Constructor.ViewModel;
using PrintingText.Windows.WindowsVM;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrintingText.Windows
{
    public partial class TableApi : Window
    {
        public TableApi()
        {
            InitializeComponent();
            DataContext = new WindowsApiVM();
        }

        private void ClickButton_Cancel(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void ClickButton_Send(object sender, ExecutedRoutedEventArgs e)
        {
            if (((WindowsApiVM)DataContext).TableIsUsedApi == false)
            {
                ((ConstructorMainVM)Owner.DataContext).CreateTable(new Point(0, 0), ((WindowsApiVM)DataContext).Columns, ((WindowsApiVM)DataContext).Rows);
            }
            else
            {
                ((ConstructorMainVM)Owner.DataContext).CreateTable(new Point(0, 0), ((WindowsApiVM)DataContext).Columns, ((WindowsApiVM)DataContext).Rows, ((WindowsApiVM)DataContext).Table
                    , ((WindowsApiVM)DataContext).Tuples);
            }
            Close();
        }

        private void Qwe_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            stackPanelScroll.ScrollToHorizontalOffset(e.HorizontalOffset);
        }

        private void ItemsSourceIsChanged(object sender, EventArgs e)
        {
            propertyPanel.Children.Clear();

            var List = ((WindowsApiVM)DataContext).HeadersTable;

            for (int i = 0; i < DataGrid.Columns.Count; ++i)
            {
                var innerPanel = new StackPanel()
                {
                    Orientation = Orientation.Vertical
                };

                var button = new Button()
                {
                    Width = DataGrid.ColumnWidth.Value,
                    DataContext = List[i],
                    Content = (List[i].Name == null) ? "Пусто" : List[i].Name
                };
                button.Click += ClickButton_SelectSettingApi;

                innerPanel.Children.Add(button);

                propertyPanel.Children.Add(innerPanel);
            }
        }

        private void GridOfData_Loaded(object sender, RoutedEventArgs e)
        {
            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            if (dpd != null)
            {
                propertyPanel.Children.Clear();
                var List = ((WindowsApiVM)DataContext).HeadersTable;
                for (int i = 0; i < 1; ++i)
                {
                    var innerPanel = new StackPanel()
                    {
                        Orientation = Orientation.Vertical
                    };
                    var button = new Button()
                    {
                        Width = DataGrid.ColumnWidth.Value,
                        DataContext = List[i],
                        Content = "Пусто"
                    };
                    button.Click += ClickButton_SelectSettingApi;
                    innerPanel.Children.Add(button);
                    propertyPanel.Children.Add(innerPanel);
                }
                dpd.AddValueChanged(DataGrid, ItemsSourceIsChanged);
            }
        }

        private void ClickButton_SelectSettingApi(object sender, RoutedEventArgs e)
        {
            var settingTable = new SettingTable()
            {
                Focusable = true,
                Owner = this,
                DataContext = ((Control)sender).DataContext
            };
            settingTable.Show();
        }
    }
}
