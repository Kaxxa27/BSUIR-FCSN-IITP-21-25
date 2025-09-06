using System;
using System.Collections.Generic;
using System.Text;
using _153503_KakhnouskiEugene_Lab1.Collections;

namespace _153503_KakhnouskiEugene_Lab1.Entities
{
    class Client
    {
        private MyCustomCollection<Product> buy_prod = new MyCustomCollection<Product>();
        public MyCustomCollection<Product> BuyProd { get => buy_prod; set => buy_prod = value; }

        public string Name { get; set; }

        public Client(string Name)
        {
            this.Name = Name;
        }

        public void BuyProduct(Product pr)
        {
            BuyProd.Add(pr);
        }
    }
}
