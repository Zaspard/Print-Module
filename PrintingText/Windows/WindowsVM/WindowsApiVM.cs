﻿using API;
using Constructor.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrintingText.Windows.WindowsVM
{
    class WindowsApiVM : BaseVM
    {
        private int rows;
        private int columns;
        private int oldColumn, oldRow;
        private int startRow, finishRow;
        private int oldStartRow;
        private bool tableIsUsedApi = false;
        private Header selectHeader;
        private Dossier api = new Dossier();
        public ObservableCollection<double> Table { get; } = new ObservableCollection<double>();
        public ObservableCollection<Header> HeadersTable { get; } = new ObservableCollection<Header>();
        public List<Tuple<int, int, string, string, int, int>> Tuples = new List<Tuple<int, int, string, string, int, int>>();
        private List<int> DeleteTuple = new List<int>();

        public WindowsApiVM()
        {
            Table.Add(0);
            rows = 1;
            finishRow = 1;
            Columns = 1;
            StartRow = 1;       
        }

        public Dossier Api
        {
            get { return api; }
        }

        public Header SelectHeader
        {
            get
            {
                return selectHeader;
            }
            set
            {
                selectHeader = value;
                OnPropertyChanged("SelecetHeader");
            }
        }

        public int Rows
        {
            get
            {
                return rows;
            }
            set
            {
                if (value == 0 || value < 0)
                { return; }
                oldRow = rows;
                rows = value;
                finishRow = Rows + startRow - 1;
                OnPropertyChanged("FinishRow");
                if (oldRow < rows)
                {
                    CreateTextOnRow();
                }
                else if (oldRow > rows)
                {
                    DeleteTableElement(false, oldRow, rows, columns);
                }
                else
                {
                    EditTable(columns, rows);
                }
                RefreshTable();
                OnPropertyChanged("Rows");
            }
        }

        public int Columns
        {
            get
            {
                return columns;
            }
            set
            {
                if (value == 0 || value < 0)
                { return; }
                oldColumn = columns;
                columns = value;
                var oldColumns = oldColumn;
                while (oldColumns < columns)
                {
                    HeadersTable.Add(new Header() { Column = oldColumns });
                    Tuples.Add(Tuple.Create(columns - 1, 0, "", "", StartRow, FinishRow));
                    oldColumns++;
                }
                if (oldColumn < columns)
                {
                    CreateTextOnColumn();
                }
                else if (oldColumn > columns)
                {
                    DeleteHeaders();
                    DeleteTableElement(true ,oldColumn, columns, rows);
                }
                else
                {
                    EditTable(columns, rows);
                }
                OnPropertyChanged("Columns");
                RefreshTable();
            }
        }

        public bool TableIsUsedApi
        {
            get
            {
                return tableIsUsedApi;
            }
            set
            {
                tableIsUsedApi = value;
                OnPropertyChanged("TableIsUsedApi");
            }
        }

        public int StartRow
        {
            get { return startRow;}
            set
            {
                if (value < 0)
                { return; }
                oldStartRow = startRow;
                startRow = value;
                finishRow = Rows + startRow - 1;
                OnPropertyChanged("FinishRow");
                OnPropertyChanged();
                RefreshTable();
            }
        }

        public int FinishRow
        {
            get { return finishRow; }
            set
            {
                if (value < 0 || value < startRow)
                { return; }
                finishRow = value;
                Rows = finishRow - startRow + 1;
                OnPropertyChanged("FinishRow");
            }
        }

        #region Добавление новых строк и столбцов
        public void CreateTextOnRow()
        {
            if (Columns == 1)
            {
                while (oldRow < Rows)
                {
                    Table.Add(0);
                    oldRow++;
                }
                EditTable(Columns, Rows);
                return;
            }
            for (; oldRow < Rows; oldRow++)
            {
                for (var i = 0; i <= Columns - 1; i++)
                {
                    {
                        Table.Add(0);
                    }
                }
            }
            EditTable(Columns, Rows);
            return;
        }

        public void CreateTextOnColumn()
        {
            while (oldColumn < Columns)
            {
                for (var i = 1; i < Rows; i++)
                {
                    Table.Insert(((oldColumn + 1) * i) - 1, 0);
                }
                Table.Add(0);
                oldColumn++;
            }
            EditTable(Columns, Rows);
            return;
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
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    content[j] = Table[(columns * i) + j];
                }
                DataGridTable.Rows.Add(content);

            }
            DataGridTable.EndLoadData();
            DefaultTableView = DataGridTable.DefaultView;
            Debug.WriteLine("ChangedApiWindow");
        }
        #endregion

        #region Удаление элементов
        public void DeleteTableElement(bool deleteColumn, int oldfirst, int first, int second)
        {
            if (deleteColumn)
            {
                while (oldfirst > first)
                {
                    for (var i = second; i >= 1; i--)
                    {
                        Table.RemoveAt((oldfirst * i) - 1);
                    }
                    oldfirst--;
                }
            }
            else
            {
                while (oldfirst > first)
                {
                    for (var i = second; i >= 1; i--)
                    {
                        Table.RemoveAt((first * second) + i - 1);
                    }
                    oldfirst--;
                }
            }
            EditTable(Columns, Rows);
        }

        public void DeleteHeaders()
        {
            var DeletedHeadersCollection = new List<Header>();
            foreach (var header in HeadersTable)
            {
                if (header.Column > Columns - 1)
                {
                    DeletedHeadersCollection.Add(header);
                }
            }
            foreach (var header in DeletedHeadersCollection)
            {
                HeadersTable.Remove(header);
            }
            //Delete Tuple
            var DeletedTuplesCollection = new List<Tuple<int, int, string, string, int, int>>();
            foreach (var tuple in Tuples)
            {
                if (tuple.Item1 > Columns - 1)
                {
                    DeletedTuplesCollection.Add(tuple);
                }
            }
            foreach (var tuple in DeletedTuplesCollection)
            {
                Tuples.Remove(tuple);
            }
            DeletedTuplesCollection.Clear();
            EditTable(columns, rows);
        }
        #endregion

        private void RefreshTable()
        {
            foreach (var item in DeleteTuple)
            {
                Tuples[item] = Tuple.Create(columns - 1, 0, "", "", StartRow, FinishRow);
            }
            DeleteTuple.Clear();
            EditTable(columns, rows);
        }


        public void SetSettings(Tuple<int, int, string, string, int, int> tuple)
        {
            Tuples[tuple.Item1] = tuple;
            for (var i = 0; i < Tuples.Count; i++)
            {
                Tuples[i] = Tuple.Create(Tuples[i].Item1, Tuples[i].Item2, Tuples[i].Item3, Tuples[i].Item4, StartRow, FinishRow);
            }
            EditTable(columns, rows);
        }
    }
}