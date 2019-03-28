using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using Constructor.Model;
using Constructor.ViewModel.Table;
using System.ComponentModel;

namespace Constructor.ViewModel
{
    public class TemplateVM : BaseVM
    {
        private double width;
        private double height;
        private State state = State.normally;
        private ITable selectTable;
        private ObservableCollection<ITable> table;

        public TemplateVM()
        {
            Width = 800;
            Height = 1200;
        }

        public ObservableCollection<ITable> Table
        {
            get
            {
                if (table == null)
                {
                    table = new ObservableCollection<ITable>();
                }
                return table;
            }
        }

        public ITable SelectTable
        {
            get { return selectTable; }
            set
            {
                selectTable = value;
                OnPropertyChanged("SelectTable");
                if (SelectTable != null)
                {
                    SelectTable.PropertyChanged += SelectTable_PropertyChanged;
                }
            }
        }

        private void SelectTable_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectCell")
            {
                SelectTable = (ITable)sender;
            }
        }

        public double Width
        {
            get{ return width; }
            set
            {
                width = value;
                OnPropertyChanged("Width");
            }
        }

        public double Height
        {
            get { return height; }
            set
            {
                height = value;
                OnPropertyChanged("Height");
            }
        }

        public State State
        {
            get { return state; }
            set
            {
                state = value;
                OnPropertyChanged("State");
            }
        }
    }
}

public enum State
{
    normally = 0,
    createText = 1,
    createTable = 2,
    createImage = 3
}
