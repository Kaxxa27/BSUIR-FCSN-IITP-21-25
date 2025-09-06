using System;
using _153503_KakhnouskiEugene_Lab1.Collections;
using _153503_KakhnouskiEugene_Lab1.Entities;

namespace _153503_KakhnouskiEugene_Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Journal journal = new Journal();
            Internet_Shop MyShoop = new Internet_Shop();
     

            MyShoop.Product_Added += journal.On_Product_Added;
            MyShoop.Client_Added  += journal.On_Client_Added;

            MyShoop.Client_Buy_Product += (ClientName) =>            
                Console.WriteLine($" Клиент {ClientName} совершил покупку!");
            


            #region List of Product

            MyShoop.AddProduct("LG TV", 700);
            MyShoop.AddProduct("IPhone 13", 1200);
            MyShoop.AddProduct("Coffe Machine", 670);
            MyShoop.AddProduct("IPad 2020", 2000);
            MyShoop.AddProduct("Keyboard Logitec", 250);
            MyShoop.AddProduct("Mouse Razer 2.0", 130);

            #endregion

            #region List of Clients

            MyShoop.AddClient("Jeka");
            MyShoop.AddClient("Den");
            MyShoop.AddClient("Tom");
            MyShoop.AddClient("Jonson");

            #endregion

            #region Zakupka

            MyShoop.Client_BuyProduct(MyShoop.Clients[0], MyShoop.Prod[0]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[0], MyShoop.Prod[1]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[0], MyShoop.Prod[2]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[0], MyShoop.Prod[0]);


            MyShoop.Client_BuyProduct(MyShoop.Clients[1], MyShoop.Prod[0]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[1], MyShoop.Prod[1]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[1], MyShoop.Prod[2]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[1], MyShoop.Prod[3]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[1], MyShoop.Prod[4]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[1], MyShoop.Prod[5]);

            MyShoop.Client_BuyProduct(MyShoop.Clients[2], MyShoop.Prod[3]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[2], MyShoop.Prod[4]);
            MyShoop.Client_BuyProduct(MyShoop.Clients[2], MyShoop.Prod[5]);

            #endregion

            foreach (var client in MyShoop.Clients)
            {
                MyShoop.Get_All_Product_Of_CLient(client);
                MyShoop.Get_All_Price_Of_CLient(client);
            }

            MyShoop.Get_Name_Of_Clients();
            MyShoop.Get_Name_Of_Prod_In_Shop();

            journal.View_Added_Client();
            journal.View_Added_Product();
   
        }    
    }
}
