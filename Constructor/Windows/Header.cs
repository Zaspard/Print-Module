using Constructor.Model;
using Constructor.Model.api;
using Constructor.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Constructor.Windows
{
    public class Header: BaseVM
    {
        private int column;
        private bool typeText;
        private bool typeFamily;
        private bool typeDepth;
        private string name;
        private Family selectFamily;
        private DataType selectDataType;
        private TypeSelectingApi typeSelectingApi;

        public List<Family> Families { get; } = new List<Family>();
        public ObservableCollection<DataType> DataTypes { get; } = new ObservableCollection<DataType>();

        public bool TypeText
        {
            get
            {
                return typeText;
            }
            set
            {
                typeText = value;
                if (typeText == true)
                {
                    TypeFamily = false;
                    TypeDepth = false;
                    TypeSelectingApi = TypeSelectingApi.TypeText;
                }
                OnPropertyChanged("TypeText");
            }
        }

        public bool TypeFamily
        {
            get
            {
                return typeFamily;
            }
            set
            {
                typeFamily = value;
                if (typeFamily == true)
                {
                    TypeText = false;
                    TypeDepth = false;
                    TypeSelectingApi = TypeSelectingApi.TypeFamily;
                }
                OnPropertyChanged("TypeFamily");
            }
        }

        public bool TypeDepth
        {
            get
            {
                return typeDepth;
            }
            set
            {
                typeDepth = value;
                if (typeDepth == true)
                {
                    TypeText = false;
                    TypeFamily = false;
                    TypeSelectingApi = TypeSelectingApi.TypeFamily;
                }
                TypeSelectingApi = TypeSelectingApi.TypeDepth;
                OnPropertyChanged("TypeDepth");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public TypeSelectingApi TypeSelectingApi
        {
            get
            {
                return typeSelectingApi;
            }
            set
            {
                typeSelectingApi = value;
                OnPropertyChanged("TypeSelectingApi");
            }
        }

        public Family SelectFamily
        {
            get
            {
                return selectFamily;
            }
            set
            {
                selectFamily = value;
                DataTypes.Clear();
                foreach (var dataType in selectFamily.DataTypes)
                {
                    DataTypes.Add(dataType);
                }
                OnPropertyChanged();
            }
        }

        public DataType SelectDataType
        {
            get
            {
                return selectDataType;
            }
            set
            {
                selectDataType = value;
                OnPropertyChanged();
            }
        }

        public int Column
        {
            get { return column; }
            set
            {
                column = value;
                OnPropertyChanged("Column");
            }
        }

        public void AddFamilies(List<Family> list)
        {
            if (Families.Count == 0)
            {
                foreach (var family in list)
                {
                    Families.Add(family);
                }
            }
        }

        public Tuple<int,int,string,string> Setting()
        {
            if (!typeText && !typeFamily && !typeDepth)
            {
                TypeSelectingApi = TypeSelectingApi.None;
            }
            if (TypeSelectingApi == TypeSelectingApi.TypeText)
            {
                Name = name;
                return Tuple.Create(column, 1, name, "");
            }
            else if (TypeSelectingApi == TypeSelectingApi.TypeFamily)
            {
                if (SelectFamily == null)
                {
                    return null;
                }
                Name = (selectDataType != null) ? selectDataType.Name : selectFamily.Name;
                return Tuple.Create(column, 2, Name, (selectDataType != null) ? selectFamily.Name : null);
            }
            else if (TypeSelectingApi == TypeSelectingApi.TypeDepth)
            {
                Name = "Depth";
                return Tuple.Create(column, 1, "Depth", "");
            }
            else
            {
                Name = null;
                return Tuple.Create(column, 3, "", "");
            }
        }

    }

    public enum TypeSelectingApi
    {
        TypeText = 0,
        TypeFamily = 1,
        TypeDepth = 2, 
        None = 3
    }
}