using Constructor.ViewModel.Table.Array;
using Constructor.ViewModel.Table.TextOrImage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Constructor.ViewModel.Table
{
    public class TableWithArrayVM : BaseVM, ITable
    {
        private double width, height;
        private int columns, rows;
        private int oldColumns = 1, oldRows = 1;
        private double xPoint, yPoint, zPoint;
        private Thickness margin;
        private bool isBorder = true;
        private Thickness borderThickness;
        private IUserControl selectCell;
        private string nameTable;

        public ObservableCollection<IUserControl> Cells { get; } = new ObservableCollection<IUserControl>();
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
        public void SelectingCell(int column, int row)
        {
            SelectCell = Cells[((Columns)*row)+(column)];
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
                if (value == 0)
                {
                    return;
                }
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
                {
                    return;
                }
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

        #region Добавление новых строк и столбцов
        public void CreateTextOnRow()
        {
            var cell = Cells[Cells.Count - 1];
            double addHeight = 0;
            if (Columns == 1)
            {
                while (oldRows < Rows)
                {
                    var tableCell = new UserTableVM()
                    {
                        Content = "",
                        Height = cell.Height,
                        Width = cell.Width,
                        CellRow = oldRows,
                        CellColumn = Columns - 1
                    };
                    addHeight += cell.Height;
                    Cells.Add(tableCell);
                    oldRows++;
                }
                Height += addHeight;
                EditTable(Columns, Rows);
                return;
            }
            for (; oldRows < Rows; oldRows++)
            {
                addHeight += cell.Height;
                for (var i = 0; i <= Columns - 1; i++)
                {
                    {
                        var tableCell = new UserTableVM()
                        {
                            Content = "",
                            Height = cell.Height,
                            Width = cell.Width,
                            CellRow = oldRows,
                            CellColumn = i
                        };
                        Cells.Add(tableCell);
                    }
                }
            }
            Height += addHeight;
            EditTable(Columns, Rows);
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
                    var tableCell = new UserTableVM()
                    {
                        Content = "",
                        Height = cell.Height,
                        Width = cell.Width,
                        CellRow = i - 1,
                        CellColumn = oldColumns
                    };
                    Cells.Insert(((oldColumns + 1) * i) - 1, tableCell);
                }
                var tableCellEnd = new UserTableVM()
                {
                    Content = "",
                    Height = cell.Height,
                    Width = cell.Width,
                    CellRow = Rows - 1,
                    CellColumn = oldColumns
                };
                Cells.Add(tableCellEnd);
                oldColumns++;
                addWidth += cell.Width;
            }
            Width += addWidth;
            Debug.WriteLine("Add");
            EditTable(Columns, Rows);
            return;
        }
        #endregion

        #region Удаление элементов
        public void DeleteTextOnRow()
        {
            foreach (var cell in Cells)
            {
                if (cell.CellRow > Rows - 1)
                {
                    DeletedCellsCollection.Add(cell);
                }
            }
            for (var i = Rows; i < oldRows; i++)
            {
                foreach (var deletedCell in DeletedCellsCollection)
                {
                    if (deletedCell.CellRow == i)
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
            EditTable(Columns, Rows);
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
            EditTable(Columns, Rows);
        }
        #endregion

        #region Создание первого элемента
        public void CreateUserTable()
        {
            var userTable = new UserTableVM()
            {
                Content = "",
                CellRow = 0,
                CellColumn = 0,
                Height = 20,
                Width = 50
            };
            Cells.Add(userTable);
            SelectCell = userTable;
            EditTable(Columns, Rows);
        }
        #endregion

        #region Создание таблицы
        private DataTable dataGridTable;
        private DataView defaultTableView;

        public DataTable DataGridTable
        {
            get { return dataGridTable; }

            set
            {
                dataGridTable = value;
                OnPropertyChanged("DataGridTable");
            }
        }
        public DataView DefaultTableView
        {
            get { return defaultTableView; }

            set
            {
                defaultTableView = value;
                OnPropertyChanged("DefaultTableView");
            }
        }

        private void EditTable(int columns, int rows)
        {
            if (DataGridTable != null)
            {
                DataGridTable.Clear();
            }
            DataGridTable = new DataTable();
            DefaultTableView = new DataView();

            DataGridTable.BeginLoadData();
            for (int i = 1; i <= columns; ++i)
            {
                DataGridTable.Columns.Add();
            }
            var content = new object[columns];
            for (int i = 1; i < rows + 1; i++)
            {
                for (int j = 1; j < columns + 1; j++)
                {
                    content[j - 1] = Cells[(j * i) - 1].Content;
                }
                DataGridTable.Rows.Add(content);
                
            }
            DataGridTable.EndLoadData();
            DefaultTableView = DataGridTable.DefaultView;
            Debug.WriteLine("Changed");
        }
        #endregion
    }
}
