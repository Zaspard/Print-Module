using Constructor.UsersControl;
using PrintingText.ViewModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Constructor.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private State state = State.normally;
        private Grid grid = new Grid()
        {
            Background = new SolidColorBrush(Colors.Aqua)
        };

        private double _panelX;
        private double _panelY;

        public MainWindowViewModel()
        {}

        public Grid Grid
        {
            get { return grid; }
            set
            {
                grid = value;
                OnPropertyChanged("Grid");
            }
        }

        public ICommand MouseDownCommand 
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (state.Equals(State.createTextBox))
                    {
                        var someUC = new SomeUC() { Margin = new Thickness(PanelX, PanelY, 0, 0) };
                        Grid.Children.Add(someUC);
                        OnPropertyChanged("Grid");
                        state = State.normally;
                    }
                });
            }
        }

        public ICommand CreateTextBox
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (state.Equals(State.createTextBox))
                    {
                        state = State.normally;
                    }
                    else
                        state = State.createTextBox;
                });
            }
        }

        public double PanelX
        {
            get { return _panelX; }
            set
            {
                if (value.Equals(_panelX)) return;
                _panelX = value;
                OnPropertyChanged("PanelX");
            }
        }

        public double PanelY
        {
            get { return _panelY; }
            set
            {
                if (value.Equals(_panelY)) return;
                _panelY = value;
                OnPropertyChanged("PanelY");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

public enum State
{
    normally = 1,
    createTextBox = 2,
    createTabel = 3,
    createImage = 4
}