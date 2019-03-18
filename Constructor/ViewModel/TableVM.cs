using Constructor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Constructor.ViewModel
{
    public class TableVM : BaseVM
    {
        private double width, height;
        private int columns, rows;
        private int oldColumns = 1, oldRows = 1;
        private double xPoint, yPoint ,zPoint;
        private Thickness margin;
        private SolidColorBrush background;
        private bool isSelected = false;
        private bool isBorder = true;
        private Thickness borderThickness;
        private ICellVM selectCell;
        private string nameTable;
        private string nameColor;
        public List<string> Colors { get; } = new List<string>();

        public ObservableCollection<ICellVM> Cells { get; } = new ObservableCollection<ICellVM>();
        public List<ICellVM> DeletedCellsCollection = new List<ICellVM>();

        public TableVM()
        {
            //Background
            Type type = typeof(System.Drawing.Color);
            PropertyInfo[] colorInfo = type.GetProperties(BindingFlags.Public |
                BindingFlags.Static);
            foreach (PropertyInfo info in colorInfo)
            {
                Colors.Add(info.Name);
            }
        }

        public ICellVM SelectCell
        {
            get { return selectCell; }
            set
            {
                selectCell = value;
                OnPropertyChanged("SelectCell");
                SelectCell.PropertyChanged += SelectCell_PropertyChanged;
            }
        }

        public void SelectingCell(object sender)
        {
            var cell = ((TextBox)sender).DataContext;
            var index = Cells.IndexOf((ICellVM)cell);
            SelectCell = Cells[index];
        }

        private void SelectCell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WidthCell")
            {
                var changeWidth = false;
                foreach (var cell in Cells)
                {
                    if (((ICellVM)sender).CellColumn == cell.CellColumn)
                    {
                        cell.SelectInvokeOnProperyChanged = true;
                        if (!changeWidth)
                        {
                            Width += ((ICellVM)sender).Width - ((ICellVM)sender).OldWidth;
                            changeWidth = true;
                        }
                        cell.Width = ((ICellVM)sender).Width;
                        cell.SelectInvokeOnProperyChanged = false;
                    }
                }                
            }
            if (e.PropertyName == "HeightCell")
            {
                var changeHeight = false;
                foreach (var cell in Cells)
                {
                    if (((ICellVM)sender).CellRow == cell.CellRow)
                    {
                        cell.SelectInvokeOnProperyChanged = true;
                        if (!changeHeight)
                        {
                            Height += ((ICellVM)sender).Height - ((ICellVM)sender).OldHeight;
                            changeHeight = true;
                        }
                        cell.Height = ((ICellVM)sender).Height;
                        cell.SelectInvokeOnProperyChanged = false;
                    }
                }
            }
        }

        public string NameTable
        {
            get { return nameTable; }
            set
            {
                nameTable = value;
                OnPropertyChanged("NameTable");
            }
        }

        public string NameColor
        {
            get { return nameColor; }
            set
            {
                nameColor = value;
                Background = new BrushConverter().ConvertFromString(nameColor) as SolidColorBrush;
                OnPropertyChanged("NameColor");
            }
        }

        public SolidColorBrush Background
        {
            get { return background; }
            set
            {
                background = value;
                OnPropertyChanged("Background");
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

        public int Columns
        {
            get { return columns; }
            set
            {
                if (value== 0)
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
                if (value == 0)
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

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public void CreateTextOnRow()
        {
            var cell = Cells[Cells.Count - 1];
            if (Columns==1)
            {
                while (oldRows < Rows)
                {
                    var textCell = new TextCellVM()
                    {
                        Content = "T",
                        Height = cell.Height,
                        Width = cell.Width,
                        CellRow = oldRows,
                        CellColumn = Columns - 1,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Center,
                        NameColor = System.Windows.Media.Colors.White.ToString(),
                        IsBorder = true
                    };
                    Height += cell.Height;
                    Cells.Add(textCell);
                    SelectCell = textCell;
                    oldRows++;
                }
                return;
            }
            for (; oldRows < Rows; oldRows++)
            {
                Height += cell.Height;
                for (var i = 0; i <= Columns-1; i++)
                {
                    var textCell = new TextCellVM()
                    {
                        Content = "T",
                        Height = cell.Height,
                        Width = cell.Width,
                        CellRow = oldRows,
                        CellColumn = i,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Center,
                        NameColor = System.Windows.Media.Colors.White.ToString(),
                        IsBorder = true
                    };
                    Cells.Add(textCell);
                    SelectCell = textCell;
                }
            }
        }

        public void CreateTextOnColumn()
        {
            var cell = Cells[Cells.Count-1];
            if (Rows == 1)
            {
                while (oldColumns < Columns)
                {
                    var textCell = new TextCellVM()
                    {
                        Content = "T",
                        Height = cell.Height,
                        Width = cell.Width,
                        CellRow = Rows - 1,
                        CellColumn = oldColumns,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Center,
                        NameColor = System.Windows.Media.Colors.White.ToString(),
                        IsBorder = true
                    };
                    Width += cell.Width;
                    Cells.Add(textCell);
                    SelectCell = textCell;
                    oldColumns++;
                }
            }
            for (; oldColumns < Columns; oldColumns++)
            {
                Width += cell.Width;
                for (var i = 0; i <= Rows - 1; i++)
                {
                    var textCell = new TextCellVM()
                    {
                        Content = "T",
                        Height = cell.Height,
                        Width = cell.Width,
                        CellRow = i,
                        CellColumn = oldColumns,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Center,
                        NameColor = System.Windows.Media.Colors.White.ToString(),
                        IsBorder = true
                    };
                    Cells.Add(textCell);
                    SelectCell = textCell;
                }
            }
        }

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
                        Height -= deletedCell.Height;
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
                        Width -= deletedCell.Width;
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

        public TextCellVM CreateTextBox()
        {
            var textCell = new TextCellVM()
            {
                Content = "T",
                Height = 50,
                Width = 80,
                CellRow = 0,
                CellColumn = 0,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                NameColor = System.Windows.Media.Colors.White.ToString(),
                IsBorder = true
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
    }
}
