using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4_Kakhnouski_Eugene_153503
{
    class Shop
    {
        private List<Product> _products;

        public Shop(List<Product> Products)
        {
            this.Products = Products;
        }
        public Shop()
        {
            Products = new List<Product>();
        }
        public List<Product> Products
        {
            get { return _products; }
            set { _products = value; }
        }
    }
}
