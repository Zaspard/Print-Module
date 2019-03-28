using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Constructor.ViewModel.Table.TextOrImage
{
    public class ImageCellVM : BaseVM, ICellVM
    {
        private string nameColor;
        private Image content;
        private int cellRow;
        private int cellColumn;
        private Brush borderBrush;
        private Thickness borderThickness;
        private HorizontalAlignment horizontalAlignment;
        private VerticalAlignment verticalAlignment;
        private double width, height;
        private bool cellHaveApi = false;
        private SolidColorBrush background = new SolidColorBrush();
        private int angle;
        private Point renderTransformOrigin;

        public double OldWidth { get; set; }
        public double OldHeight { get; set; }
        public List<string> Colors { get; } = new List<string>();
        public List<HorizontalAlignment> HorizontalAlignments { get; } = new List<HorizontalAlignment>();
        public List<VerticalAlignment> VerticalAlignments { get; } = new List<VerticalAlignment>();
        public bool SelectInvokeOnProperyChanged { get; set; } = false;

        //Private ImageCell
        private string url;
        private double opacity;
        private bool isStretched;

        public ImageCellVM()
        {
            //Background
            Type typeBackground = typeof(System.Drawing.Color);
            PropertyInfo[] colorInfo = typeBackground.GetProperties(BindingFlags.Public |
                BindingFlags.Static);
            foreach (PropertyInfo info in colorInfo)
            {
                Colors.Add(info.Name);
            }
            //HorizontalAlignment
            foreach (var item in Enum.GetValues(typeof(HorizontalAlignment)))
            {
                HorizontalAlignments.Add((HorizontalAlignment)item);
            }
            //VerticalAlignment
            foreach (var item in Enum.GetValues(typeof(VerticalAlignment)))
            {
                VerticalAlignments.Add((VerticalAlignment)item);
            }
        }

        public object Content
        {
            get { return content; }
            set
            {               
                content = (value != null && value is Image)?(Image)value:null;
                OnPropertyChanged("Content");
            }
        }

        public HorizontalAlignment HorizontalAlignment
        {
            get { return horizontalAlignment; }
            set
            {
                horizontalAlignment = value;
                OnPropertyChanged("HorizontalAlignment");
            }
        }

        public VerticalAlignment VerticalAlignment
        {
            get { return verticalAlignment; }
            set
            {
                verticalAlignment = value;
                OnPropertyChanged("VerticalAlignment");
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

        public double ChangeWidthInColumn
        {
            get { return width; }
            set
            {
                width = value;
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

        public string NameColor
        {
            get { return nameColor; }
            set
            {
                nameColor = value;
                OnPropertyChanged("NameColor");
            }
        }

        public SolidColorBrush Background
        {
            get { return background; }
            set
            {
                background = value;
                OnPropertyChanged("Background");
            }
        }

        public int Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                OnPropertyChanged("Angle");
            }
        }

        public Point RenderTransformOrigin
        {
            get { return renderTransformOrigin; }
            set
            {
                renderTransformOrigin = value;
                OnPropertyChanged("RenderTransformOrigin");
            }
        }

        public int CellRow
        {
            get { return cellRow; }
            set
            {
                cellRow = value;
                OnPropertyChanged("CellRow");
            }
        }

        public int CellColumn
        {
            get { return cellColumn; }
            set
            {
                cellColumn = value;
                OnPropertyChanged("CellColumn");
            }
        }

        public Brush BorderBrush
        {
            get { return borderBrush; }
            set
            {
                borderBrush = value;
                OnPropertyChanged("BorderBrush");
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

        public string Url
        {
            get { return url; }
            set
            {
                url = value;
                OnPropertyChanged("Url");
            }
        }

        public double Opacity
        {
            get { return opacity; }
            set
            {
                opacity = value;
                OnPropertyChanged("Opacity");
            }
        }

        public bool IsStretched
        {
            get { return isStretched; }
            set
            {
                isStretched = value;
                OnPropertyChanged("IsStretched");
            }
        }

        public bool CellHaveApi
        {
            get { return cellHaveApi; }
            set
            {
                cellHaveApi = value;
                OnPropertyChanged("CellHaveApi");
            }
        }
    }
}
