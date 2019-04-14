using API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Constructor.ViewModel.Table
{
    public interface ITable
    {
        double Width { get; set; }
        double Height { get; set; }
        int Columns { get; set; }
        int Rows { get; set; }
        double XPoint { get; set; }
        double YPoint { get; set; }
        double ZPoint { get; set; }
        Thickness Margin { get; set; }
        bool IsBorder { get; set; }
        Thickness BorderThickness { get; set; }
        IUserControl SelectCell { get; set; }
        string NameTable { get; set; }
        ObservableCollection<IUserControl> Cells { get; }
        bool IsUsedApi { get; set; }
        event PropertyChangedEventHandler PropertyChanged;
        int Angle { get; set; }
        Point RenderTransformOrigin { get; set; }
        void FillCellInTheData(Field SelectField);

    }
}
