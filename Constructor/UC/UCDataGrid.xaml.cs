using Constructor.ViewModel.Table;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Constructor.UC
{
    public partial class UCDataGrid : UserControl
    {
        public UCDataGrid()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dataGrid.SelectedCells.Count != 0)
            {
                var cellInfo = dataGrid.SelectedCells[0];
                var column = cellInfo.Column.DisplayIndex;
                var row = dataGrid.Items.IndexOf(dataGrid.CurrentItem);
                ((TableWithArrayVM)DataContext).SelectingCell(column, row);
            }
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (dataGrid.SelectedCells.Count != 0)
            {
                var cellInfo = dataGrid.SelectedCells[0];
                var content = cellInfo.Column.GetCellContent(cellInfo.Item);
                ((TableWithArrayVM)DataContext).SelectCell.Content = ((TextBox)content).Text;
            }
        }
    }
}
