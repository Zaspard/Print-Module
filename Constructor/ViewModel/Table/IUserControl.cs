using System.ComponentModel;

namespace Constructor.ViewModel.Table
{
    public interface IUserControl
    {
        object Content { get; set; }
        int CellRow { get; set; } //определяет к какой ячейке это отностится
        int CellColumn { get; set; }
        double Width { get; set; }
        double Height { get; set; }
        double OldWidth { get; set; }
        double OldHeight { get; set; }
        event PropertyChangedEventHandler PropertyChanged;
        bool SelectInvokeOnProperyChanged { get; set; }
    }
}
