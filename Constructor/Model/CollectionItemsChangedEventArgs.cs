using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constructor.Model
{
    public class CollectionItemsChangedEventArgs : EventArgs
    {
        private ICollection items = null;
        public CollectionItemsChangedEventArgs(ICollection items)
        {
            this.items = items;
        }

        public ICollection Items
        {
            get
            {
                return items;
            }
        }
    }
}
