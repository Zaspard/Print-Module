using Constructor.ViewModel;
using Microsoft.Win32;
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

namespace PrintingText.Windows
{
    public partial class Constructor : Window
    {
        public Constructor()
        {
            InitializeComponent();
        }

        public ConstructorMainVM ViewModel
        {
            get
            {
                return (ConstructorMainVM)DataContext;
            }
        }

        private void SimpleRequestByAPI(object sender, ExecutedRoutedEventArgs e)
        {
            SimpleApi simpleApi = new SimpleApi();
            simpleApi.Focusable = true;
            simpleApi.Owner = this;
            simpleApi.Show();
        }

        private void ClickButton_CreateTable(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.Template.State = State.createTable;
            TableApi tableApi = new TableApi();
            tableApi.Focusable = true;
            tableApi.Owner = this;
            tableApi.Show();
        }

        private void ClickButton_CreateTextBox(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.Template.State = State.createText;
        }

        private void ClickButton_CreateImage(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.Template.State = State.createImage;
        }

        private void CreateElement_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ViewModel.Template.State == State.createText)
            {
                Point newLocation = Mouse.GetPosition(constructor);
                ViewModel.CreateTextBox(newLocation);
            }
            else if (ViewModel.Template.State == State.createImage)
            {
                Point newLocation = Mouse.GetPosition(constructor);
                ViewModel.CreateImage(newLocation);
            }
        }

        private void Select(object sender, ExecutedRoutedEventArgs e)
        {
            Point location = Mouse.GetPosition(constructor);
            ViewModel.Select(location);
        }

        private void DeleteSelectedTable(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.DeleteSelectedTable();
        }

        private void CreateOpenDialog(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                ViewModel.AddImageInSelectCell(op.FileName);
            }
        }

        private void Serialize(object sender, ExecutedRoutedEventArgs e)
        {
            if (ViewModel.Seriliz())
            {
                Close();
            }
        }

        private void Close(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
