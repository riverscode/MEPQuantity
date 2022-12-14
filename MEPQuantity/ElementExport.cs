using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPQuantity
{
    public class ElementExport
    {
        private string _category;
        private string _quantity;

        public ElementExport(string category, string quantity)
        {
            this._category = category;
            this._quantity = quantity;
        }

        public string GetCategory()
        {
            return _category;
        }


        public string GetQuantity()
        {
            return _quantity;
        }
    }
}
