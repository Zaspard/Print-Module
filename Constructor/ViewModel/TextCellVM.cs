using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace Constructor.ViewModel
{
    public class TextCellVM : BaseVM, ICellVM
    {
        private string content;
        private int cellRow,  cellColumn;
        private Brush borderBrush;
        private Thickness borderThickness;
        private HorizontalAlignment horizontalAlignment;
        private VerticalAlignment verticalAlignment;
        private double width, height;
        public double OldWidth { get; set; }
        public double OldHeight { get; set; }
        private SolidColorBrush background;
        private int angle;
        private Point renderTransformOrigin;
        private bool isBorder;
        //Font
        private FontFamily fontFamily;
        private float fontSize;
        private FontStyle fontStyle;
        private FontWeight fontWeight;
        private FontStretch fontStretch;
        private string nameColor;
        public List<string> Colors { get; } = new List<string>();
        public List<HorizontalAlignment> HorizontalAlignments { get; } = new List<HorizontalAlignment>();
        public List<VerticalAlignment> VerticalAlignments { get; } = new List<VerticalAlignment>();
        public List<FontFamily> FontFamils { get; } = new List<FontFamily>();
        public List<FamilyTypeface> AllFontStyles { get; } = new List<FamilyTypeface>();
        public ObservableCollection<FontStyle> FontStyles { get; } = new ObservableCollection<FontStyle>();
        public ObservableCollection<FontWeight> FontWeights { get; } = new ObservableCollection<FontWeight>();
        public ObservableCollection<FontStretch> FontStretches { get; } = new ObservableCollection<FontStretch>();
        public bool SelectInvokeOnProperyChanged { get; set; } = false;

        public TextCellVM()
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
            //FontFamily
            foreach (var fontFamily in Fonts.SystemFontFamilies)
            {
                FontFamils.Add(fontFamily);
            }
            FontFamily = new FontFamily("Times New Roman");
            FontSize = 12;
        }

        public object Content
        {   get { return content; }
            set
            {
                content = value.ToString();
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
                OldWidth = width;
                width = value;
                if (!SelectInvokeOnProperyChanged)
                {
                    OnPropertyChanged("WidthCell");
                }
                else
                {
                    OnPropertyChanged("");
                }
            }
        }

        public double Height
        {
            get { return height; }
            set
            {
                OldHeight = height;
                height = value;
                if (!SelectInvokeOnProperyChanged)
                {
                    OnPropertyChanged("HeightCell");
                }
                else
                {
                    OnPropertyChanged("");
                }
            }
        }

        public string NameColor
        {
            get { return nameColor; }
            set
            {
                nameColor = value;
                Background = new BrushConverter().ConvertFromString(nameColor) as SolidColorBrush;
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
        public bool IsBorder
        {
            get { return isBorder; }
            set
            {
                isBorder = value;
                if (!isBorder)
                {
                    BorderThickness = new Thickness(0, 0, 0, 0);
                }
                else
                {
                    BorderThickness = new Thickness(1, 1, 1, 1);
                }
                OnPropertyChanged("IsBorder");
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
        // 
        public FontFamily FontFamily
        {
            get { return fontFamily; }
            set
            {
                fontFamily = value;
                FontStyles.Clear();
                FontStretches.Clear();
                FontWeights.Clear();
                foreach (var font in fontFamily.FamilyTypefaces)
                {
                    var foundStyle = false;
                    var foundWeight = false;
                    var foundStretch = false;
                    foreach (var item in FontStyles)
                    {
                        if (item == font.Style)
                        {
                            foundStyle = true;
                            break;
                        }
                    }
                    foreach (var item in FontWeights)
                    {
                        if (item == font.Weight)
                        {
                            foundWeight = true;
                            break;
                        }
                    }
                    foreach (var item in FontStretches)
                    {
                        if (item == font.Stretch)
                        {
                            foundStretch = true;
                            break;
                        }
                    }
                    if (!foundStyle)
                    {
                        FontStyles.Add(font.Style);
                    }
                    if (!foundWeight)
                    {
                        FontWeights.Add(font.Weight);
                    }
                    if (!foundStretch)
                    {
                        FontStretches.Add(font.Stretch);
                    }
                }
                FontStyle = FontStyles[0];
                FontWeight = FontWeights[0];
                FontStretch = FontStretches[0];
                OnPropertyChanged("FontFamily");
            }
        }
        public float FontSize
        {
            get { return fontSize; }
            set
            {
                fontSize = value;
                OnPropertyChanged("FontSize");
            }
        }
        public FontStyle FontStyle
        {
            get { return fontStyle; }
            set
            {
                fontStyle = value;;
                OnPropertyChanged("FontStyle");
            }
        }
        public FontWeight FontWeight
        {
            get { return fontWeight; }
            set
            {
                fontWeight = value;
                OnPropertyChanged("FontWeight");
            }
        }
        public FontStretch FontStretch
        {
            get { return fontStretch; }
            set
            {
                fontStretch = value;
                OnPropertyChanged("FontStretch");
            }
        }


        //
    }
}
