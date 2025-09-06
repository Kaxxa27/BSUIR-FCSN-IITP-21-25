using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _153503_KakhnouskiEugene_Lab3.Entities
{
    class Client
    {
        private List<Product> buy_prod = new List<Product>();
        public List<Product> BuyProd { get => buy_prod; set => buy_prod = value; }
        public string Name { get; set; }
        public Client(string Name) => this.Name = Name;
      
        public void BuyProduct(Product pr)
        {
            BuyProd.Add(pr);
        }

        public IEnumerable<(string Name, double Total)> GetTotalByProduct()
        {
           return BuyProd.GroupBy(g => g.Name).Select(s => (s.Key, s.Sum(p => p.Price)));
        }
    }
}
