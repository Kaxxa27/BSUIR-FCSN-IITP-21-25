using System;
using System.Collections.Generic;
using System.Text;
using _153503_KakhnouskiEugene_Lab1.Collections;

namespace _153503_KakhnouskiEugene_Lab1.Entities
{
    class Internet_Shop
    {
        private MyCustomCollection<Product> prod = new MyCustomCollection<Product>();
        private MyCustomCollection<Client> clients = new MyCustomCollection<Client>();

        public delegate void Notify(string info);
        public event Notify? Product_Added;
        public event Notify? Client_Added;
        public event Notify? Client_Buy_Product;

        public MyCustomCollection<Product> Prod { get=>prod; set=>prod=value; }
        public MyCustomCollection<Client> Clients { get=>clients; set=>clients = value; }
        public Internet_Shop() { }

        #region Add    
        public void AddProduct(string name, double price)
        {
            prod.Add(new Product(name, price));
            Product_Added?.Invoke(name);
        }
            
        public void AddClient(Client cl)
        {
            Clients.Add(cl);
            Client_Added?.Invoke(cl.Name);
        }

        public void AddClient(string name)
        {
            AddClient(new Client(name));
        }
        #endregion

        #region Client_Buy_Product    
        public void Client_BuyProduct(Client cl, Product pr)
        {
            cl.BuyProduct(pr);
            Client_Buy_Product?.Invoke(cl.Name);
        }
        #endregion

        #region Getters
        public void Get_All_Price_Of_CLient(Client cl)
        { 
            double AllPrice = 0;
            DateTime time = DateTime.Today;
            Console.WriteLine($"Общая сумма покупки {cl.Name}: ");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"Чек от {time.Day}.{time.Month}.{time.Year}");
            foreach (var item in cl.BuyProd)
            {
                AllPrice += item.Price;
            }
            Console.WriteLine($"Сумма {AllPrice}$");
            Console.WriteLine("---------------------------------------\n");
        }

        public void Get_All_Product_Of_CLient(Client cl)
        {
            int count = 0;
            Console.WriteLine($"Продукты {cl.Name}: ");
            Console.WriteLine("***************************************");
            foreach (var item in cl.BuyProd)
            {
                Console.WriteLine($"{count++}. {item.Name}");
            }
            Console.WriteLine("***************************************\n");
        }

        public void Get_Name_Of_Clients()
        {
            int count = 0;
            Console.WriteLine("База данных клиентов: ");
            Console.WriteLine("=======================================");
            foreach (var item in Clients)
            {
                Console.WriteLine($"[{count++}]{item.Name}");
            }
            Console.WriteLine("=======================================\n");
        }

        public void Get_Name_Of_Prod_In_Shop()
        {
            int count = 0;
            Console.WriteLine("База данных продуктов в наличии: ");
            Console.WriteLine("=======================================");
            foreach (var item in Prod)
            {
                Console.WriteLine($"[{count++}]{item.Name} {item.Price}$");
            }
            Console.WriteLine("=======================================\n");
        }
        #endregion
    }
}
