using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using _153503_КахновскийЕС_Domain;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace Serializer
{
    public class MySerializer : ISerializer
    {

        #region SerializeByLINQ

        public void SerializeByLINQ(IEnumerable<Book> Lib, string fileName)
        {

            XDocument xDocument = new XDocument();

            XElement xLib = new XElement("Library");

            foreach (var item in Lib)
            {
                XElement xBook = new XElement("Book");
                xBook.Add(new XAttribute("Name", item.Name ?? "Non"));
                xBook.Add(new XAttribute("YearOfPublication", item.YearOfPublication));
                xBook.Add(new XAttribute("Availability", item.Availability));

                xLib.Add(xBook);
            }
            xDocument.Add(xLib);
            xDocument.Save(fileName);
            Console.WriteLine("XML_BY_LINQ READY!");

        }
        public IEnumerable<Book> DeSerializeByLINQ(string fileName)
        {

            List<Book> books = new List<Book>();

            XDocument xDocument = XDocument.Load(fileName);

            XElement Library = xDocument.Element("Library");

            if (Library is not null)
            {
                foreach (XElement book in Library.Elements("Book"))
                {

                    XAttribute Name = book.Attribute("Name");
                    XAttribute Year = book.Attribute("YearOfPublication");
                    XAttribute Ava = book.Attribute("Availability");

                    books.Add(new Book(Name.Value, Int32.Parse(Year.Value), Boolean.Parse(Ava.Value)));
                }
            }

            return books;
        }

        #endregion

        #region SerializeXML

        public void SerializeXML(IEnumerable<Book> Lib, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Book>));
                xml.Serialize(fs, Lib);
                Console.WriteLine("XML_FILE READY!");
            }
        }
        public IEnumerable<Book> DeSerializeXML(string fileName)
        {

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Book>));
                List<Book> book = xml.Deserialize(fs) as List<Book>;
                return book;

                //XmlSerializer xml = new XmlSerializer(typeof(Book[]));
                //Book[] book = xml.Deserialize(fs) as Book[];
                //return book;
            }
        }

        #endregion

        #region SerializeJSON
        public IEnumerable<Book> DeSerializeJSON(string fileName)
        {
            string js = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<Book>>(js);

            //List<Book>? Books = null;

            //using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            //{
            //    Books = JsonSerializer.Deserialize<List<Book>>(fs);
            //}
            //return Books ?? new List<Book>();
        }
        public void SerializeJSON(IEnumerable<Book> Lib, string fileName)
        {
            //JsonSerializer.Serialize(Lib);
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                JsonSerializer.SerializeAsync(fs, Lib, options);
                Console.WriteLine("JSON_FILE READY!");
            }
        }
        #endregion
    }
}
