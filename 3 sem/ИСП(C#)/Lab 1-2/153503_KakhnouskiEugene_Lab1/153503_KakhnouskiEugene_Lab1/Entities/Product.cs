using System;
using System.Collections.Generic;
using System.Text;

namespace _153503_KakhnouskiEugene_Lab1.Entities
{
    class Product
    {

        private string name;
        private double price;

        public string Name { get=>name; set=>name = value; }

        public double Price { get=>price; set=>price = value; }

        public Product(string name, double price)
        {
            this.name = name;
            this.price = price;
        }
    }
}
