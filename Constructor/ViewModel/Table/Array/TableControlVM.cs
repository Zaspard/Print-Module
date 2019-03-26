namespace Constructor.ViewModel.Table.Array
{
    class UserTableVM : BaseVM, IUserControl
    {
        private string content;
        private int cellRow;
        private int cellColumn;
        private double width, height;
        public double OldWidth { get; set; }
        public double OldHeight { get; set; }
        public bool SelectInvokeOnProperyChanged { get; set; } = false;

        public object Content
        {
            get { return content; }
            set
            {
                content = value.ToString();
                OnPropertyChanged("Content");
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
    }
}
