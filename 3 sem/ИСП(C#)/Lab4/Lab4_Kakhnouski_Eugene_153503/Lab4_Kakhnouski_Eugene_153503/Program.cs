using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4_Kakhnouski_Eugene_153503
{
    class Program
    {
        static void Main(string[] args)
        {
            FileService File = new FileService();

            Shop Magazin = new Shop();
      

            Shop ALMI = new Shop();

            Product milk = new Product("Milk", 200);
            Product bread = new Product("bread", 200);
            Product TV = new Product("TV", 3000, true);
            Product IPhone = new Product("IPhone 13", 1500);
            Product CIG = new Product("CIG", 777);

            Magazin.Products.Add(milk);
            Magazin.Products.Add(bread);
            Magazin.Products.Add(TV);
            Magazin.Products.Add(IPhone);
            Magazin.Products.Add(CIG);
            Magazin.Products.Add(new Product("Jeka", 100000));

            File.SaveData(Magazin.Products, @"C:\BSUIR\3 sem\ИСП(C#)\Lab4\Products.txt");

            ALMI.Products = File.ReadFile(@"C:\BSUIR\3 sem\ИСП(C#)\Lab4\Products.txt").ToList();

            foreach (var item in ALMI.Products)
            {
                Console.WriteLine($"Name - {item.Name}\nPrice - {item.Price}\nisBroken - {item.isBroken}\n");
            }

            List<Product> sortedList = ALMI.Products.OrderBy(pr => pr, new MyCustomComparer()).ToList();

            Console.WriteLine("============================================");
            foreach (var item in sortedList)
            {
                Console.WriteLine($"Name - {item.Name}\nPrice - {item.Price}\nisBroken - {item.isBroken}\n");
            }
        }
    }
}
