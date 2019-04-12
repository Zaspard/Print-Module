namespace PrintingText.Model
{
    class FindedTemplate
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
                url = value;
                var tmp = url.Remove(0,9);                
                Name = tmp.Remove(tmp.IndexOf(".json"), 5);
            }
        }
    }
}
