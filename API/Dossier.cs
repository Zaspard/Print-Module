using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace API
{
    public class Dossier : INotifyPropertyChanged
    {
        public List<Field> List { get; set; } = new List<Field>();
        public ObservableCollection<string> ListNameAttribute { get; set; } = new ObservableCollection<string>();
        public List<string> ListElements { get; set; } = new List<string>() { "Field", "Bush", "Well", "Borehole", "Dataset" };

        public Classificator Classificator { get; set; } = new Classificator();

        private string selectElement;
        private string selectNameAttribute;
        private Field selectField;

        public Dossier()
        {
            double[] firstcurse = new double[30] {  812, 948, -78, 192, -79, 712, 570, -20, 621, 713, 860, 408, 360, 74, 765, 284, 569, -91, 801, 188, 304, 88, 265, 43, 66, 987, 648, -9, 93, 791 };
            double[] firstcurse1 = new double[30] {  232, 836, 542, 154, 174, 264, 811, -92, 957, -43, 510, 266, 334, 221, 970, 283, 191, 895, -55, 923, 695, 591, 94, 209, 385, 809, 417, 149, -68, -66 };
            double[] secondcurse = new double[30] {  428, 644, 114, 274, 58, 826, 828, 396, 606, 141, 821, 37, 913, 907, 789, 926, 69, 822, 475, 31, -25, 992, 578, 547, 432, 374, -47, 128, -56, -33 };
            double[] secondcurse1 = new double[30] {  264, 772, 840, 102, 674, 811, 312, 532, 644, 888, 901, 831, 310, 456, -90, 364, 490, -44, 167, 191, 547, 163, 587, 948, -39, 567, -55, -15, -60, -85 };
            List.Add(new Field("FirstField", "FirstCountry", "FirstArea", firstcurse, firstcurse1));
            List.Add(new Field("SecondField", "SecondCountry", "SecondArea", secondcurse, secondcurse1));
        }

        public Field SelectField
        {
            get { return selectField; }
            set
            {
                selectField = value;
                OnPropertyChanged("SelectField");
            }
        }

        #region Заполнение массива атрибутов в зависимости от выбранного элемента иерархии. 
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
        #endregion

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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
