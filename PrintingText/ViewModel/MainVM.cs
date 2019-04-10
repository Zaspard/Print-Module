using Constructor.ViewModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Json;

namespace PrintingText.ViewModel
{
    class MainVM: BaseVM
    {
        private ITab selectTab;
        private PrintersSetting printersSetting = new PrintersSetting();
        private ConstructorTab constructorTab = new ConstructorTab();
        private TemplateVM template;

        public MainVM()
        {
            SelectTab = constructorTab;
        }

        public TemplateVM Template
        {
            get
            {
                return template;
            }
            set
            {
                template = value;
                OnPropertyChanged("Template");
            }
        }

        public ITab SelectTab
        {
            get
            {
                return selectTab;
            }
            set
            {
                selectTab = value;
                OnPropertyChanged("SelectTab");
                SelectTab.PropertyChanged += SelectTab_PropertyChanged;
            }
        }

        private void SelectTab_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Deseriliz(e.PropertyName);
        }

        public void ChangeTab(bool constructor)
        {
            SelectTab = constructor ? (ITab)constructorTab : (ITab)printersSetting;
        }


        DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(TemplateVM));

        public void Deseriliz(string nameFile)
        {
            using (FileStream fs = new FileStream(nameFile, FileMode.OpenOrCreate))
            {
                Template = (TemplateVM)jsonFormatter.ReadObject(fs);
            }
        }
    }
}
