using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class Borehole //Скважинный ствол
    {
        string Name { get; set; }
        string Attribute { get; set; }
        List<Dataset> Datasets { get; set; } = new List<Dataset>();
        Dictionary<string, string> CollectionAttribures { get; set; } = new Dictionary<string, string>();

        public Borehole()
        {

        }

        public Borehole(string borehole, double[] firstcurse, double[] secondcurse)
        {
            CollectionAttribures.Add("NameBorehole", borehole);
            double[] depth = new double[27] { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0, 1.1, 1.2, 1.3, 1.4, 1.5, 1.6, 1.7, 1.8, 1.9, 2.0, 2.1, 2.2, 2.3, 2.4, 2.5, 2.6, 2.7 };
            Datasets.Add(new Dataset("FirstDataSet"));
            Datasets.Add(new Dataset("SecondDataSet"));
            Datasets[0].AddCurse(firstcurse, "first", "firstFamily", "firstType");
            Datasets[0].AddCurse(secondcurse, "second", "firstFamily", "secondType");
            Datasets[0].AddCurse(depth, "Depth", "secondFamily", "firstType");
        }

        public IEnumerable<string> SearchingRightLVL(string request)
        {
            if (request == typeof(Borehole).Name)
            {
                return GetListNameAttribute();
            }
            else
            {
                return Datasets[0].SearchingRightLVL(request);
            }
        }

        public IEnumerable<string> GetListNameAttribute()
        {
            var ListNameAttribute = new List<string>();
            foreach (var element in CollectionAttribures)
            {
                ListNameAttribute.Add(element.Key);
            }
            return ListNameAttribute;
        }

        public string GetAttribute(string nameAttribute)
        {
            if (CollectionAttribures.ContainsKey(nameAttribute))
            {
                return CollectionAttribures[nameAttribute];
            }
            else
            {
                foreach (var dataset in Datasets)
                {
                    return dataset.GetAttribute(nameAttribute);
                }
            }
            return null;
        }

        public double[] SearchCurse(string name)
        {
            foreach (var dataset in Datasets)
            {
                if (dataset.SearchCurse(name) != null)
                {
                    return dataset.SearchCurse(name);
                }
            }
            return null;
        }

        public double[] SearchCurse(string family, string type)
        {
            foreach (var dataset in Datasets)
            {
                if (dataset.SearchCurse(family,type) != null)
                {
                    return dataset.SearchCurse(family, type);
                }
            }
            return null;
        }
    }
}
