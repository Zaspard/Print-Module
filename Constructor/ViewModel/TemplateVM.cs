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

namespace Constructor.ViewModel
{
    public class TemplateVM : BaseVM
    {
        private double width;
        private double height;
        private State state = State.normally;
        private TableVM selectTable;
        private bool isSelected = false;
        private ObservableCollection<TableVM> table;

        public TemplateVM()
        {
            Width = 800;
            Height = 1200;
        }

        public ObservableCollection<TableVM> Table
        {
            get
            {
                if (table == null)
                {
                    table = new ObservableCollection<TableVM>();
                }
                return table;
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public TableVM SelectTable
        {
            get { return selectTable; }
            set
            {
                selectTable = value;
                OnPropertyChanged("SelectTable");
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
