using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
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

        public Well(string bush, string latitude, string longitude, double[] firstcurse, double[] firstcurse1)
        {
            CollectionAttribures.Add("NameWell", bush);
            CollectionAttribures.Add("WellLatitude", latitude);
            CollectionAttribures.Add("WellLongitude", longitude);
            Boreholes.Add(new Borehole("FirstBorehole", firstcurse, firstcurse1));
        }

        public IEnumerable<string> SearchingRightLVL(string request)
        {
            if (request == typeof(Well).Name)
            {
                return GetListNameAttribute();
            }
            else
            {
                return Boreholes[0].SearchingRightLVL(request);
            }
        }

        public string GetAttribute(string nameAttribute)
        {
            if (CollectionAttribures.ContainsKey(nameAttribute))
            {
                return CollectionAttribures[nameAttribute];
            }
            else if (!CollectionAttribures.ContainsKey(nameAttribute))
            {
                foreach (var borehole in Boreholes)
                {
                   return borehole.GetAttribute(nameAttribute);
                }
            }
            return null;
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
