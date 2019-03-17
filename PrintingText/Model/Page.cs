using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PrintingText
{
    class Page : INotifyPropertyChanged
    {
        private bool isPortrait = true;
        private double left;
        private double top;
        private double right;
        private double bottom;
        private double width;
        private double height;

        public bool IsPortrait
        {
            get { return isPortrait; }
            set
            {
                isPortrait = value;
                OnPropertyChanged("IsPortrait");
            }
        }

        public double Left
        {
            get { return left; }
            set
            {
                left = value;
                OnPropertyChanged("Left");
            }
        }

        public double Top
        {
            get { return top; }
            set
            {
                top = value;
                OnPropertyChanged("Top");
            }
        }

        public double Right
        {
            get { return right; }
            set
            {
                right = value;
                OnPropertyChanged("Right");
            }
        }

        public double Bottom
        {
            get { return bottom; }
            set
            {
                bottom = value;
                OnPropertyChanged("Bottom");
            }
        }

        public double Width 
        {
            get { return width; }
            set
            {
                width = value;
                OnPropertyChanged("Wight");
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            //ReInitPage();
        }
    }
}









/*
 * class Page
    {
        private bool IsPortrait { get; set; } = true;
        public double Wight { get; set; }
        public double Height { get; set; }

        public List <PageContent> Create()
        {
           var list = new List<PageContent>();
            list.Add(CreateFixedPage());
            list.Add(CreateFixedPage1());
            return list;
        }

        public List<PageContent> ReCreation()
        {
            var list = new List<PageContent>();
            list.Add(CreateFixedPage());
            list.Add(CreateFixedPage1());
            return list;
        }

        PageContent CreateFixedPage()
        {
            var pageContent = new PageContent();
            var fp = new FixedPage
            {
                Width = Wight,
                Height = Height
            };
            var g = new Grid();
            g.HorizontalAlignment = HorizontalAlignment.Center;
            g.VerticalAlignment = VerticalAlignment.Center;
            fp.Children.Add(g);
            var textBox = new TextBox { Text = "zxcvbnm,./", Margin = new Thickness(0, 0, 0, 0) };
            g.Children.Add(textBox);
            ((IAddChild)pageContent).AddChild(fp);
            return pageContent;
        }

        PageContent CreateFixedPage1()
        {
            var pageContent = new PageContent();
            var fp = new FixedPage
            {
                Width = Wight,
                Height = Height
            };
            var g = new Grid();
            g.HorizontalAlignment = HorizontalAlignment.Center;
            g.VerticalAlignment = VerticalAlignment.Center;
            fp.Children.Add(g);
            var textBox = new TextBox { Text = "qwertyuiop[asdfghjkl.zxcvbnm," };
            g.Children.Add(textBox);
            ((IAddChild)pageContent).AddChild(fp);
            return pageContent;
        }
    }
 */
