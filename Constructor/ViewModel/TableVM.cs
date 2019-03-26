using Constructor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
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
        private bool isBorder = true;
        private Thickness borderThickness;
        private ICellVM selectCell;
        private string nameTable;

        public ObservableCollection<ICellVM> Cells { get; } = new ObservableCollection<ICellVM>();
        public List<ICellVM> DeletedCellsCollection = new List<ICellVM>();

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

        public void CreateTextOnRow()
        {
            //await Task.Run(() =>
            {
                var cell = Cells[Cells.Count - 1];
                double addHeight = 0;
                if (Columns == 1)
                {
                    while (oldRows < Rows)
                    {
                        //Application.Current.Dispatcher.Invoke(() =>
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
                                IsBorder = true
                            };
                            addHeight += cell.Height;
                            Cells.Add(textCell);
                            oldRows++;
                        }//);
                    }
                    Height += addHeight;
                    EditTable();
                    return;
                }
                for (; oldRows < Rows; oldRows++)
                {
                    addHeight += cell.Height;
                    for (var i = 0; i <= Columns - 1; i++)
                    {

                        //Application.Current.Dispatcher.Invoke(() =>
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
                                IsBorder = true
                            };
                            Cells.Add(textCell);
                        }//);
                    }
                }
                Height += addHeight;
                EditTable();
            }//);
        }

        public  void CreateTextOnColumn()
        {
            //await Task.Run(() =>
            {
                var cell = Cells[Cells.Count - 1];
                double addWidth = 0;
                if (Rows == 1)
                {
                    while (oldColumns < Columns)
                    {
                       // Application.Current.Dispatcher.Invoke(() =>
                        {
                            var textCell = new TextCellVM()
                            {
                                Content = "",
                                Height = cell.Height,
                                Width = cell.Width,
                                CellRow = Rows - 1,
                                CellColumn = oldColumns,
                                HorizontalAlignment = HorizontalAlignment.Stretch,
                                VerticalAlignment = VerticalAlignment.Center,
                                NameColor = System.Windows.Media.Colors.White.ToString(),
                                IsBorder = true
                            };

                            addWidth += cell.Width;
                            Cells.Add(textCell);
                            oldColumns++;
                       }//);
                    }
                    Width = addWidth;
                    EditTable();
                    return;
                }
                for (; oldColumns < Columns; oldColumns++)
                {
                    addWidth += cell.Width;
                    for (var i = 0; i <= Rows - 1; i++)
                    {
                       // Application.Current.Dispatcher.Invoke(() =>
                        {
                            var textCell = new TextCellVM()
                            {
                                Content = "",
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
                        }//);
                    }
                }
                Width += addWidth;
                EditTable();
           }//);
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
            EditTable();
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
            EditTable();
        }

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
                IsBorder = true
            };
            Cells.Add(textCell);
            SelectCell = textCell;
            EditTable();
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
            EditTable();
            return image;
        }



        /////
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

        //private void EditTable()
        //{
        //    DataTable dt = new DataTable();
        //    if (dt != null)
        //    {
        //        dt.Clear();
        //    }
        //    dt = new DataTable();

        //    for (int i = 1; i <= columns; ++i)
        //    {
        //        dt.Columns.Add("", typeof(TextCellVM));
        //    }
        //    var content = new object[columns];
        //    for (int i = 0; i < rows; ++i)
        //    {
        //        for (int j = 0; j < columns; ++j)
        //        {
        //            foreach (var cell in Cells)
        //            {
        //                if ((cell.CellColumn == j) && (cell.CellRow == i))
        //                {
        //                    content[j] = cell;
        //                    break;
        //                }
        //            }
        //        }
        //        dt.Rows.Add(content);
        //    }
        //    DataGridTable = dt;
        //}

        private void EditTable()
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
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    foreach (var cell in Cells)
                    {
                        if ((cell.CellColumn == j) && (cell.CellRow == i))
                        {
                            content[j] = cell.Content;
                            break;
                        }
                    }
                }
                DataGridTable.Rows.Add(content);
            }
            DataGridTable.EndLoadData();

            DefaultTableView = DataGridTable.DefaultView;
            Debug.WriteLine("Changed");
        }
    }
}
