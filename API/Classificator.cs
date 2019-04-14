using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class Classificator: INotifyPropertyChanged
    {
        public List<Family> Families = new List<Family>();

        public Classificator()
        {
            var first = new Family() { Name = "firstFamily" };
            first.DataTypes.Add(new DataType() { Name = "firstType" });
            first.DataTypes.Add(new DataType() { Name = "secondType" });
            Families.Add(first);
            var second = new Family() { Name = "secondFamily" };
            second.DataTypes.Add(new DataType() { Name = "firstType" });
            Families.Add(second);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
    
    public class Family
    {
        public List<DataType> DataTypes = new List<DataType>();
        public string Name { get; set; }
    }

    public class DataType
    {
        public string Name { get; set; }
    }
}
