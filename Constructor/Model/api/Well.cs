using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constructor.Model.api
{
    public class Well  //Скважина
    {
        string Name { get; set; }
        string Attribute { get; set; }
        List<Borehole> Boreholes { get; set; } = new List<Borehole>();
        Dictionary<string, string> CollectionAttribures { get; set; } = new Dictionary<string, string>();

        public Well()
        {

        }

        public Well(string bush, string latitude, string longitude)
        {
            CollectionAttribures.Add("NameWell", bush);
            CollectionAttribures.Add("WellLatitude", latitude);
            CollectionAttribures.Add("WellLongitude", longitude);
            Boreholes.Add(new Borehole("FirstBorehole"));
        }

        public IEnumerable<string> SearchingRightLVL(string request)
        {
            if (request == typeof(Well).Name)
            {
                return GetNameAttribut();
            }
            else
            {
                return Boreholes[0].SearchingRightLVL(request);
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
            foreach (var borehole in Boreholes)
            {
                if (borehole.SearchCurse(name) != null)
                {
                    return borehole.SearchCurse(name);
                }
            }
            return null;
        }

        public double[] SearchCurse(string family, string type)
        {
            foreach (var borehole in Boreholes)
            {
                if (borehole.SearchCurse(family, type) != null)
                {
                    return borehole.SearchCurse(family, type);
                }
            }
            return null;
        }
    }
}
