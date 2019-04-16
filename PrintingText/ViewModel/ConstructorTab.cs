using Constructor.ViewModel;
using PrintingText.Model;
using System.Collections.ObjectModel;
using System.IO;

namespace PrintingText.ViewModel
{
    class ConstructorTab:BaseVM, ITab
    {
        private FindedTemplate selectedFiles;
        public ObservableCollection<FindedTemplate> CollectionFiles { get; set; } = new ObservableCollection<FindedTemplate>();
        private bool isAwait = true;

        public ConstructorTab()
        {
            if (!Directory.Exists("Template"))
            {
                Directory.CreateDirectory("Template");
            }
            string[] allFoundFiles = Directory.GetFiles("Template", "*json", SearchOption.AllDirectories);
            foreach (var file in allFoundFiles)
            {
                CollectionFiles.Add(new FindedTemplate() { Url = file });
            }
        }

        public FindedTemplate SelectedFiles
        {
            get { return selectedFiles; }
            set
            {
                selectedFiles = value;
                if (selectedFiles != null)
                {
                    OnPropertyChanged("SelectedFiles");
                }
            }
        }

        public bool IsAwait
        {
            get { return isAwait; }
            set
            {
                isAwait = value;
                OnPropertyChanged("IsAwait");
            }
        }

        public void ReloadingCollectionFiles()
        {
            CollectionFiles.Clear();
            string[] allFoundFiles = Directory.GetFiles("Template", "*json", SearchOption.AllDirectories);
            foreach (var file in allFoundFiles)
            {
                CollectionFiles.Add(new FindedTemplate() { Url = file });
            }
        }
    }
}
