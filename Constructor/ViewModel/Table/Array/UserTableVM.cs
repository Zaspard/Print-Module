using System;
using System.Runtime.Serialization;

namespace Constructor.ViewModel.Table.Array
{
    [DataContract]
    class UserTableVM : BaseVM, IUserControl
    {
        [DataMember]
        private string content;
        [DataMember]
        private int cellRow;
        [DataMember]
        private int cellColumn;
        [DataMember]
        private double width, height;
        [DataMember]
        public double OldWidth { get; set; }
        [DataMember]
        public double OldHeight { get; set; }
        [DataMember]
        public bool SelectInvokeOnProperyChanged { get; set; } = false;
        [DataMember]
        private bool isUsedApi = false;

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

        public bool IsUsedApi
        {
            get { return isUsedApi; }
            set
            {
                isUsedApi = value;
                OnPropertyChanged("CellHaveApi");
            }
        }

        #region not using
        public string Url { get; set; } 
        public bool IsBorderLeft { get; set; }
        public bool IsBorderTop { get; set; }
        public bool IsBorderRight { get; set; }
        public bool IsBorderBottom { get; set; }
        #endregion
    }
}
