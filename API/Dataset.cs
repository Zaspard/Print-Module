using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class Dataset //Набор данных
    {
        string Name { get; set; }
        string Attribute { get; set; }
        Dictionary<string, string> CollectionAttribures { get; set; } = new Dictionary<string, string>();
        List<Curve> Curves = new List<Curve>();

        public Dataset()
        {}

        public Dataset(string dataset)
        {
            CollectionAttribures.Add("NameDataset", dataset);
        }

        public IEnumerable<string> SearchingRightLVL(string request)
        {
            if (request == typeof(Dataset).Name)
            {
                return GetListNameAttribute();
            }
            else
            {
                return null;
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
            return null;
        }

        public void AddCurse(double[] array, string name, string family, string type)
        {
            Curves.Add(new Curve(array, name, family, type));
        }

        public double[] SearchCurse(string name)
        {
            foreach (var curve in Curves)
            {
                if (curve.name == name)
                {
                    return curve.array;
                }
            }
            return null;
        }

        public double[] SearchCurse(string family, string type)
        {
            if (type != null)
            {
                foreach (var curve in Curves)
                {
                    if ((curve.family == family) && (curve.type == type))
                    {
                        return curve.array;
                    }
                }
            }
            else
            {
                foreach (var curve in Curves)
                {
                    if (curve.family == family)
                    {
                        return curve.array;
                    }
                }
            }
            return null;
        }
    }
}
