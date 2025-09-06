using System;
using System.Collections.Generic;
using System.Text;

namespace _153503_KakhnouskiEugene_Lab3.Entities
{
    class Journal
    {
        private List<string> addedProduct = new List<string>();
        private List<string> addedCLient = new List<string>();

        public void On_Product_Added(string Product) =>
            addedProduct.Add(Product);

        public void On_Client_Added(string client) =>
            addedCLient.Add(client);

        #region View_Added_Product/Client
        public void View_Added_Product()
        {
            Console.WriteLine("Добавленные продукты:");
            Console.WriteLine("=======================================");
            foreach (var name in addedProduct)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("=======================================\n");
        }
        public void View_Added_Client()
        {
            Console.WriteLine("Добавленные клиенты:");
            Console.WriteLine("=======================================");
            foreach (var name in addedCLient)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("=======================================\n");
        }
        #endregion
    }
}
