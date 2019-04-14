﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class Bush  //Куст
    {
        string Name { get; set; }
        string Attribute { get; set; }
        List<Well> Wells { get; set; } = new List<Well>();
        Dictionary<string, string> CollectionAttribures { get; set; } = new Dictionary<string, string>();

        public Bush()
        {}

        public Bush(string bush, string latitude, string longitude, double[] firstcurse, double[] firstcurse1)
        {
            CollectionAttribures.Add("NameBush", bush);
            CollectionAttribures.Add("BushLatitude", latitude);
            CollectionAttribures.Add("BushLongitude", longitude);
            Wells.Add(new Well("FirstWell", "1", "1", firstcurse, firstcurse1));
        }

        public IEnumerable<string> SearchingRightLVL(string request)
        {
            if (request == typeof(Bush).Name)
            {
                return GetListNameAttribute();
            }
            else
            {
                return Wells[0].SearchingRightLVL(request);
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
                foreach (var well in Wells)
                {
                    return well.GetAttribute(nameAttribute);
                }
            }
            return null;
        }

        public double[] SearchCurse(string name)
        {
            foreach (var well in Wells)
            {
                if (well.SearchCurse(name) != null)
                {
                    return well.SearchCurse(name);
                }
            }
            return null;
        }

        public double[] SearchCurse(string family, string type)
        {
            foreach (var well in Wells)
            {
                if (well.SearchCurse(family, type) != null)
                {
                    return well.SearchCurse(family, type);
                }
            }
            return null;
        }
    }
}
