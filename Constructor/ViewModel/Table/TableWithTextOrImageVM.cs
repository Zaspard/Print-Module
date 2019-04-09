using Constructor.ViewModel.Table.TextOrImage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.Serialization;

namespace Constructor.ViewModel.Table
{
    [DataContract]
    [KnownType(typeof(ImageCellVM))]
    [KnownType(typeof(TextCellVM))]
    public class TableWithTextOrImageVM : BaseVM, ITable
    {
        [DataMember]
        private double width, height;
        [DataMember]
        private int columns, rows;
        [DataMember]
        private int oldColumns = 1, oldRows = 1;
        [DataMember]
        private double xPoint, yPoint, zPoint;
        [DataMember]
        private Thickness margin;
        [DataMember]
        private bool isBorder = true;
        [DataMember]
        private Thickness borderThickness;
        [field: NonSerialized]
        private IUserControl selectCell;
        [DataMember]
        private string nameTable;
        [DataMember]
        private bool isUsedAPi = false;
        [DataMember]
        private int angle;
        [DataMember]
        private Point renderTransformOrigin;
        [DataMember]
        public ObservableCollection<IUserControl> Cells { get; set; } = new ObservableCollection<IUserControl>();
        [DataMember]
        public List<IUserControl> DeletedCellsCollection = new List<IUserControl>();

        public IUserControl SelectCell
        {
            get { return selectCell; }
            set
            {
                selectCell = value;
                OnPropertyChanged("SelectCell");
                SelectCell.PropertyChanged += SelectCell_PropertyChanged;
            }
        }

        #region Выбор ячейки
        public void SelectingCell(object sender)
        {
            var cell = ((TextBox)sender).DataContext;
            var index = Cells.IndexOf((IUserControl)cell);
            SelectCell = Cells[index];
        }

        public void SelectingImageCell(object sender)
        {
            var cell = ((Image)sender).DataContext;
            var index = Cells.IndexOf((IUserControl)cell);
            SelectCell = Cells[index];
        }

        private void SelectCell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WidthCell")
            {
                var changeWidth = false;
                foreach (var cell in Cells)
                {
                    if (((IUserControl)sender).CellColumn == cell.CellColumn)
                    {
                        cell.SelectInvokeOnProperyChanged = true;
                        if (!changeWidth)
                        {
                            Width += ((IUserControl)sender).Width - ((IUserControl)sender).OldWidth;
                            changeWidth = true;
                        }
                        cell.Width = ((IUserControl)sender).Width;
                        cell.SelectInvokeOnProperyChanged = false;
                    }
                }
            }
            if (e.PropertyName == "HeightCell")
            {
                var changeHeight = false;
                foreach (var cell in Cells)
                {
                    if (((IUserControl)sender).CellRow == cell.CellRow)
                    {
                        cell.SelectInvokeOnProperyChanged = true;
                        if (!changeHeight)
                        {
                            Height += ((IUserControl)sender).Height - ((IUserControl)sender).OldHeight;
                            changeHeight = true;
                        }
                        cell.Height = ((IUserControl)sender).Height;
                        cell.SelectInvokeOnProperyChanged = false;
                    }
                }
            }
        }
        #endregion

        public string NameTable
        {
            get { return nameTable; }
            set
            {
                nameTable = value;
                OnPropertyChanged("NameTable");
            }
        }

        public Thickness Margin
        {
            get { return margin; }
            set
            {
                margin = value;
                OnPropertyChanged("Margin");
            }
        }

        public double Width
        {
            get { return width; }
            set
            {
                if (value == 0 || value < 0)
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

        public int Columns
        {
            get { return columns; }
            set
            {
                if (value == 0 || value < 0)
                { return; }
                oldColumns = columns;
                columns = value;
                if (columns != 1)
                {
                    if (oldColumns < columns)
                    {
                        CreateTextOnColumn();
                    }
                }
                if (oldColumns > columns)
                {
                    DeleteTextOnColumn();
                }
                OnPropertyChanged("Columns");
            }
        }

        public int Rows
        {
            get { return rows; }
            set
            {
                if (value == 0 || value < 0)
                { return; }
                oldRows = rows;
                rows = value;
                if (rows != 1)
                {
                    if (oldRows < rows)
                    {
                        CreateTextOnRow();
                    }
                }
                if (oldRows > rows)
                {
                    DeleteTextOnRow();
                }
                OnPropertyChanged("Rows");
            }
        }

        public double XPoint
        {
            get { return xPoint; }
            set
            {
                if (value < 0)
                { return; }
                xPoint = value;
                margin.Left = value;
                OnPropertyChanged("XPoint");
            }
        }

        public double YPoint
        {
            get { return yPoint; }
            set
            {
                if (value < 0)
                { return; }
                yPoint = value;
                margin.Top = value;
                OnPropertyChanged("YPoint");
            }
        }

        public double ZPoint
        {
            get { return zPoint; }
            set
            {
                zPoint = value;
                OnPropertyChanged("ZPoint");
            }
        }

        public bool IsBorder
        {
            get { return isBorder; }
            set
            {
                isBorder = value;
                if (!isBorder)
                {
                    BorderThickness = new Thickness(0, 0, 0, 0);
                }
                else
                {
                    BorderThickness = new Thickness(1, 1, 1, 1);
                }
                OnPropertyChanged("IsBorder");
            }
        }

        public Thickness BorderThickness
        {
            get { return borderThickness; }
            set
            {
                borderThickness = value;
                OnPropertyChanged("BorderThickness");
            }
        }

        public bool IsUsedApi
        {
            get { return isUsedAPi; }
            set
            {
                isUsedAPi = value;
                OnPropertyChanged("IsUsedApi");
            }
        }

        public int Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                OnPropertyChanged("Angle");
            }
        }

        public Point RenderTransformOrigin
        {
            get { return renderTransformOrigin; }
            set
            {
                renderTransformOrigin = value;
                OnPropertyChanged("RenderTransformOrigin");
            }
        }

        #region Добавление новых строк и столбцов
        public void CreateTextOnRow()
        {
            var cell = Cells[Cells.Count - 1];
            double addHeight = 0;
            if (Columns == 1)
            {
                while (oldRows < Rows)
                {
                    var textCell = new TextCellVM()
                    {
                        Content = "",
                        Height = cell.Height,
                        Width = cell.Width,
                        CellRow = oldRows,
                        CellColumn = Columns - 1,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Center,
                        NameColor = System.Windows.Media.Colors.White.ToString(),
                    };
                    addHeight += cell.Height;
                    Cells.Add(textCell);
                    oldRows++;
                }
                Height += addHeight;
                return;
            }
            for (; oldRows < Rows; oldRows++)
            {
                addHeight += cell.Height;
                for (var i = 0; i <= Columns - 1; i++)
                {
                    {
                        var textCell = new TextCellVM()
                        {
                            Content = "",
                            Height = cell.Height,
                            Width = cell.Width,
                            CellRow = oldRows,
                            CellColumn = i,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Center,
                            NameColor = System.Windows.Media.Colors.White.ToString(),
                        };
                        Cells.Add(textCell);
                    }
                }
            }
            Height += addHeight;
            return;
        }

        public void CreateTextOnColumn()
        {
            var cell = Cells[Cells.Count - 1];
            double addWidth = 0;
            while (oldColumns < Columns)
            {
                for (var i = 1; i < Rows; i++)
                {
                    var textCell = new TextCellVM()
                    {
                        Content = "",
                        Height = cell.Height,
                        Width = cell.Width,
                        CellRow = i - 1,
                        CellColumn = oldColumns,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Center,
                        NameColor = System.Windows.Media.Colors.White.ToString(),
                    };
                    Cells.Insert(((oldColumns + 1) * i) - 1, textCell);
                }
                var textCellEnd = new TextCellVM()
                {
                    Content = "",
                    Height = cell.Height,
                    Width = cell.Width,
                    CellRow = Rows - 1,
                    CellColumn = oldColumns,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Center,
                    NameColor = System.Windows.Media.Colors.White.ToString(),
                };
                Cells.Add(textCellEnd);
                oldColumns++;
                addWidth += cell.Width;
            }
            Width += addWidth;
            Debug.WriteLine("Add");
            return;
        }
        #endregion

        #region Удаление элементов
        public void DeleteTextOnRow()
        {
            foreach (var cell in Cells)
            {
                if (cell.CellRow > Rows-1)
                {
                    DeletedCellsCollection.Add(cell);
                }
            }
            for (var i = Rows; i < oldRows; i++)
            {
                foreach (var deletedCell in DeletedCellsCollection)
                {
                    if (deletedCell.CellRow==i)
                    {
                        Height -= ((ICellVM)deletedCell).Height;
                        break;
                    }
                }
            }
            foreach (var deletedCell in DeletedCellsCollection)
            {
                Cells.Remove(deletedCell);                
            }
            DeletedCellsCollection.Clear();
        }

        public void DeleteTextOnColumn()
        {
            foreach (var cell in Cells)
            {
                if (cell.CellColumn > Columns - 1)
                {
                    DeletedCellsCollection.Add(cell);
                }
            }
            for (var i = Columns; i < oldColumns; i++)
            {
                foreach (var deletedCell in DeletedCellsCollection)
                {
                    if (deletedCell.CellColumn == i)
                    {
                        Width -= ((ICellVM)deletedCell).Width;
                        break;
                    }
                }
            }
            foreach (var deletedCell in DeletedCellsCollection)
            {
                Cells.Remove(deletedCell);
            }
            DeletedCellsCollection.Clear();
        }
        #endregion

        #region Создание первого элемента
        public TextCellVM CreateTextBox()
        {
            var textCell = new TextCellVM()
            {
                Content = "",
                Height = 50,
                Width = 80,
                CellRow = 0,
                CellColumn = 0,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                NameColor = System.Windows.Media.Colors.White.ToString(),
            };
            Cells.Add(textCell);
            SelectCell = textCell;
            return textCell;
        }

        public ImageCellVM CreateImage()
        {
            var image = new ImageCellVM()
            {
                Content = "image.png",
                Height = 50,
                Width = 80,
                CellRow = 0,
                CellColumn = 0,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                NameColor = System.Windows.Media.Colors.White.ToString(),
            };
            Cells.Add(image);
            SelectCell = image;
            return image;
        }
        #endregion

    }
}
