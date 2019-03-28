using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Constructor.ViewModel.Table.TextOrImage
{
    public interface ICellVM :IUserControl
    {
        HorizontalAlignment HorizontalAlignment { get; set; }
        VerticalAlignment VerticalAlignment { get; set; }
        SolidColorBrush Background { get; set; }
        string NameColor { get; set; }
        List<string> Colors { get; }
        List<HorizontalAlignment> HorizontalAlignments { get; }
        List<VerticalAlignment> VerticalAlignments { get; }
        Brush BorderBrush { get; set; }
        Thickness BorderThickness { get; set; }
    }
}
