using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constructor.Model.api
{
    public class Dataset //Набор данных
    {
        string Name { get; set; }
        string Attribute { get; set; }
        Dictionary<string, string> CollectionAttribures { get; set; } = new Dictionary<string, string>();

        public Dataset()
        {

        }

        public Dataset(string dataset)
        {
            CollectionAttribures.Add("NameDataset", dataset);
        }

        public IEnumerable<string> SearchingRightLVL(string request)
        {
            if (request == typeof(Dataset).Name)
            {
                return GetNameAttribut();
            }
            else
            {
                return null;
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
