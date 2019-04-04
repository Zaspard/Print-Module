using Constructor.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Constructor.Model.api
{
    public class API : BaseVM
    {
        public List<Field> List = new List<Field>();

        public List<string> ListElements { get; set; } = new List<string>() { "Field", "Bush", "Well", "Borehole", "Dataset" };
        public ObservableCollection<string> ListNameAttribute { get; set; } = new ObservableCollection<string>();
        public Classificator Classificator { get; set; } = new Classificator();

        private string selectElement;
        private string selectNameAttribute;

        public API()
        {
            List.Add(new Field("FirstField", "FirstCountry", "FirstArea"));
            List.Add(new Field("SecondField", "SecondCountry", "SecondArea"));
        }

        public string SelectElement
        {
            get
            {
                return selectElement;
            }
            set
            {
                selectElement = value;
                ListNameAttribute.Clear();
                foreach (var item in (List<string>)List[0].SearchingRightLVL(selectElement))
                {
                    ListNameAttribute.Add(item);
                }
                OnPropertyChanged("SelecentElement");
            }
        }

        public string SelectNameAttribute
        {
            get
            {
                return selectNameAttribute;
            }
            set
            {
                selectNameAttribute = value;
                OnPropertyChanged("SelectNameAttribute");
            }
        }

    }
}
