using Constructor.ViewModel;
using System.Collections.ObjectModel;
using System.IO;
using Constructor.ViewModel.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;


namespace PrintingText.ViewModel
{
    class ConstructorTab:BaseVM, ITab
    {
        private string selectedFiles = "";
        public ObservableCollection<string> CollectionFiles { get; set; } = new ObservableCollection<string>();

        public ConstructorTab()
        {
            if (!Directory.Exists("Template"))
            {
                Directory.CreateDirectory("Template");
            }
            string[] allFoundFiles = Directory.GetFiles("Template", "*json", SearchOption.AllDirectories);
            foreach (var file in allFoundFiles)
            {
                CollectionFiles.Add(file);
            }
        }

        public string SelectedFiles
        {
            get { return selectedFiles; }
            set
            {
                selectedFiles = value;
                OnPropertyChanged(SelectedFiles);
            }
        }
    }
}
