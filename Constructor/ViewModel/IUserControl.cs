using System.Windows;
using System.Windows.Media;

namespace Constructor.ViewModel
{
    public interface IUserControl
    {
       int CellRow { get; set; } //определяет к какой ячейке это отностится
       int CellColumn { get; set; }
       Brush BorderBrush { get; set; }
       Thickness BorderThickness { get; set; }
    }
}
