using Constructor.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constructor.Model.api
{
    public class Classificator: BaseVM
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
