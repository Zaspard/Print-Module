using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constructor.Model.api
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

        public Borehole(string borehole)
        {
            CollectionAttribures.Add("NameBorehole", borehole);
            double[] firstcurse = new double[30] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 ,20 ,21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
            double[] secondcurse = new double[21] { 2571.6027, 1.5935, 189.8989, 461.0468, 21.4505, 166.2000, 133.7544, 15.7016, 30.2221, 178.0000, 0.0004, 34.6110, 4453.1071, 3046.8750, 1.3709, 331.7069, 71.0000, 0.0000, 0.0000, 0.0000, 0.0000 };
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
                return GetNameAttribut();
            }
            else
            {
                return Datasets[0].SearchingRightLVL(request);
            }
        }

        public IEnumerable<string> GetNameAttribut()
        {
            var ListNameAttribute = new List<string>();
            foreach (var element in CollectionAttribures)
            {
                ListNameAttribute.Add(element.Key);
            }
            return ListNameAttribute;
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
