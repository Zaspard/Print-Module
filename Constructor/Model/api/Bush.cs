using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constructor.Model.api
{
    public class Bush  //Куст
    {
        string Name { get; set; }
        string Attribute { get; set; }
        List<Well> Wells { get; set; } = new List<Well>();
        Dictionary<string, string> CollectionAttribures { get; set; } = new Dictionary<string, string>();

        public Bush()
        {}

        public Bush(string bush, string latitude, string longitude)
        {
            CollectionAttribures.Add("NameBush", bush);
            CollectionAttribures.Add("BushLatitude", latitude);
            CollectionAttribures.Add("BushLongitude", longitude);
            Wells.Add(new Well("FirstWell", "1", "1"));
        }

        public IEnumerable<string> SearchingRightLVL(string request)
        {
            if (request == typeof(Bush).Name)
            {
                return GetNameAttribut();
            }
            else
            {
                return Wells[0].SearchingRightLVL(request);
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
