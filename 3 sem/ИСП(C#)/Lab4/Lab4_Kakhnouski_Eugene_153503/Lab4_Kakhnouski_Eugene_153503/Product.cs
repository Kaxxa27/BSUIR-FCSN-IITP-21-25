using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4_Kakhnouski_Eugene_153503
{
    class Product
    {
        private int _price;
        private string _name;
        private bool _isBroken;

        public Product(string Name, int Price, bool isBroken = false )
        {
            this.Name = Name;
            this.Price = Price;
            this.isBroken = isBroken;
        }
        public bool isBroken
        {
            get { return _isBroken; }
            set { _isBroken = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

    }
}
