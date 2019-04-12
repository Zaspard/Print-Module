using Constructor.ViewModel;
using Constructor.Windows;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;

namespace Constructor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainVM ViewModel
        {
            get
            {
                return (MainVM)DataContext;
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
            TableWindow tableWindow = new TableWindow();
            tableWindow.Focusable = true;
            tableWindow.Owner = this;
            tableWindow.Show();            
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