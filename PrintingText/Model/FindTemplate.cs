namespace PrintingText.Model
{
    class FindTemplate
    {
        private string name = "";
        private string url = "";

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                Name = value;
            }
        }
    }
}
