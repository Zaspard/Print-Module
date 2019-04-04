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
            double[] firstcurse = new double[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            double[] secondcurse = new double[10] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            double[] depth = new double[10] { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.01 };
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
