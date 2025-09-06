using System;
using System.Collections.Generic;
using System.Linq;
using _153503_КахновскийЕС_Domain;
using Serializer;

namespace _153503_КахновскийЕС_Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Path for Files JSON, XML, XML_BY_LINQ
            string JSON = @"C:\BSUIR\3 sem\ИСП(C#)\Lab5\153503_КахновскийЕС_Lab5\153503_КахновскийЕС_Lab5\Files\JSON_FILE.json";
            string XML = @"C:\BSUIR\3 sem\ИСП(C#)\Lab5\153503_КахновскийЕС_Lab5\153503_КахновскийЕС_Lab5\Files\XML_FILE.xml";
            string XML_BY_LINQ = @"C:\BSUIR\3 sem\ИСП(C#)\Lab5\153503_КахновскийЕС_Lab5\153503_КахновскийЕС_Lab5\Files\XML_BY_LINQ.xml";
            #endregion


            Library lib = new Library();
            Library l = new Library();


            l.Books.Add(new Book());
            
            lib.Books.Add(new Book());
            lib.Books.Add(new Book("Book1", 1890,false));
            lib.Books.Add(new Book("Book2", 2022,true));
            lib.Books.Add(new Book("Book3",2010));
            lib.Books.Add(new Book("Book4",1099,false));


            MySerializer ser = new MySerializer();

            ser.SerializeJSON(lib.Books, JSON);
            ser.SerializeXML(lib.Books, XML);
            ser.SerializeByLINQ(lib.Books, XML_BY_LINQ);

            
            List<Book> b = ser.DeSerializeByLINQ(XML_BY_LINQ).ToList();
            //List<Book> b = ser.DeSerializeJSON(JSON).ToList();
            //List<Book> b = ser.DeSerializeXML(XML).ToList();
 
            foreach (var i in b)
            {
                Console.WriteLine($" Name: {i.Name}\n Year: {i.YearOfPublication}\n Availability: {i.Availability}\n");
            }
        }
    }
}
