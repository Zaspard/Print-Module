using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constructor.Model.api
{
    class Curve
    {
        public double[] array = new double[10];
        public string name;
        public string family;
        public string type;

        public Curve(double[] array, string name, string family, string type)
        {
            this.array = array;
            this.name = name;
            this.family = family;
            this.type = type;
        }
    }
}
