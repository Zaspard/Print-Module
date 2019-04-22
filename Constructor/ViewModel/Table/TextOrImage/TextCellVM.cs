using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Runtime.Serialization;

namespace Constructor.ViewModel.Table.TextOrImage
{
    [DataContract]
    public class TextCellVM : BaseVM, ICellVM
    {
        [DataMember]
        private string nameColor;
        [DataMember]
        private string content;
        [DataMember]
        private int cellRow,  cellColumn;
        [DataMember]
        private Brush borderBrush;
        [DataMember]
        private Thickness borderThickness;
        [DataMember]
        private HorizontalAlignment horizontalAlignment;
        [DataMember]
        private VerticalAlignment verticalAlignment;
        [DataMember]
        private double width, height;
        [field: NonSerialized]
        private SolidColorBrush background;
        [DataMember]
        private int angle;
        [DataMember]
        private bool isBorderLeft = true;
        [DataMember]
        private bool isBorderTop = true;
        [DataMember]
        private bool isBorderRight = true;
        [DataMember]
        private bool isBorderBottom = true;
        [DataMember]
        private bool cellHaveApi = false;
        [DataMember]
        public double OldWidth { get; set; }
        [DataMember]
        public double OldHeight { get; set; }
        [field:NonSerialized]
        public List<string> Colors { get; set; } = new List<string>();
        [field: NonSerialized]
        public List<HorizontalAlignment> HorizontalAlignments { get; set; } = new List<HorizontalAlignment>();
        [field: NonSerialized]
        public List<VerticalAlignment> VerticalAlignments { get; set; } = new List<VerticalAlignment>();
        [DataMember]
        public bool SelectInvokeOnProperyChanged { get; set; } = false;
        [DataMember]
        private int left = 1, top = 1, right = 1, bottom = 1;

        //Private TextCell
        [field: NonSerialized]
        private FontFamily fontFamily;
        [DataMember]
        private float fontSize;
        [field: NonSerialized]
        private FontStyle fontStyle;
        [field: NonSerialized]
        private FontWeight fontWeight;
        [field: NonSerialized]
        private FontStretch fontStretch;
        [field: NonSerialized]
        public List<FontFamily> FontFamils { get; set; } = new List<FontFamily>();
        [field: NonSerialized]
        public ObservableCollection<FontStyle> FontStyles { get; set; } = new ObservableCollection<FontStyle>();
        [field: NonSerialized]
        public ObservableCollection<FontWeight> FontWeights { get; set; } = new ObservableCollection<FontWeight>();
        [field: NonSerialized]
        public ObservableCollection<FontStretch> FontStretches { get; set; } = new ObservableCollection<FontStretch>();

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
            BorderThickness = new Thickness(1, 1, 1, 1);
            Background = new SolidColorBrush(System.Windows.Media.Colors.White);
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
                if (value <= 0)
                { return; }
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
                if (value <= 0)
                { return; }
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

        public int CellRow
        {
            get { return cellRow; }
            set
            {
                if (value < 0)
                { return; }
                cellRow = value;
                OnPropertyChanged("CellRow");
            }
        }

        public int CellColumn
        {
            get { return cellColumn; }
            set
            {
                if (value < 0)
                { return; }
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

        public bool IsBorderLeft
        {
            get { return isBorderLeft; }
            set
            {
                isBorderLeft = value;
                left = isBorderLeft ? 1:0;
                ChangeBorger();
                if (!SelectInvokeOnProperyChanged)
                {
                    OnPropertyChanged("IsBorderLeft");
                }
                else
                {
                    OnPropertyChanged("");
                }
            }
        }

        public bool IsBorderTop
        {
            get { return isBorderTop; }
            set
            {
                isBorderTop = value;
                top = isBorderTop ? 1 : 0;
                ChangeBorger();
                if (!SelectInvokeOnProperyChanged)
                {
                    OnPropertyChanged("IsBorderTop");
                }
                else
                {
                    OnPropertyChanged("");
                }
            }
        }

        public bool IsBorderRight
        {
            get { return isBorderRight; }
            set
            {
                isBorderRight = value;
                right = isBorderRight ? 1 : 0;
                ChangeBorger();
                if (!SelectInvokeOnProperyChanged)
                {
                    OnPropertyChanged("IsBorderRight");
                }
                else
                {
                    OnPropertyChanged("");
                }             
            }
        }

        public bool IsBorderBottom
        {
            get { return isBorderBottom; }
            set
            {
                isBorderBottom = value;
                bottom = isBorderBottom ? 1 : 0;
                ChangeBorger();
                if (!SelectInvokeOnProperyChanged)
                {
                    OnPropertyChanged("IsBorderBottom");
                }
                else
                {
                    OnPropertyChanged("");
                }
                
            }
        }

        private void ChangeBorger()
        {
            BorderThickness = new Thickness(left,top,right,bottom);
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
                if (value < 0)
                { return; }
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

        public bool IsUsedApi
        {
            get { return cellHaveApi; }
            set
            {
                cellHaveApi = value;
                OnPropertyChanged("CellHaveApi");
            }
        }

        public string Url { get; set; } //not using

        #region Serialize
        [DataMember]
        private string nameColorSerialize;
        [DataMember]
        private string fontFamilySerialize;
        [DataMember]
        private string fontStyleSerialize;
        [DataMember]
        private string fontStretchSerialize;
        [DataMember]
        private string fontWeightSerialize;


        [OnSerializing]
        void StreamTextCell(StreamingContext sc)
        {
            nameColorSerialize = background.ToString();
            fontFamilySerialize = FontFamils.ToString();
            fontStyleSerialize = FontStyle.ToString();
            fontStretchSerialize = FontStretch.ToString();
            fontWeightSerialize = FontWeight.ToString();       
        }

        [OnDeserialized]
        void LoadTextCell(StreamingContext sc)
        {
            //Filling collections
            Colors = new List<string>();
            HorizontalAlignments = new List<HorizontalAlignment>();
            VerticalAlignments = new List<VerticalAlignment>();
            FontFamils = new List<FontFamily>();
            FontStyles = new ObservableCollection<FontStyle>();
            FontWeights = new ObservableCollection<FontWeight>();
            FontStretches = new ObservableCollection<FontStretch>();

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
            //Filling property
            BrushConverter brushConverter = new BrushConverter();
            FontFamilyConverter familyConverter = new FontFamilyConverter();
            FontStyleConverter styleConverter = new FontStyleConverter();
            FontStretchConverter stretchConverter = new FontStretchConverter();
            FontWeightConverter weightConverter = new FontWeightConverter();

            Background = nameColorSerialize!= null ? (SolidColorBrush)brushConverter.ConvertFromString(nameColorSerialize) : new SolidColorBrush(System.Windows.Media.Colors.White);
            FontFamily = fontFamilySerialize != null ? (FontFamily)familyConverter.ConvertFromString(fontFamilySerialize) : new FontFamily("Times New Roman");
            FontStyle = fontStyleSerialize != null ? (FontStyle)styleConverter.ConvertFromString(fontStyleSerialize) : FontStyles[0];
            FontStretch = fontStretchSerialize != null ? (FontStretch)stretchConverter.ConvertFromString(fontStretchSerialize) : FontStretches[0];
            FontWeight = fontWeightSerialize != null ? (FontWeight)weightConverter.ConvertFromString(fontWeightSerialize) : FontWeights[0];          
        }
        #endregion

    }
}
