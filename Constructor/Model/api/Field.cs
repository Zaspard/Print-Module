using Constructor.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constructor.Model.api
{
    public class Field //Месторождение
    {

        List<Bush> Bushes { get; set; } = new List<Bush>();
        Dictionary<string, string> CollectionAttribures { get; set; } = new Dictionary<string, string>();

        public Field()
        { }

        public Field(string Field,string Country, string Area)
        {
            CollectionAttribures.Add("NameField", Field);
            CollectionAttribures.Add("NameCountry", Country);
            CollectionAttribures.Add("NameArea", Area);
            Bushes.Add(new Bush("FirstBush","1","1"));
        }

        public IEnumerable<string> SearchingRightLVL(string request)
        {
            if (request == typeof(Field).Name)
            {
                return GetNameAttribut();
            }
            else
            {
                return Bushes[0].SearchingRightLVL(request);
            }   
        }

        public IEnumerable<string> GetNameAttribut()
        {
            var ListNameAttribute = new List<string>();
            foreach(var element in CollectionAttribures)
            {
                ListNameAttribute.Add(element.Key);
            }
            return ListNameAttribute;
        }

        public double[] SearchCurse(string name)
        {
            foreach (var bush in Bushes)
            {
                if (bush.SearchCurse(name) != null)
                {
                    return bush.SearchCurse(name);
                }
            }
            return null;
        }

        public double[] SearchCurse(string family, string type)
        {
            foreach (var bush in Bushes)
            {
                //return (bush.SearchCurse(family, type)!=null) ? (bush.SearchCurse(family, type)) : continue
                if (bush.SearchCurse(family, type) != null)
                {
                    return bush.SearchCurse(family, type);
                }
            }
            return null;
        }
    }
}
