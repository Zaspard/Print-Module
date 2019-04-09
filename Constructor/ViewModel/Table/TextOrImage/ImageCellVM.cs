using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Constructor.ViewModel.Table.TextOrImage
{
    [DataContract]
    public class ImageCellVM : BaseVM, ICellVM
    {
        [DataMember]
        private string nameColor;

        [field: NonSerialized]
        private BitmapImage content;
        [DataMember]
        private int cellRow;
        [DataMember]
        private int cellColumn;
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
        [DataMember]
        private bool cellHaveApi = false;
        [field: NonSerialized]
        private SolidColorBrush background = new SolidColorBrush();
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
        private int left = 1, top = 1, right = 1, bottom = 1;
        [DataMember]
        public double OldWidth { get; set; }
        [DataMember]
        public double OldHeight { get; set; }
        [field: NonSerialized]
        public List<string> Colors { get; set; } = new List<string>();
        [field: NonSerialized]
        public List<HorizontalAlignment> HorizontalAlignments { get; set; } = new List<HorizontalAlignment>();
        [field: NonSerialized]
        public List<VerticalAlignment> VerticalAlignments { get; set; } = new List<VerticalAlignment>();
        [DataMember]
        public bool SelectInvokeOnProperyChanged { get; set; } = false;

        //Private ImageCell
        [DataMember]
        private string url;
        [DataMember]
        private double opacity;
        [DataMember]
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
            BorderThickness = new Thickness(1, 1, 1, 1);
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;
            Opacity = 1.0;
        }

        public object Content
        {
            get { return content; }
            set
            {               
                content = (value != null && value is BitmapImage)?(BitmapImage)value: WhiteBackground();
                OnPropertyChanged("Content");
            }
        }

        private BitmapImage WhiteBackground()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(@"C:\Users\zaspard\source\repos\PrintingText\Constructor\Resources\Image1.bmp");
            image.EndInit();
            return image;
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
                if (value < 0)
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
                if (value < 0)
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
                left = isBorderLeft ? 1 : 0;
                ChangeBorger();
                OnPropertyChanged("IsBorderLeft");
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
                OnPropertyChanged("IsBorderTop");
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
                OnPropertyChanged("IsBorderRight");
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
                OnPropertyChanged("IsBorderBottom");
            }
        }

        private void ChangeBorger()
        {
            BorderThickness = new Thickness(left, top, right, bottom);
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
                Content = new BitmapImage();
                ((BitmapImage)Content).BeginInit();
                ((BitmapImage)Content).CacheOption = BitmapCacheOption.OnLoad;
                ((BitmapImage)Content).CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                ((BitmapImage)Content).UriSource = new Uri(url, UriKind.Absolute);
                ((BitmapImage)Content).EndInit();
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

        #region Serialize
        [DataMember]
        private byte[] serializedImage;
        [DataMember]
        private string nameColorSerialize;


        [OnSerializing]
        void StreamImage(StreamingContext sc)
        {
            nameColorSerialize = background.ToString();
            if (Content != null)
            {
                BitmapImage src = (BitmapImage)Content;
                MemoryStream stream = new MemoryStream();
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(src));
                encoder.Save(stream);
                stream.Flush();
                serializedImage = stream.ToArray();
            }
        }

        [OnDeserialized]
        void LoadImage(StreamingContext sc)
        {
            //Filling collection
            Colors = new List<string>();
            HorizontalAlignments = new List<HorizontalAlignment>();
            VerticalAlignments = new List<VerticalAlignment>();
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
            //Deserialize
            BrushConverter brushConverter = new BrushConverter();
            Background = nameColorSerialize != null ? (SolidColorBrush)brushConverter.ConvertFromString(nameColorSerialize) : new SolidColorBrush(System.Windows.Media.Colors.White);
            if (serializedImage != null)
            {
                MemoryStream stream = new MemoryStream(serializedImage);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();
                Content = image;
                /*using (MemoryStream stream = new MemoryStream(serializedImage))
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.EndInit();
                    Content = image;
                    content = image;
                    OnPropertyChanged("Content");
                }*/
            }
        }
        #endregion
    }
}