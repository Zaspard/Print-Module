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
            Datasets.Add(new Dataset("FirstDataSet"));
            Datasets.Add(new Dataset("SecondDataSet"));
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
    }
}
