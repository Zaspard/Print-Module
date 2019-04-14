using API;
using Constructor.ViewModel.Table;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Constructor.ViewModel
{
    [DataContract]
    [KnownType(typeof(TableWithArrayVM))]
    [KnownType(typeof(TableWithTextOrImageVM))]
    public class TemplateVM : BaseVM
    {
        [DataMember]
        private string nameTeplate;
        [DataMember]
        private double width;
        [DataMember]
        private double height;
        [field:NonSerialized]
        private State state = State.normally;
        [field: NonSerialized]
        private ITable selectTable;
        [DataMember]
        public ObservableCollection<ITable> Table { get; set; } = new ObservableCollection<ITable>();

        public TemplateVM()
        {
            Width = 800;
            Height = 1200;
        }
        
        public string NameTemplate
        {
            get { return nameTeplate; }
            set
            {
                nameTeplate = value;
                OnPropertyChanged("NameTemplate");
            }
        }

        public ITable SelectTable
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
                if (value == 0 || value <0)
                { return; }
                width = value;
                OnPropertyChanged("Width");
            }
        }

        public double Height
        {
            get { return height; }
            set
            {
                if (value == 0 || value < 0)
                { return; }
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

        #region Для предпросмотра
        [field: NonSerialized]
        private bool isEnabled = false;

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }
        #endregion

        #region Заполенение данными
        public void FillInTheData(Field SelectField)
        {
            foreach (var table in Table)
            {
                if (table.IsUsedApi)
                {
                    table.FillCellInTheData(SelectField);
                }
            }
        }
        #endregion
    }
}

public enum State
{
    normally = 0,
    createText = 1,
    createTable = 2,
    createImage = 3
}
