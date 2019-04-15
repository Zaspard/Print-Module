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
        bool IsUsedApi { get; set; }
        string Url { get; set; } //Только для ячейки с изображением
        bool IsBorderLeft { get; set; }
        bool IsBorderTop { get; set; }
        bool IsBorderRight { get; set; }
        bool IsBorderBottom { get; set; }
    }
}
