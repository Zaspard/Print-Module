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
        private bool cellHaveApi = false;

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

        public bool CellHaveApi
        {
            get { return cellHaveApi; }
            set
            {
                cellHaveApi = value;
                OnPropertyChanged("CellHaveApi");
            }
        }

        public string Url { get; set; } //not using
    }
}
