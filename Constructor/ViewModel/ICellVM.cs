using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Constructor.ViewModel
{
    public interface ICellVM :IUserControl
    {
        object Content { get; set; }
        HorizontalAlignment HorizontalAlignment { get; set; }
        VerticalAlignment VerticalAlignment { get; set; }
        double Width { get; set; }
        double Height { get; set; }
        SolidColorBrush Background { get; set; }
        int Angle { get; set; }
        Point RenderTransformOrigin { get; set; }
        string NameColor { get; set; }
        List<string> Colors { get; }
        List<HorizontalAlignment> HorizontalAlignments { get; }
        List<VerticalAlignment> VerticalAlignments { get; }
        //margin?
    }
}
