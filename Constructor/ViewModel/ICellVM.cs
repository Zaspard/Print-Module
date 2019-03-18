using System.Collections.Generic;
using System.ComponentModel;
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
        double OldWidth { get; set; }
        double OldHeight { get; set; }
        SolidColorBrush Background { get; set; }
        int Angle { get; set; }
        Point RenderTransformOrigin { get; set; }
        string NameColor { get; set; }
        List<string> Colors { get; }
        List<HorizontalAlignment> HorizontalAlignments { get; }
        List<VerticalAlignment> VerticalAlignments { get; }
        bool SelectInvokeOnProperyChanged { get; set; }
        event PropertyChangedEventHandler PropertyChanged;
        //margin?
    }
}
