using System;
using _153503_KakhnouskiEugene_Lab3.Entities;

namespace _153503_KakhnouskiEugene_Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Internet_Shop MyShoop = new Internet_Shop();

            MyShoop.Client_Buy_Product += (ClientName) =>            
                Console.WriteLine($" Клиент {ClientName} совершил покупку!");

            #region List of Product

            MyShoop.AddProduct("LG TV", new Product("LG TV", 700));
            MyShoop.AddProduct("IPhone 13", new Product("IPhone 13", 1200));
            MyShoop.AddProduct("Coffe Machine", new Product("Coffe Machine", 670));
            MyShoop.AddProduct("IPad 2020", new Product("IPad 2020", 2000));
            MyShoop.AddProduct("Keyboard Logitec", new Product("Keyboard Logitec", 250));
            MyShoop.AddProduct("Mouse Razer 2.0", new Product("Mouse Razer 2.0", 130));

            #endregion

            #region List of Clients

            MyShoop.AddClient(new Client("Jeka"));
            MyShoop.AddClient(new Client("Den"));
            MyShoop.AddClient(new Client("Tom"));
            MyShoop.AddClient(new Client("Jonson"));
            #endregion

            #region Zakupka

            MyShoop.Client_BuyProduct(MyShoop.Clients[0], MyShoop.Product["LG TV"]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[0], MyShoop.Product["IPhone 13"]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[0], MyShoop.Product["Coffe Machine"]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[0], MyShoop.Product["Mouse Razer 2.0"]);

            MyShoop.Client_BuyProduct(MyShoop.Clients[1], MyShoop.Product["LG TV"]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[1], MyShoop.Product["IPhone 13"]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[1], MyShoop.Product["IPad 2020"]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[1], MyShoop.Product["Mouse Razer 2.0"]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[1], MyShoop.Product["IPad 2020"]);

            MyShoop.Client_BuyProduct(MyShoop.Clients[2], MyShoop.Product["Keyboard Logitec"]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[2], MyShoop.Product["Mouse Razer 2.0"]);
            #endregion

            foreach (var client in MyShoop.Clients)
            {
                MyShoop.Get_All_Product_Of_CLient(client);
                MyShoop.Get_All_Price_Of_CLient(client);
            }

            MyShoop.Get_Name_Of_Clients();
            MyShoop.Get_Name_Of_Prod_In_Shop();

            MyShoop.journal.View_Added_Client();
            MyShoop.journal.View_Added_Product();

            MyShoop.Get_Name_Of_Prod_Sorted_OrderBy();
            MyShoop.Get_All_Price_Bought_In_Shop();
            MyShoop.Get_Max_Price_Bought_Of_Client();
            MyShoop.Get_Clients_Who_Paid_More_Than_Sum(3000);
            MyShoop.Get_List_Of_Sum_of_Client("Jeka");
        }    
    }
}
