using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _153503_KakhnouskiEugene_Lab3.Entities
{
    class Internet_Shop
    {
        private Dictionary<string,Product> prod = new Dictionary<string, Product>();
        private List<Client> clients = new List<Client>();

        public Journal journal { get; set; }

        public delegate void Notify(string info);
        public event Notify? Product_Added;
        public event Notify? Client_Added;
        public event Notify? Client_Buy_Product;

        public Dictionary<string, Product> Product { get=>prod; set=>prod=value; }
        public List<Client>  Clients { get=>clients; set=>clients = value; }
        public Internet_Shop() 
        {
            journal = new Journal();
            Product_Added += journal.On_Product_Added;
            Client_Added += journal.On_Client_Added;

        }

        #region Add    
        public void AddProduct(string name, Product pr)
        {
            prod.Add(name, pr);
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
            DateTime time = DateTime.Today;
            Console.WriteLine($"Общая сумма покупки {cl.Name}: ");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"Чек от {time.Day}.{time.Month}.{time.Year}");
            Console.WriteLine($"Сумма {All_Price_Of_Client(cl)}$");
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
            foreach (var item in Product)
            {
                Console.WriteLine($"[{count++}]{item.Key} {item.Value.Price}$");
            }
            Console.WriteLine("=======================================\n");
        }

        public void Get_Name_Of_Prod_Sorted_OrderBy()
        {
            Console.WriteLine("Отсортированный список продуктов по стоимости:");
            foreach (var item in Sorted_Name_List())
            {
                Console.WriteLine($"{item.Value.Name} - {item.Value.Price}$");
            }
        }

        public void Get_All_Price_Bought_In_Shop() =>
            Console.WriteLine($"\nОбщая стоимость проданной продукции: {All_Price_Bought_In_Shop_List()}$");

        public void Get_Max_Price_Bought_Of_Client() =>
            Console.WriteLine($"\nПокупатель, заплативший максимальную сумму: {Max_Bought_Price_Of_CLient().Name} - " +
                $"{All_Price_Of_Client(Max_Bought_Price_Of_CLient())}$");
        public void Get_Clients_Who_Paid_More_Than_Sum(double sum) =>
           Console.WriteLine($"\nКол-во клиентов, заплативших больше {sum}$: {Clients_Who_Paid_More_than_Sum(sum)}");

        public void Get_List_Of_Sum_of_Client(string name)
        {
            Console.WriteLine($"\nСписок сумм {name}:");
            foreach (var item in List_Of_Sum_Of_Client(name))
            {
                Console.WriteLine($"{item}$");
            }
        }
        #endregion

        #region LINQ_Methods
        private IEnumerable<KeyValuePair<string, Product>> Sorted_Name_List() =>
            Product.OrderByDescending(pr => pr.Value.Price)
            ;


        private double All_Price_Bought_In_Shop_List() =>
            Clients.Select(cl => cl.BuyProd.Sum(s => s.Price)).ToList().Sum(s => s);

        private double All_Price_Of_Client(Client cl) =>
            cl.BuyProd.Sum(s => s.Price);

        private Client Max_Bought_Price_Of_CLient()
        {
           var max_value = Clients.Select(cl => cl.BuyProd.Sum(s => s.Price)).Max(m => m);
            clients.Where(c => c.BuyProd.Sum(s => s.Price) == max_value);
            
            foreach (var item in Clients)
            {
                if (All_Price_Of_Client(item) == max_value) return item;
            }
            throw new Exception();
        }

        private int Clients_Who_Paid_More_than_Sum(double sum)
        {
            return Clients.Where(cl => All_Price_Of_Client(cl) > sum).Aggregate(0, (count, sum) => ++count);
           // return Clients.Select(cl => cl.BuyProd.Sum(s => s.Price)).Count(a => a > 1000);
        }

        private IEnumerable<double> List_Of_Sum_Of_Client(string name)
        {
            Client client = new Client("");
            foreach (var item in Clients)
            {
                if (item.Name == name)
                {
                    client = item;
                    break;
                }             
            }

            return client.BuyProd.Select(s => s.Price).ToList();
        }

        #endregion
    }
}
