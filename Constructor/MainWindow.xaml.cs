using Constructor.ViewModel;
using Constructor.Windows;
using Constructor.SimpleAdorner;
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


        //////////////////////////////////////Adorner

        //AdornerLayer aLayer;
        //bool _isDown;
        //bool selected = false;
        //UIElement selectedElement = null;

        //Point _startPoint;
        //private double _originalLeft;
        //private double _originalTop;

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    MouseLeftButtonDown += new MouseButtonEventHandler(Window1_MouseLeftButtonDown);
        //    MouseLeftButtonUp += new MouseButtonEventHandler(DragFinishedMouseHandler);

        //    //constructor.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(myCanvas_PreviewMouseLeftButtonDown);
        //    constructor.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(DragFinishedMouseHandler);
        //}

        //// Handler for drag stopping on leaving the window
        //void Window1_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    StopDragging();
        //    e.Handled = true;
        //}

        //// Handler for drag stopping on user choise
        //void DragFinishedMouseHandler(object sender, MouseButtonEventArgs e)
        //{
        //    StopDragging();
        //    e.Handled = true;
        //}

        //// Method for stopping dragging
        //private void StopDragging()
        //{
        //    if (_isDown)
        //    {
        //        _isDown = false;
        //    }
        //}


        //// Handler for clearing element selection, adorner removal
        //void Window1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (selected)
        //    {
        //        selected = false;
        //        if (selectedElement != null)
        //        {
        //            aLayer.Remove(aLayer.GetAdorners(selectedElement)[0]);
        //            selectedElement = null;
        //        }
        //    }
        //}

        // Handler for element selection on the canvas providing resizing adorner
        //void myCanvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (selected)
        //    {
        //        selected = false;
        //        if (selectedElement != null)
        //        {
        //            aLayer.Remove(aLayer.GetAdorners(selectedElement)[0]);
        //            selectedElement = null;
        //        }
        //    }
        //    Point location = Mouse.GetPosition(constructor);
        //    ViewModel.Select(location);
        //    if (e.Source != constructor)
        //    {
        //        _isDown = true;
        //        _startPoint = e.GetPosition(constructor);

        //        selectedElement = e.Source as UIElement;

        //        _originalLeft = Canvas.GetLeft(selectedElement);
        //        _originalTop = Canvas.GetTop(selectedElement);

        //        aLayer = AdornerLayer.GetAdornerLayer(selectedElement);
        //        aLayer.Add(new AdornerResize(selectedElement));
        //        selected = true;
        //        e.Handled = true;
        //    }
        //}
    }
}